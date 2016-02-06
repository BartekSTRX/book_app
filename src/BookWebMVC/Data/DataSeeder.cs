using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using BookWebMVC.Data.Model;
using Microsoft.AspNet.Identity;

namespace BookWebMVC.Data
{
    public class DataSeeder
    {
        private readonly UserManager<BookWebUser> _userManager;

        public DataSeeder(UserManager<BookWebUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SeedDataAsync()
        {
            var user1 = new BookWebUser
            {
                UserName = "user1",
                Email = "user1@ab.cd"
            };
            var user2 = new BookWebUser
            {
                UserName = "user2",
                Email = "user2@ef.gh"
            };

            if (await _userManager.FindByEmailAsync(user1.Email) == null)
            {
                await _userManager.CreateAsync(user1, "P@ssw0rd!1");
            }
            if (await _userManager.FindByEmailAsync(user2.Email) == null)
            {
                await _userManager.CreateAsync(user2, "P@ssw0rd!2");
            }
        }
    }
}
