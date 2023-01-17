using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinchuckLab.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Ammount { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsFinished { get; set; }
    }
}
