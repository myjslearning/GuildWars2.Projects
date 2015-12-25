using System;
using System.ComponentModel.DataAnnotations;

namespace GuildWars2Web.Models
{
    public class ProfileViewModel
    {

        [Display(Name = "Role")]
        public string Role { get; set; }


        [Display(Name = "Level")]
        public int Level { get; set; }


        [Display(Name = "Rank")]
        public string Rank { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }


        [Display(Name = "Avatar")]
        [DataType(DataType.ImageUrl)]
        public string Avatar { get; set; }

        [Display(Name = "Subcribe Date")]
        [DataType(DataType.DateTime)]
        public DateTime SubDate { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Userame")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password confirmation does not match.")]
        public string ConfirmPassword { get; set; }

        /*[Display(Name = "Accept Terms")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must agree with the terms of service.")]
        public bool AcceptTerms { get; set; }*/
    }
}