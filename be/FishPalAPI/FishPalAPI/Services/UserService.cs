using FishPalAPI.Data;
using FishPalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using PostmarkDotNet;
using System.Threading.Tasks;

namespace FishPalAPI.Services
{
    public class UserService
    {
        private ApplicationDbContext context;

        public UserService()
        {
            context = new ApplicationDbContext();
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

            var client = new PostmarkClient("c6f92306-e8f6-4d85-be35-1033ec1273b7");

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

            var client = new PostmarkClient("c6f92306-e8f6-4d85-be35-1033ec1273b7");

            var response = await client.SendMessageAsync(message);

            if (response.Status != PostmarkStatus.Success)
            {
                Console.WriteLine("Response was: " + response.Message);
            }
            return true;
        }
    }
}
