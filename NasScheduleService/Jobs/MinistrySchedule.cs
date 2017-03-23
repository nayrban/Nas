using NASEFLibrary.Model;
using NasNotifications;
using NasScheduleService.Dto;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using NasUtilities.Utils;

namespace NasScheduleService.Jobs
{
    public class MinistrySchedule : IJob
    {
        private NasEntities _nasEntities;

        public MinistrySchedule()
        {
            _nasEntities = new NasEntities();
        }

        public void Execute(JobExecutionContext context)
        {
            List<MinistryNotification> listOfMinistryNotifications =  getMinistryNotifications();

            listOfMinistryNotifications.ForEach(prepareNotificationInfo);
        }

        private List<MinistryNotification> getMinistryNotifications()
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
                   RoleDetailSchedule = rd.rd.schedule,
                   RoleDetailMembers = rd.rd.RoleDetailMember
               });

            return entryPoint.ToList();
        }

        #region send notification regions
        private  void prepareNotificationInfo(MinistryNotification ministryNotification)
        {
            string notKeys = ministryNotification.RoleDetailNotKeys;                        
            string resourceTag = !string.IsNullOrEmpty(ministryNotification.RoleDetailDescription) ?
                ministryNotification.RoleDetailDescription : ministryNotification.RoleDescription;

            string message = Resources.ResourceManager.GetString(resourceTag).Inject(ministryNotification);

            string dayShortName = DateTime.Now.GetShortestDayName();

            if (notKeys.Contains(dayShortName)) {
                NotificationMessage notificationMessage;
                List<Member> memBerList = getMembers(ministryNotification);
                foreach(Member member in memBerList)
                {
                    notificationMessage = new NotificationMessage {                     
                        Message = message.Inject(member),
                        Title = ministryNotification.MinistryName,
                        NotificationKeyList = new List<string> {
                            member.Device.First().notificationKey,
                        }
                    };
                    SendNotification(notificationMessage);
                }
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

        private  void SendNotification(NotificationMessage notificationMessage)
        {
            CloudNotification cloudNotification = new FcmNotification();
            cloudNotification.sendNotification(notificationMessage);
        }
        #endregion        
    }
}
