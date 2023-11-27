using System.Text.RegularExpressions;
using FluentValidation;
using LabWork1.Models;

namespace LabWork1.Models
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(model => model.FirstName).NotNull().Length(2, 50).Must(BeAValidName).WithMessage("First Name must be a valid name");
            RuleFor(model => model.LastName).NotNull().Length(2, 50).Must(BeAValidName).WithMessage("Last Name must be a valid name");
            RuleFor(model => model.Age).NotNull().Must(BeAValidInteger).WithMessage("Age must be a valid integer");
            RuleFor(model => model.Sex).Null().Length(2, 30)
                .WithMessage("Sex must be between 2 and 30 characters");
            RuleFor(model => model.Email).NotNull().EmailAddress().WithMessage("Email must be a valid email address");
            RuleFor(model => model.Phone).Null().Must(BeAValidPhoneNumber)
                .WithMessage("Phone must be a valid phone number");
            RuleFor(model => model.Address).NotNull().Length(5, 255)
                .WithMessage("Address must be between 5 and 255 characters");
            RuleFor(model => model.City).NotNull().Length(2, 50)
                .WithMessage("City must be between 2 and 50 characters");
            RuleFor(model => model.State).NotNull().Length(2, 50)
                .WithMessage("State must be between 2 and 50 characters");
            RuleFor(model => model.ZipCode).NotNull().Length(5, 9)
                .WithMessage("Zip Code must be between 5 and 9 characters");
        }

        private bool BeAValidInteger(int value)
        {
            return value > 0;
        }

        private bool BeABooleanValue(bool value)
        {
            return value || !value;
        }

        private bool BeAValidPhoneNumber(string value)
        {
            if (value != null)
            {
                return Regex.IsMatch(value, @"^\d{3}-\d{3}-\d{4}$");
            }
            return false;
        }

        private bool BeAValidName(string value)
        {
            if (value != null)
            {
                return Regex.IsMatch(value, @"^[A-Z][a-z]+$");
            }

            return false;
            
        }
    
    }
}