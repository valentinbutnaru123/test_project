
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Repositories.User;
using BLL.DTO.UserDTO;
using Newtonsoft.Json;



namespace WebApp.Controllers
{
	[Authorize]
	public class AuthorizationController : Controller
    {
     
       private readonly IUserRepository userRepository;
	   private readonly LoginModelValidator validation;
    
        public AuthorizationController(IUserRepository userRepository,LoginModelValidator validation)
		{
			//this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			//this.validation = validation ?? throw new ArgumentNullException(nameof(validation));
			this.userRepository = userRepository;
			this.validation = validation;
		}

		[AllowAnonymous]
		public ActionResult Login()
        {
            return View();
        }


		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> Login(BLL.DTO.UserDTO.GetUserDTO model)
		{

			if (ModelState.IsValid)
			{

				var result = validation.Validate(model);

				if (!result.IsValid)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
					}
					return View(model); // return to the view with the validation errors
				}

				var user = userRepository.LoginUser(model.Login, model.Password);

				if (user == null)
				{
					ModelState.AddModelError("", "Invalid login attempt."); 
					return View(model);
				}



				else
				{
					string userData = JsonConvert.SerializeObject(user);

					var authTicket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddDays(15), false, userData);

					string encTicket = FormsAuthentication.Encrypt(authTicket);
					var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
					Response.Cookies.Add(faCookie);

					return RedirectToAction("Index", "Home");

				}
			}
			return View(model);
		}


		[HttpGet]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authorization");
        }
    }
}