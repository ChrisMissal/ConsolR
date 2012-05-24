﻿using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Compilify.Web;
using Compilify.Web.Infrastructure.DependencyInjection;
using WebActivator;

[assembly: PreApplicationStartMethod(typeof(DependencyInjection), "Initialize")]

namespace Compilify.Web
{
    public static class DependencyInjection
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();


            builder.RegisterModule(new MvcModule());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}