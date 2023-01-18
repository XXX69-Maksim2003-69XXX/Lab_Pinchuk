using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string? Passport { get; set; } = null;

        public Client? Client { get; set; } = null;
    }
}
