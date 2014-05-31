using System;
using Autofac;

namespace SeleniumWithXunit.Infrastructure
{
    public class TestRun
    {
        public static void Setup()
        {
            SetupContainer();
            SetupCleanup();
        }

        private static void SetupCleanup()
        {
            AppDomain.CurrentDomain.DomainUnload += (sender, args) => Container.Dispose();
        }

        private static void SetupContainer()
        {
            var builder = new ContainerBuilder();

            var thisAssembly = typeof (TestRun).Assembly;

            builder.RegisterType<BrowserFactory>().AsSelf().SingleInstance();
            builder.RegisterType<BrowserPool>().AsSelf().SingleInstance();

            builder.RegisterAssemblyTypes(thisAssembly)
                .Where(t => t.Implements<IPageObject>())
                .AsSelf()
                .PropertiesAutowired()
                .InstancePerDependency();

            builder.Register(c => c.Resolve<BrowserPool>().TakeOne())
                .OnRelease(browser => Container.Resolve<BrowserPool>().Return(browser))
                .AsSelf()
                .InstancePerLifetimeScope();

            Container = builder.Build();
        }


        public static IContainer Container { get; private set; }
    }
}