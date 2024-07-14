using Autofac;
using Autofac.Integration.Mvc;
using BLL.Repositories;
using BLL.Repositories.User;
using DataAccessLayer;
using FluentValidation;
using System.Web.UI;
using WebApp.Controllers;
using BLL.DTO.UserDTO;
using BBL.DTO.UserDTO.UserValidation;
using BLL.Repositories.Pos;
using System.Linq;
using BBL.DTO.PosDTO.PosValidation;
using BLL.Repositories.Issue;



namespace WebApp
{
    public static class DIConfiguration
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Register DbContext
            builder.Register(c =>
            {
                return new ApplicationDbContext();
            }).SingleInstance();

            // Registering Repositories 
            RegisterRepositories(builder);

            // Registering Validators
            RegisterValidators(builder);

            // Registering Controllers
            RegisterControllers(builder);

          
            
            return builder.Build();
        }

        public static void RegisterRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<PosRepository>().As<IPosRepository>(); 
            builder.RegisterType<IssueRepository>().As<IIssueRepository>(); 
        }


        public static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(HomeController).Assembly);
        }

        public static void RegisterValidators(ContainerBuilder builder) 
        {
		    
            builder.RegisterAssemblyTypes(typeof(LoginModelValidator).Assembly)
	        .Where(t => t.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IValidator<>))))
	        .AsImplementedInterfaces()
	        .InstancePerLifetimeScope();

			builder.RegisterType<LoginModelValidator>().AsSelf().InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(typeof(AddUserValidation).Assembly)
           .Where(t=>t.IsClosedTypeOf(typeof(IValidator<>)))
           .AsImplementedInterfaces()
           .InstancePerLifetimeScope();

			builder.RegisterType<AddUserValidation>().AsSelf().InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(typeof(PosValidation).Assembly)
		   .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
		   .AsImplementedInterfaces()
		   .InstancePerLifetimeScope();

			builder.RegisterType<PosValidation>().AsSelf().InstancePerLifetimeScope();


		}

	}
}
