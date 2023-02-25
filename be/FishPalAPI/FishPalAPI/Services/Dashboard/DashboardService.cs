using FishPalAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FishPalAPI.Services.Dashboard
{
    public class DashboardService
    {
        private ApplicationDbContext context;

        public DashboardService()
        {
            context = new ApplicationDbContext();
        }


        public int getDocumentAknowledgementsTrueCount(int profileId)
        {
                var items = context.DocumentMessages.Where(x => x.Recipient.Id == profileId && x.Acknowledged == true).Count();
                return items;           
        }

        public int getDocumentAknowledgementsFalseCount(int profileId)
        {
            var items = context.DocumentMessages.Where(x => x.Recipient.Id == profileId && x.Acknowledged == false).Count();
            return items;
        }

        public int getEnrolledCoursesCount(int profileId)
        {
            var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
            var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
            var count = context.UserCourses.Include(a => a.user).Include(a => a.course)
                .Where(a => a.user.Id == user.Id).Count();
            return count;
        }

        public int getEnrolledCoursesApprovedCount(int profileId)
        {
                var userProfile = context.UserProfiles.Where(a => a.Id == profileId).FirstOrDefault();
                var user = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(userProfile)).FirstOrDefault();
                var count = context.UserCourses.Include(a => a.user).Include(a => a.course)
                    .Where(a => a.user.Id == user.Id && a.Approved == true).Count();
                return count;           
        }
    }
}
