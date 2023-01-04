using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinchuckLab.Models
{
    public class MailBranch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Parcel> Parcels { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
