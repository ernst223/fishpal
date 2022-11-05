using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Models.DocumentMessageModels
{
    public class UploadDocumentMessageDTO
    {
        public int documentId { get; set; }
        public string title { get; set; }
        public string note { get; set; }
    }
}
