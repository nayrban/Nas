using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using NasScheduleService.Jobs;

namespace NasScheduleService
{
    public partial class Service1 : ServiceBase
    {
        private static IScheduler _scheduler;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();

            IJob myJob = new MinistrySchedule();
            JobDetail jobDetail = new JobDetail("MinistryJob", "Group1", myJob.GetType());
            Trigger trigger = new CronTrigger("MinistryTrigger", "Group1", /*"0 00 6 * * ? *"*/"0 0/1 * * * ?");

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
