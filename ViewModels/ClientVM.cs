using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class ClientVM
    {
        private int _ClientId;
        public int ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
