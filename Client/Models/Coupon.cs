using System;
using System.Collections.Generic;

namespace Client.Models
{
    public partial class Coupon
    {
        public int Id { get; set; }
        public int Discount { get; set; }
        public string Code { get; set; } = null!;
        public int Limit { get; set; }
    }
}
