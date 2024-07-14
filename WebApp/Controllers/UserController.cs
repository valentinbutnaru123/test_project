using BBL.DTO.UserDTO.UserValidation;
using BLL.DTO.UserDTO;
using BLL.Repositories.User;
using System.Web.Mvc;
using WebApp.Helpers;

namespace WebApp.Controllers
{
	[Authorize]
	public class UserController : Controller
	{
		private readonly IUserRepository userRepository;
		private readonly AddUserValidation validation;

		public UserController(IUserRepository userRepository, AddUserValidation validation)
		{

			this.userRepository = userRepository;
			this.validation = validation;
		}

		// GET: User
		[HttpGet]
		public ActionResult Browse()
		{
			return View();
		}

		[HttpGet]
		public JsonResult QueryUsers()
		{
			var criteria = Request.GetPaginatingCriteria();

			var users = userRepository.QueryUsers(criteria);

			var jsonData = new
			{
				draw = Request.QueryString["draw"],
				recordsTotal = users.Total,
				recordsFiltered = users.TotalFiltered,
				data = users.UserDTO
			};

			return Json(jsonData, JsonRequestBehavior.AllowGet);
		}


		[HttpGet]
		public ActionResult CreateUser()
		{
			ViewBag.UserTypes = userRepository.GetAllUsersType();
			return View();
		}

		[HttpPost]
		public ActionResult CreateUser(AddUserDTO model)
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
					ViewBag.UserTypes = userRepository.GetAllUsersType();
					return View(model);
				}

				userRepository.AddUser(model);

				return RedirectToAction("Browse");
			}

			ViewBag.UserTypes = userRepository.GetAllUsersType();
			return View(model);
		}

		[HttpPost]
		public ActionResult EditUser(UpdateUserDTO user)
		{
			ViewBag.UserTypes = userRepository.GetAllUsersType();

			if (ModelState.IsValid)
			{
				userRepository.UpdateUser(user);
				return PartialView("_EditUserPartialView", user);
			}

			return PartialView("_EditUserPartialView", user);
		}

		[HttpGet]
		public ActionResult EditUserPartialView(int userId)
		{
			var getUserDTO = userRepository.GetUserById(userId);

			ViewBag.UserTypes = userRepository.GetAllUsersType();

			var updateUserDTO = new UpdateUserDTO
			{
				Id = userId,
				Name = getUserDTO.Name,
				Email = getUserDTO.Email,
				Login = getUserDTO.Login,
				Telephone = getUserDTO.Telephone,
				UserTypeId = getUserDTO.UserTypeId,
			};

			return PartialView("_EditUserPartialView", updateUserDTO);
		}

		[HttpGet]
		public ActionResult DetailsUserPartialView(int userId)
		{
			var getUserDTO = userRepository.GetUserById(userId);

			ViewBag.UserTypes = userRepository.GetAllUsersType();

			return PartialView("_DetailsUserPartialView", getUserDTO);
		}
	}
}