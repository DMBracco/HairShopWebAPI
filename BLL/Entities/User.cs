using Microsoft.AspNetCore.Identity;

namespace BLL.Entities
{
    public class User : IdentityUser
    {
        public List<Check> Check { get; set; } = new();
        public List<Supply> Supply { get; set; } = new();
    }
}
