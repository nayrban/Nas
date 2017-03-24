using System;
using System.ServiceProcess;
using Quartz;
using Quartz.Impl;
using NasScheduleService.Jobs;
using NASEFLibrary.Model;

namespace NasScheduleService
{
    public partial class ScheduleService : ServiceBase
    {
        private static IScheduler _scheduler;        

        public ScheduleService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            NasEntities _nasEntities  = new NasEntities();

            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();

            IJob myJob = new MinistrySchedule();
            JobDetail jobDetail = new JobDetail("MinistryJob", "Group1", myJob.GetType());
            Trigger trigger = new CronTrigger("NasTriggers", "Group1", /*"0 00 6 * * ? *"*/"0 0/1 * * * ?");

            jobDetail.JobDataMap.Put("entity", _nasEntities);
            _scheduler.ScheduleJob(jobDetail, trigger);

            _scheduler.Start();
        }

        protected override void OnStop()
        {
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
