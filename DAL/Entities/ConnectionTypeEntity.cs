using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Entities
{
	public class ConnectionTypeEntity
	{
		public int Id { get; set; }	
		public string ConnectionType { get; set; }
		public List <PosEntity> Pos { get; set; }	//+
	}
}
