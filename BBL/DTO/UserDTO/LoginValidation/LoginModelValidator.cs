using BLL.DTO.UserDTO;
using FluentValidation;


namespace BLL.DTO.UserDTO
{
    public class LoginModelValidator:AbstractValidator<GetUserDTO>
	{
		public LoginModelValidator()
		{

			RuleFor(x => x.Login).NotEmpty().WithMessage("test login error");
			RuleFor(x => x.Password).NotNull().WithMessage("test password error");

		}		
	}

}