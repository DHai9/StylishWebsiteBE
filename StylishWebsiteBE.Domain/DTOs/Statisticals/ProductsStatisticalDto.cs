using StylishWebsiteBE.Domain.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StylishWebsiteBE.Domain.DTOs.Statisticals {
    public class ProductsStatisticalDto {
        // product variant
        public Guid Id { get; set; }
        public string SkuId { get; set; }
        public string Name { get; set; }
        public int TotalSale { get; set; }
        public long Revenue { get; set; }
        public float Rate { get; set; }
        public string DateTime { get; set; }
        public List<OrderDetailStatisticalDto> OrderDetails { get; set; }
    }
}
