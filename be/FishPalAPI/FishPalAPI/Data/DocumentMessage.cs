using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FishPalAPI.Data
{
    public class DocumentMessage
    {
        [Key]
        public int Id { get; set; }
        public string SendTo { get; set; }
        public string SendFrom { get; set; }
        public Document DocumentSend { get; set; }
    }
}
