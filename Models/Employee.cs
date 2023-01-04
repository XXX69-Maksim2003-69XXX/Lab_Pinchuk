using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinchuckLab.Models
{
    public class Employee : Person 
    {
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
    }
}
