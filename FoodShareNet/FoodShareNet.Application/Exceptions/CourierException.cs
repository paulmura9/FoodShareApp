using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Exceptions
{
    internal class CourierException:Exception
    {
        public CourierException(string messege) : base(messege) { }
    }
}
