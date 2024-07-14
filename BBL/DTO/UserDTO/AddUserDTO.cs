using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.UserDTO
{
	public class AddUserDTO
	{
		
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Login { get; set; }
		public string Telephone { get; set; }
		public int UserTypeId { get; set; }
		public string UserType { get; set; }
	}
}
