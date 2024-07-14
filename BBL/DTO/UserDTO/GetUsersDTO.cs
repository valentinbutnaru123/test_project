using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.UserDTO
{
	public class GetUsersDTO
	{
		public int Total { get; set; }
		public int TotalFiltered { get; set; }
		public List<GetUserDTO> UserDTO { get; set; }
	}
}
