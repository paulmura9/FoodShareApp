using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Exceptions
{
    internal class ProductException:Exception
    {
        public ProductException(string messege) : base(messege) { }

    }
}
