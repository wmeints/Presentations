using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NerdCooking.Models
{
    [CustomValidation(typeof(RegistrationViewModel), "ValidatePassword",
        ErrorMessage = "Password does not match the password confirmation")]
    public class RegistrationViewModel
    {
        [Required]
        [Display(Prompt = "Full name",Name = "Full name")]
        public string FullName { get; set; }

        [Required]
        [Display(Prompt = "Username",Name = "Username")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Prompt = "Password", Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Prompt = "Retype password",Name = "Retype password")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required]
        [Display(Prompt = "E-mail address",Name = "E-mail address")]
        public string EmailAddress { get; set; }

        public static ValidationResult ValidatePassword(RegistrationViewModel viewModel,ValidationContext context)
        {
            if(!viewModel.Password.Equals(viewModel.PasswordConfirmation))
            {
                return new ValidationResult(null,new string [] { "Password" });
            }

            return ValidationResult.Success;
        }
    }
}