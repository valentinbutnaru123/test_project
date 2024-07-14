using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Entities
{
	public class CityEntity
	{
		public int Id { get; set; }
		public string CityName { get; set; }
		public List<PosEntity> Pos { get; set; }	//+
	}
}
