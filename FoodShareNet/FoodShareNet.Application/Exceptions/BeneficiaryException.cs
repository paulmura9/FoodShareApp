using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareNet.Application.Exceptions
{
    internal class BeneficiaryException:Exception
    {
        public BeneficiaryException(string messege) : base(messege) { }
    }
}
