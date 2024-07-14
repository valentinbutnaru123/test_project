using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BLL.DTO.UserDTO;


namespace BLL.DTO.UserDTO
{

	public class GetUserDTO
	{
		public int Id { get; set; }	
		public string Name { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }
	    public string Email { get; set; }
		public string Login { get; set; }
		public string Telephone { get; set; }
		public int UserTypeId { get; set; }
		public string UserType { get; set; }	
	}
}
