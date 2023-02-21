using FishPalAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FishPalAPI.Services
{
    public class ClothesService
    {
        private ApplicationDbContext context;

        public ClothesService()
        {
            context = new ApplicationDbContext();
        }
        

        public List<OrderItems> getOrderItems()
        {
            try
            {
                var items = context.OrderItems.ToList();
                return items;
            }
            catch (Exception e)
            {
                return null;
            }
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
        public bool updateOrderItem(OrderItems T)
        {
            try
            {
                context.OrderItems.Update(T);
                context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool deleteOrderItem(int orderItemId)
        {
            try
            {
                var recordToDelete = context.OrderItems.Where(y => y.Id == orderItemId).FirstOrDefault();
                context.OrderItems.Remove(recordToDelete);
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
