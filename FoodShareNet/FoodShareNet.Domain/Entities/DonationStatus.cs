using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Domain.Entities
{
    public class DonationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } // Available, Reserved
    }

}
