using FishPalAPI.Data;
using FishPalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Services
{
    public class ClubService
    {
        private ApplicationDbContext context;

        public ClubService()
        {
            context = new ApplicationDbContext();
        }

        public List<ProvinceDTO> getAllProvinces()
        {
            List<Province> provinces = context.Provinces.ToList();
            List<ProvinceDTO> result = new List<ProvinceDTO>();
            foreach(var entry in provinces)
            {
                result.Add(new ProvinceDTO()
                {
                    Id = entry.Id,
                    Description = entry.Description
                });
            }
            return result;
        }

        public List<ClubDTO> getAllClubs()
        {
            List<Club> clubs = context.Clubs.ToList();
            List<ClubDTO> result = new List<ClubDTO>();
            foreach(var entry in clubs)
            {
                result.Add(new ClubDTO()
                {
                    Id = entry.Id,
                    Description = entry.Description,
                    Province = entry.Province.Description
                });
            }
            return result;
        }

        public List<ClubDTO> getClubsInProvince(int provinceId)
        {
            List<Club> clubs = context.Clubs.Where(p => p.Province.Id == provinceId).ToList();
            List<ClubDTO> result = new List<ClubDTO>();
            foreach (var entry in clubs)
            {
                result.Add(new ClubDTO()
                {
                    Id = entry.Id,
                    Description = entry.Description,
                    Province = entry.Province.Description
                });
            }
            return result;
        }
    }
}
