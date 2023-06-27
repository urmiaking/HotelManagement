using FluentValidation;
using HotelManagement.Models.DTOs;

namespace HotelManagement.Business.Validations;

public class AuthenticationDtoValidator : AbstractValidator<AuthenticationDto>
{
    public AuthenticationDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithName("نام کاربری")
            .WithMessage("لطفا {PropertyName} را وارد کنید");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithName("رمز عبور")
            .WithMessage("لطفا {PropertyName} را وارد کنید");

    }
}