using FishPalAPI.Data;
using FishPalAPI.Models.UserInformation.Geo_Province_Information;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FishPalAPI.Services.Member_Information.Geo_Province_Information
{
    public class GeoProvinceService
    {
        private ApplicationDbContext context;
        private UserService userService;
        public GeoProvinceService()
        {
            context = new ApplicationDbContext();
            userService = new UserService();
        }

        public GeoProvinceInformationDTO getGeoProvinceInformation(int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.geoProvinceInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentUser = context.Users.Include(a => a.profiles).Where(a => a.profiles.Contains(currentProfile)).FirstOrDefault();

            if (currentProfile.userInformation == null) {
                currentProfile.userInformation = userService.getDefaultUserInformation();
                context.SaveChanges();

                // Refetching
                currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.geoProvinceInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();

            }

            var currentGeoProvinceInformation = context.GeoProvinecInformation.Where(a => a.Id == currentProfile.userInformation.geoProvinceInformation.Id).FirstOrDefault();

            return new GeoProvinceInformationDTO()
            {
                Id = currentGeoProvinceInformation.Id,
                GeoProvince = currentGeoProvinceInformation.GeoProvince,
                ProvincialSasaccManagement = currentGeoProvinceInformation.ProvincialSasaccManagement,
                Position = currentGeoProvinceInformation.Position,
                DistrictMunicipality = currentGeoProvinceInformation.DistrictMunicipality
            };
        }

        public void updateGeoProvinceInformation(GeoProvinceInformationDTO geoProvinceInformation, int profileId)
        {
            var currentProfile = context.UserProfiles.Include(a => a.userInformation).ThenInclude(a => a.geoProvinceInformation)
                .Where(a => a.Id == profileId).FirstOrDefault();
            var currentGeoProvinceInformation = context.GeoProvinecInformation.Where(a => a.Id == currentProfile.userInformation.geoProvinceInformation.Id).FirstOrDefault();

            currentGeoProvinceInformation.GeoProvince = geoProvinceInformation.GeoProvince;
            currentGeoProvinceInformation.ProvincialSasaccManagement = geoProvinceInformation.ProvincialSasaccManagement;
            currentGeoProvinceInformation.Position = geoProvinceInformation.Position;
            currentGeoProvinceInformation.DistrictMunicipality = geoProvinceInformation.DistrictMunicipality;
            context.SaveChanges();
        }
    }
}
