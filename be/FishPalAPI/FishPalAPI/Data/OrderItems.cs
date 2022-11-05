using System.ComponentModel.DataAnnotations;

namespace FishPalAPI.Data
{
    public class OrderItems
    {
        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; }
        public string ItemName { get; set; }

        public double Amount { get; set; }
    }
}
