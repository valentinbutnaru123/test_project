
using System.Collections.Generic;
using System.Web.UI;

namespace BLL.DTO.PosDTO
{
	public class AddPOSDTO
	{
		public string Name { get; set; }
		public string Telephone { get; set; }
		public string CellPhone { get; set; }
		public string Address { get; set; }
		public int City_Id { get; set; }
		public string CityName { get; set;}
		public string Model { get; set; }
		public string Brand { get; set; }
		public int ConnType_Id { get; set; }
		public string ConnectionType { get; set; }
		public string MorningOperning { get; set; }
		public string MorningClosing { get; set; }
		public string AfternoonOpening { get; set; }
		public string AfternonClosing { get; set; }
		public List<string> SelectedDays { get; set; } = new List<string>();
		public long InsertDate { get; set; }

		


	}
}
