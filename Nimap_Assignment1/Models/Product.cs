using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManojHiray_Nimap.Models
{
    public class Product
    {
     
        public int ProductId { get; set; }

        public string ProductName { get; set; }

       
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
        

    }

    public class PaginationRecords
    {
        public IEnumerable<Product> Records { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int Count { get; set; }
    }
}
