using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.DocumentMessageModels
{
    public class DocumentMessageDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public string note { get; set; }
        public string sendFrom { get; set; }
    }
}
