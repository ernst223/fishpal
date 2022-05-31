using FishPalAPI.Data;
using FishPalAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public List<FacetDTO> getAllFacetsForRegistration()
        {
            List<Facet> facets = context.Facets.Include(a => a.Provinces).ToList();
            List<FacetDTO> result = new List<FacetDTO>();
            foreach (var entry in facets)
            {
                result.Add(new FacetDTO()
                {
                    Id = entry.Id,
                    Name = entry.Name,
                    provinces = getProvincesDTO(entry.Provinces)
                });
            }
            return result;
        }
        public List<ProvinceDTO> getProvincesDTO(List<Province> provinces)
        {
            List<ProvinceDTO> result = new List<ProvinceDTO>();
            foreach(var entry in provinces)
            {
                result.Add(new ProvinceDTO()
                {
                    Id = entry.Id,
                    Name = entry.Name
                });
            }
            return result;
        }

        public List<ClubDTO> getFacetClubsInProvince(int facetId, int provinceId)
        {
            List<Club> clubs = context.Clubs.Include(a => a.Province).Include(a => a.Facet)
                .Where(p => p.Province.Id == provinceId && p.Facet.Id == facetId).ToList();
            List<ClubDTO> result = new List<ClubDTO>();
            foreach (var entry in clubs)
            {
                result.Add(new ClubDTO()
                {
                    Id = entry.Id,
                    Name = entry.Name,
                    Province = entry.Province.Name,
                    Facet = entry.Facet.Name
                });
            }
            return result;
        }
    }
}
