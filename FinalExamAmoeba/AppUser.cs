using Microsoft.AspNetCore.Identity;

namespace FinalExamAmoeba
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
