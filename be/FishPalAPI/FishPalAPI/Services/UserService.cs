using FishPalAPI.Data;
using FishPalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using PostmarkDotNet;
using System.Threading.Tasks;
using FishPalAPI.Models.MobileAppModels;

namespace FishPalAPI.Services
{
    public class UserService
    {
        private ApplicationDbContext context;

        public UserService()
        {
            context = new ApplicationDbContext();
        }

        public async Task<object> allUserInfo(string username, int? federation)
        {
            var query = context.Users;
            if (federation != null)
            {
                query.Include(a => a.role).Include(a => a.federations).Include(a => a.clubs).Where(o => o.UserName == username && o.federations.Any(x => x.Id == federation));
            }
            else {
                query.Include(a => a.role).Include(a => a.federations).Include(a => a.clubs).Where(o => o.UserName == username);
            }

            var userRecord = query.FirstOrDefaultAsync();

            if (userRecord != null)
            {
                return userRecord;//changes needs to be made here after merge with ernst
            }
            return userRecord;//changes needs to be made here after merge with ernst
        }
        
        public string getUserRole(User user)
        {
            var tempUser = context.Users.Include(a => a.role).Where(o => o.Id == user.Id).FirstOrDefault();
            if (tempUser != null)
            {
                return tempUser.role.Description;
            }
            return "A3";
        }

        public bool addUserClubs(string userId, List<int> clubs)
        {
            var user = context.Users.Include(a => a.clubs).Include(a => a.role).Where(a => a.Id == userId).FirstOrDefault();
            addUserDefaultRole(user);
            if (user != null)
            {
                foreach(var entry in clubs)
                {
                    user.clubs.Add(context.Clubs.Where(a => a.Id == entry).First());
                }
                context.SaveChanges();
            }
            return true;
        }

        public void addUserDefaultRole(User user)
        {
            var role = context.Role.Where(a => a.Description == "A3").FirstOrDefault();
            if(role == null)
            {
                context.Role.Add(new Role()
                {
                    Description = "A3"
                });
                context.SaveChanges();
                var newRole = context.Role.Where(a => a.Description == "A3").FirstOrDefault();
                user.role = newRole;
            } else
            {
                user.role = role;
            }
            context.SaveChanges();
        }

        public bool removeUserClubs(string userId)
        {
            var user = context.Users.Include(a => a.clubs).Where(a => a.Id == userId).FirstOrDefault();
            user.clubs = null;
            context.SaveChanges();
            return true;
        }

        public async Task<bool> sendConfirmEmailAsync(User user)
        {
            var message = new TemplatedPostmarkMessage
            {
                From = "admin@fishpal.co.za",
                To = user.UserName,
                TemplateAlias = "confirmemail",
                TemplateModel = new Dictionary<string, object> {
                    { "name", user.Name + " " + user.Surname },
                    { "action_url", user.Id },
                    { "username", user.UserName }
                  },
            };

            var client = new PostmarkClient("fc1530d0-ed32-488f-8f40-33fb54be4801");

            var response = await client.SendMessageAsync(message);

            if (response.Status != PostmarkStatus.Success)
            {
                Console.WriteLine("Response was: " + response.Message);
            }
            return true;
        }

        public async Task<bool> sendResetPasswordEmailAsync(ResetPasswordDTO resetModel, string name)
        {
            var message = new TemplatedPostmarkMessage
            {
                From = "admin@fishpal.co.za",
                To = resetModel.userName,
                TemplateAlias = "password-reset",
                TemplateModel = new Dictionary<string, object> {
                        { "name", name },
                        { "token", resetModel.token },
                        { "username", resetModel.userName },
                },
            };

            var client = new PostmarkClient("fc1530d0-ed32-488f-8f40-33fb54be4801");

            var response = await client.SendMessageAsync(message);

            if (response.Status != PostmarkStatus.Success)
            {
                Console.WriteLine("Response was: " + response.Message);
            }
            return true;
        }
    }
}
