using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ViewModels
{
    public class UserRoleVM
    {
        [Key]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }

}
