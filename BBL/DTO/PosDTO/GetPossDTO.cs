using BLL.DTO.PosDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBL.DTO.PosDTO
{
	public class GetPossDTO
	{
		public List<GetPosDTO> PossDTO { get; set; }
		public int Total { get; set; }
		public int TotalFiltered { get; set; }	
	}
}
