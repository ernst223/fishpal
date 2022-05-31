using System.ComponentModel.DataAnnotations;

namespace FishPalAPI.Data
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public double Amount { get; set; }
        public string Size { get; set; }
    }
}
