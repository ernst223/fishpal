using FishPalAPI.Data;
using FishPalAPI.Models.UserInformation.Training;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FishPalAPI.Services.Member_Information.Training
{
    public class TrainingService
    {
        private ApplicationDbContext context;
        private UserService userService;
        public TrainingService()
        {
            context = new ApplicationDbContext();
            userService = new UserService();
        }

        public TrainingDTO getTrainingInformation(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.training)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();

            if (currentProfile.userInformation == null)
            {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.training)
                .Where(a => a.Id == profileId).FirstOrDefault();

            }

            var currentTrainingInformation = context.Training.Where(a => a.Id == currentProfile.userInformation.training.Id).FirstOrDefault();

            return new TrainingDTO()
            {
                Id = currentTrainingInformation.Id,
                ManagerYearCompleted = currentTrainingInformation.ManagerYearCompleted,
                ManagerPointsReceived = currentTrainingInformation.ManagerPointsReceived,
                CoachLvl1YearCompleted = currentTrainingInformation.CoachLvl1YearCompleted,
                CoachLvl1PointsReceived = currentTrainingInformation.CoachLvl1PointsReceived,
                CoachLvl2YearCompleted = currentTrainingInformation.CoachLvl2YearCompleted,
                CoachLvl2PointsReceived = currentTrainingInformation.CoachLvl2PointsReceived,
                CaptainYearCompleted = currentTrainingInformation.CaptainYearCompleted,
                CaptainPointsReceived = currentTrainingInformation.CaptainPointsReceived,
                AdminYearCompleted = currentTrainingInformation.AdminYearCompleted,
                AdminPointsReceived = currentTrainingInformation.AdminPointsReceived
        };
        }

        public void updateTrainingInformation(TrainingDTO trainingInformation, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.training)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentTrainingInformation = context.Training.Where(a => a.Id == currentProfile.userInformation.training.Id).FirstOrDefault();

            currentTrainingInformation.ManagerYearCompleted = trainingInformation.ManagerYearCompleted;
            currentTrainingInformation.ManagerPointsReceived = trainingInformation.ManagerPointsReceived;
            currentTrainingInformation.CoachLvl1YearCompleted = trainingInformation.CoachLvl1YearCompleted;
            currentTrainingInformation.CoachLvl1PointsReceived = trainingInformation.CoachLvl1PointsReceived;
            currentTrainingInformation.CoachLvl2YearCompleted = trainingInformation.CoachLvl2YearCompleted;
            currentTrainingInformation.CoachLvl2PointsReceived = trainingInformation.CoachLvl2PointsReceived;
            currentTrainingInformation.CaptainYearCompleted = trainingInformation.CaptainYearCompleted;
            currentTrainingInformation.CaptainPointsReceived = trainingInformation.CaptainPointsReceived;
            currentTrainingInformation.AdminYearCompleted = trainingInformation.AdminYearCompleted;
            currentTrainingInformation.AdminPointsReceived = trainingInformation.AdminPointsReceived;

        context.SaveChanges();
        }
    }
}
