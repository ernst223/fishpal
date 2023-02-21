using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FishPalAPI.Data
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public int Username { get; set; }

        public string OrderDate { get; set; }

        public List<ItemListForOrder> ItemsListForOrder { get; set; }

        public double TotalAmount { get; set; }

        public int Status { get; set; }

    }
}
