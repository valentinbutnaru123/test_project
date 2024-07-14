using BLL.DTO.PosDTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BBL.DTO.PosDTO.PosValidation
{
	public class PosValidation:AbstractValidator<AddPOSDTO>
	{
		public PosValidation()
		{
			RuleFor(x => x.Name)
				.NotNull().WithMessage("Name is required.");

			RuleFor(x => x.CellPhone).NotNull().WithMessage("Telephone is required");

			RuleFor(x => x.Address)
				.NotNull().WithMessage("Email is required.").Length(20, 100);

			RuleFor(p => p.Telephone)
						   .NotEmpty()
						   .NotNull().WithMessage("Phone Number is required.")
						   .MinimumLength(9).WithMessage("PhoneNumber must not be less than 10 characters.")
						   .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
						   .Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("PhoneNumber not valid");

			RuleFor(p => p.CityName).NotNull().WithMessage("City is required");
			RuleFor(p => p.Brand).NotNull().WithMessage("Brand is required");
			RuleFor(p => p.Model).NotNull().WithMessage("Model is required");
			RuleFor(p => p.ConnectionType).NotNull().WithMessage("ConnectionType is required");

			RuleFor(p => p.MorningOperning).NotNull().WithMessage("MorningOperning is required");
			RuleFor(p => p.MorningClosing).NotNull().WithMessage("MorningClosing is required");
			RuleFor(p => p.AfternonClosing).NotNull().WithMessage("AfternonClosing is required");
			RuleFor(p => p.AfternoonOpening).NotNull().WithMessage("AfternoonOpening is required");

			RuleFor(p => p.SelectedDays).NotNull().WithMessage("CloseDays are required");

		}
	}

	
}
