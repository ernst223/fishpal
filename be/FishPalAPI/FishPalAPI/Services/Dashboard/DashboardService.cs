using FishPalAPI.Data;

namespace FishPalAPI.Services.Dashboard
{
    public class DashboardService
    {
        private ApplicationDbContext context;

        public DashboardService()
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
    }
}
