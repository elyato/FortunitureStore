using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.shared
{
    public class Order
    {
        public int Id { get; set; }

        public int OrderNumber { get; set; }
        public int ClientId { get; set; }

        public DateTime orderDate { get; set; }
        public DateTime DeliveryOrder { get; set; }

        public List<OrderDetail> OrderDetails { get ; set; }    
    }
}
