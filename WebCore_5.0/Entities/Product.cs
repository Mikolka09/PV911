using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore_5._0.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Отношение one-many
        public int VendorId { get; set; }//Это поле не обязательно
        public Vendor Vendor { get; set; }
    }
}
