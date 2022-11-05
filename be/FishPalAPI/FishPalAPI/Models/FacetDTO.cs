using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models
{
    public class FacetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Federation { get; set; }
        public List<ProvinceDTO> provinces { get; set; }
    }
}
