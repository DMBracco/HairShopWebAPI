using DAL.Entities;
using HairShopWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HairShopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ShopContext _context;

        public AccountController(ShopContext context)
        {
            _context = context;
        }

        [HttpGet("/account-check-user")]
        public UserModel CheckUser(string email, string password)
        {
            var user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.Email == email && u.Password == password);

            var userModel = new UserModel
            {
                roleName = user?.Role?.Name
            };

            if(null != userModel)
                return userModel;

            return userModel;
        }
    }
}
