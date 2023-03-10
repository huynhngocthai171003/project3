using System;
using System.Collections.Generic;

namespace Client.Models
{
    public partial class Staff
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Role { get; set; }
    }
}
