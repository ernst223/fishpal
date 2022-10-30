using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.DocumentMessageModels
{
    public class UploadDocumentMessageDTO
    {
        public IFormFile data { get; set; }
        public string userName { get; set; }
        public int sentTo { get; set; }
        public string title { get; set; }
        public string note { get; set; }
    }
}
