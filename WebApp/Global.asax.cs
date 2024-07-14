using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation;
using FluentValidation.Mvc;
using Autofac;
using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using BLL.DTO.UserDTO;
using System.Collections.Generic;
using System.Security.Claims;
using BLL.Repositories;
using System.Web.UI;


namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			FluentValidationModelValidatorProvider.Configure();
		   
			var container = DIConfiguration.Configure();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}

		protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
		
		{
			HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
			if (authCookie != null) 
			{
				FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

				var user = JsonConvert.DeserializeObject<GetUserDTO>(authTicket.UserData);

				if (user != null)
				{

					var userClaim = new List<Claim>
				    {
					new Claim (ClaimTypes.NameIdentifier,user.Id.ToString()),
					new Claim (ClaimTypes.Name ,user.Name),
					new Claim ("Login" ,user.Login),
					new Claim (ClaimTypes.Email,user.Email),
					new Claim (ClaimTypes.Role,user.UserType),

				    };


					ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaim, System.Web.Security.FormsAuthentication.FormsCookieName);
					var principal = new ClaimsPrincipal(claimsIdentity);

					HttpContext.Current.User = principal;
				}

			}

		}
	}

}
