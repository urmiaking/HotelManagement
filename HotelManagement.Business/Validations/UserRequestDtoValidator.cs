using FluentValidation;
using HotelManagement.Models.DTOs;

namespace HotelManagement.Business.Validations;

public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
    public UserRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithName("نام و نام خانوادگی")
            .WithMessage("لطفا {PropertyName} را وارد کنید");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithName("پست الکترونیکی")
            .EmailAddress()
            .WithMessage("لطفا {PropertyName} را به صورت صحیح وارد کنید");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithName("رمز عبور")
            .Equal(x => x.ConfirmPassword)
            .When(x => !string.IsNullOrWhiteSpace(x.Password))
            .WithMessage(x => $"{nameof(x.Password)} باید با {nameof(x.ConfirmPassword)} مطابقت داشته باشد");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithName("تکرار رمز عبور");

    }
}

