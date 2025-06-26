using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steel.Models
{
    public class Message
    {
        public string? Content { get; set; }
        public bool IsUser { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}

