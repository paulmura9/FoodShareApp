using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Domain.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } // Unconfirmed, Confirmed, In Delivery, Delivered

        public static explicit operator int(OrderStatus v)
        {
            throw new NotImplementedException();
        }
    }
}
