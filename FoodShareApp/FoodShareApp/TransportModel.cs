using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShareApp
{
    public class TransportModel
    {
        public string id { get; set; }
        public string oras { get; set; }
        public string expeditor { get; set; }
        public string adresaExp { get; set; }
        public string destinatar { get; set; }
        public string adresaDest { get; set; }

        public TransportModel(bool autogenerate) {
            if (autogenerate)
            {
                id = "8";
                oras = "default";
                expeditor = "default";
                adresaExp = "default";
                destinatar = "default";
                adresaDest = "default";
            }
        }
   
    }
}
