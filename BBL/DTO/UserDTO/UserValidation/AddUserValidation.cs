using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO.UserDTO;
using BLL.Repositories.User;
using FluentValidation;

namespace BBL.DTO.UserDTO.UserValidation
{
	public class AddUserValidation : AbstractValidator<AddUserDTO>
	{
		public AddUserValidation()
		{
			RuleFor(user => user.Login)
			.NotNull ().WithMessage("Login is required.");
	
			RuleFor(x => x.Password).NotNull().WithMessage(" Password is required");

			RuleFor(x => x.Email)
			.NotNull().WithMessage("Email is required.")
			.EmailAddress().WithMessage("Invalid email format.");

			RuleFor(x => x.Telephone)
				.NotNull().WithMessage("Phone number is required.");
				//.Matches(@"^\+[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

			RuleFor(x => x.Name	).NotNull().WithMessage("Please specify a  name");
			RuleFor(x => x.UserTypeId).NotNull().WithMessage("Please specify a user type");
		}
	}

	
}
