using NASEFLibrary.Model;
using NasNotifications;
using NasScheduleService.Dto;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using NasUtilities.Utils;
using System.Threading.Tasks;

namespace NasScheduleService.Jobs
{
    public class MinistrySchedule : IJob
    {

        public void Execute(JobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            NasEntities _nasEntities = (NasEntities)dataMap.Get("entity");
            List<MinistryNotification> listOfMinistryNotifications =  getMinistryNotifications(_nasEntities);

            listOfMinistryNotifications.ForEach(prepareNotificationInfo);
        }

        private List<MinistryNotification> getMinistryNotifications(NasEntities _nasEntities)
        {
            DateTime StartOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            DateTime EndOfLastWeek = StartOfWeek.AddDays(+6);

            var entryPoint = _nasEntities.Ministries.Join(_nasEntities.RoleMinistries,
               m => m.id,
               rm => rm.ministryId,
               (m, rm) => new { m, rm }).
               Join(_nasEntities.RoleDetails,
               mrm => mrm.rm.roleId,
               rd => rd.roleId,
               (mrm, rd) => new { mrm, rd }).
               Where(mrmrd => mrmrd.rd.schedule >= StartOfWeek && mrmrd.rd.schedule <= EndOfLastWeek).
               Select(rd => new MinistryNotification
               {
                   MinistryName = rd.mrm.m.name,
                   MinistryDescription = rd.mrm.m.description,
                   MinistryMembers = rd.mrm.m.MinistryMember,
                   RoleName = rd.mrm.rm.Role.name,
                   RoleDescription = rd.mrm.rm.Role.description,
                   RoleDetailNotKeys = rd.rd.notificationsKeyDays,
                   RoleDetailName = rd.rd.name,
                   RoleDetailDescription = rd.rd.description,
                   RoleDetailSchedule =  rd.rd.schedule,
                   RoleDetailMembers = rd.rd.RoleDetailMember
               });
            return entryPoint.ToList();
        }

        #region send notification regions
        private async void prepareNotificationInfo(MinistryNotification ministryNotification)
        {
            ministryNotification.RoleDetailScheduleValue = ministryNotification.RoleDetailSchedule.Value.ToString("dd/MM/yyyy");

            string notKeys =  ministryNotification.RoleDetailNotKeys;                            

            string message = getNotificationMessage(ministryNotification);
       
            string dayShortName = DateTime.Now.GetShortestDayName();

            if (notKeys.Contains(dayShortName)) {
                NotificationMessage notificationMessage;
                List<Member> memBerList = getMembers(ministryNotification);
                foreach(Member member in memBerList)
                {
                    createNotificationMessage(out notificationMessage, message, ministryNotification.MinistryName, member);
                    await SendNotification(notificationMessage);
                }
            }
        }

        private void createNotificationMessage(out NotificationMessage notificationMessage, string message, string ministryName, Member member)
        {
            notificationMessage = new NotificationMessage
            {
                Message = message.Inject(member),
                Title = ministryName,
            };

            if (member.Device.Count > 0) {
                notificationMessage.NotificationKeyList = new List<string>
                {
                    member.Device.First().notificationKey
                };
                notificationMessage.NotificationType = NotificationType.Fcm;
            }else if(!String.IsNullOrEmpty(member.email))
            {
                notificationMessage.Name = member.name;
                notificationMessage.To = member.email;                
            }            
        }

        private  List<Member> getMembers(MinistryNotification ministryNotification)
        {
            List<Member> memBerList = new List<Member>();
            if (ministryNotification.RoleDetailMembers.Count > 0)
            {
                ministryNotification.RoleDetailMembers.ToList().ForEach(rd => AddMemberToList(rd.Member,  memBerList));
            }
            else if (ministryNotification.MinistryMembers.Count > 0)
            {
                ministryNotification.MinistryMembers.ToList().ForEach(rd => AddMemberToList(rd.Member, memBerList));
            }
            return memBerList;
        }

        private  void AddMemberToList(Member member, List<Member> memBerList)
        {         
            memBerList.Add(member);
        }

        private async Task SendNotification(NotificationMessage notificationMessage)
        {
           await CloudNotificationManager.SendNotification(notificationMessage);
        }

        private string getNotificationMessage(MinistryNotification ministryNotification)
        {
            string resourceTag = !string.IsNullOrEmpty(ministryNotification.RoleDetailDescription) ?
                        ministryNotification.RoleDetailDescription : ministryNotification.RoleDescription;

            string message = Resources.ResourceManager.GetString(resourceTag);
            if (String.IsNullOrEmpty(message))
                message = resourceTag;
            message = message.Inject(ministryNotification);

            return message;
        }
        #endregion        
    }
}
