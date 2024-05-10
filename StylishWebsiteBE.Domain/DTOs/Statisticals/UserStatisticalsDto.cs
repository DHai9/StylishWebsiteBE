using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StylishWebsiteBE.Domain.DTOs.Statisticals {
    public class UserStatisticalsDto {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public decimal CountOrder { get; set; }
        public decimal SumMoney { get; set; }
        public DateTimeOffset MinDate { get; set; }
        public DateTimeOffset MaxDate { get; set; }
    }
}
