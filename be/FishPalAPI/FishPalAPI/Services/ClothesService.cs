using FishPalAPI.Data;
using System;

namespace FishPalAPI.Services
{
    public class ClothesService
    {
        private ApplicationDbContext context;

        public ClothesService()
        {
            context = new ApplicationDbContext();
        }
        
        public bool insertOrderItems(OrderItems T)
        {
            try
            {
                context.OrderItems.Add(T);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
