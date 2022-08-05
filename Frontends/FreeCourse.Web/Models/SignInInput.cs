using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models
{
    public class SignInInput
    {
        [Display(Name = "Email address")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Remmember me")]
        public bool IsRemember { get; set; }
    }
}
