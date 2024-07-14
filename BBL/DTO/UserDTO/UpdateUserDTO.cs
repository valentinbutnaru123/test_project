using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.UserDTO
{
	public class UpdateUserDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Telephone { get; set; }
		public int UserTypeId { get; set; }
	}
}
