using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishPalAPI.Data.Member_Information.Geo_Province_Information
{
    public class GeoProvinceInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GeoProvince { get; set; }
        public string ProvincialSasaccManagement { get; set; }
        public string Position { get; set; }
    }
}
