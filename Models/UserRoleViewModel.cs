using Microsoft.AspNetCore.Identity;
namespace ASMProject.Models
{
    public class UserRoleViewModel
    {
        public IdentityUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}