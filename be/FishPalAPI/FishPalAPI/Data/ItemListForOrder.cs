using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishPalAPI.Data
{
    public class ItemListForOrder
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Team { get; set; }

        public int Quantity { get; set; }

        public string Size { get; set; }

        public double TotalAmount { get; set; }

        [ForeignKey("Orders")]
        public int OrdersFKId { get; set; }
        public virtual Orders Orders { get; set; }
    }
}
