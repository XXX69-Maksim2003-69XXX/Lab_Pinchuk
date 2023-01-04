using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinchuckLab.Models
{
    public class Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceipmentId{ get; set; }
        public double Weight { get; set; }
        public double Size { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }

        public Client Client { get; set; }
        public Payment Payment { get; set; }
    }
}
