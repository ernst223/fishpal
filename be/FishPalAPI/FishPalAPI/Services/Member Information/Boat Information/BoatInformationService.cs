using FishPalAPI.Data;
using FishPalAPI.Models.UserInformation.Boat_Information;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FishPalAPI.Services.Member_Information.Boat_Information
{
    public class BoatInformationService
    {
        private ApplicationDbContext context;
        private UserService userService;
        public BoatInformationService()
        {
            context = new ApplicationDbContext();
            userService = new UserService();
        }

        public BoatInformationDTO getBoatInformation(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.boatInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();

            if (currentProfile.userInformation == null)
            {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.boatInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();

            }

            var currentBoatInformation = context.BoatInformation.Where(a => a.Id == currentProfile.userInformation.boatInformation.Id).FirstOrDefault();

            return new BoatInformationDTO()
            {
                Id = currentBoatInformation.Id,
                BoatOwner = currentBoatInformation.BoatOwner,
                BoatNumber = currentBoatInformation.BoatNumber,
                HullType = currentBoatInformation.HullType,
                HullColour = currentBoatInformation.HullColour,
                MotorMake = currentBoatInformation.MotorMake,
                HorsePower = currentBoatInformation.HorsePower,
                TowVehicleRegistrationNumber = currentBoatInformation.TowVehicleRegistrationNumber,
                TrailerRegistrationNumber = currentBoatInformation.TrailerRegistrationNumber,
                CofNumber = currentBoatInformation.CofNumber,
                CofExpiryDate = currentBoatInformation.CofExpiryDate
            };
        }

        public void updateBoatInformation(BoatInformationDTO boatInformation, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.boatInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentBoatInformation = context.BoatInformation.Where(a => a.Id == currentProfile.userInformation.boatInformation.Id).FirstOrDefault();

                currentBoatInformation.BoatOwner = boatInformation.BoatOwner;
                currentBoatInformation.BoatNumber = boatInformation.BoatNumber;
                currentBoatInformation.HullType = boatInformation.HullType;
                currentBoatInformation.HullColour = boatInformation.HullColour;
                currentBoatInformation.MotorMake = boatInformation.MotorMake;
                currentBoatInformation.HorsePower = boatInformation.HorsePower;
                currentBoatInformation.TowVehicleRegistrationNumber = boatInformation.TowVehicleRegistrationNumber;
                currentBoatInformation.TrailerRegistrationNumber = boatInformation.TrailerRegistrationNumber;
                currentBoatInformation.CofNumber = boatInformation.CofNumber;
                currentBoatInformation.CofExpiryDate = boatInformation.CofExpiryDate;
            context.SaveChanges();
        }
    }
}
