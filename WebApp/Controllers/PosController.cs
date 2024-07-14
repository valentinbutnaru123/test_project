using BLL.DTO.PosDTO;
using BLL.Repositories.Pos;
using System.Web.Mvc;
using WebApp.Helpers;

namespace WebApp.Controllers
{
	[Authorize]
	public class PosController : Controller
	{
		private readonly IPosRepository posRepository;


		public PosController(IPosRepository posRepository)
		{
			this.posRepository = posRepository;

		}

		// GET: Pos
		[HttpGet]
		public ActionResult BrowsePos()
		{
			return View();
		}


		[HttpGet]
		public JsonResult QueryPos()
		{
			var criteria = Request.GetPaginatingCriteria();

			var pos = posRepository.QueryPos(criteria);

			var jsonData = new
			{
				draw = Request.QueryString["draw"],
				recordsTotal = pos.Total,
				recordsFiltered = pos.TotalFiltered,
				data = pos.PossDTO

			};

			return Json(jsonData, JsonRequestBehavior.AllowGet);
		}


		[HttpGet]
		public ActionResult CreatePos()
		{

			ViewBag.City = posRepository.GetAllCitites();
			ViewBag.ConType = posRepository.GetAllConnectionType();
			return View();
		}

		[HttpPost]
		public ActionResult CreatePos(AddPOSDTO posModel)
		{
			if (ModelState.IsValid)
			{
				posRepository.AddPos(posModel);
				return RedirectToAction("BrowsePos");
			}


			ViewBag.City = posRepository.GetAllCitites();
			ViewBag.ConType = posRepository.GetAllConnectionType();

			return View();
		}

		[HttpPost]
		public ActionResult EditPos(UpdatePosDTO updatePos)
		{
			ViewBag.City = posRepository.GetAllCitites();
			ViewBag.ConType = posRepository.GetAllConnectionType();

			if (ModelState.IsValid)
			{
				posRepository.UpdatePos(updatePos);
				return PartialView("_EditPosPartialView", updatePos);

			}
			return PartialView("_EditPosPartialView", updatePos);
		}

		[HttpGet]
		public ActionResult EditPosPartialView(int posId)
		{
			var pos = posRepository.GetPosById(posId);

			ViewBag.City = posRepository.GetAllCitites();
			ViewBag.ConType = posRepository.GetAllConnectionType();

			var updatePosDTO = new UpdatePosDTO
			{
				Id = posId,
				Name = pos.Name,
				Telephone = pos.Telephone,
				CellPhone = pos.CellPhone,
				City_Id = pos.City_Id,
				CityName = pos.CityName,
				Brand = pos.Brand,
				Model = pos.Model,
				ConnType_Id = pos.ConnType_Id,
				ConnectionType = pos.ConnectionType,
				Address = pos.Address,
				IssuesId = pos.IssuesId,
				MorningOperning = pos.MorningOperning,
				MorningClosing = pos.MorningClosing,
				AfternoonOpening = pos.AfternoonOpening,
				AfternonClosing = pos.AfternonClosing,
				SelectedDays = pos.SelectedDays,
				Status = pos.Status,
			};

			return PartialView("_EditPosPartialView", updatePosDTO);
		}

		[HttpGet]
		public ActionResult DetailsPosPartialView(int posId)
		{
			var pos = posRepository.GetPosById(posId);

			ViewBag.City = posRepository.GetAllCitites();
			ViewBag.ConType = posRepository.GetAllConnectionType();

			return PartialView("_DetailsPosPartialView", pos);
		}
	}
}