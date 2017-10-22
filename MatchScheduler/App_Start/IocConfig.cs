using Autofac;
using Autofac.Integration.Mvc;
using MatchScheduler.Calculation;
using MatchScheduler.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatchScheduler.App_Start
{
    public class IocConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<ScheduleCalculator>().As<IScheduleCalculator>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}