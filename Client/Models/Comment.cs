using System;
using System.Collections.Generic;

namespace Client.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Rate { get; set; }
        public string Content { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
