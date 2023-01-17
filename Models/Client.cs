using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinchuckLab.Models
{
    public class Client : Person
    {
        [Required][MaxLength(13)]public string PhoneNumber { get; set; }
        [Required]public string Passport { get; set; }

        // navigation property 
        public ICollection<Parcel> Parcels { get; set; } = null;
        
        public Payment? Payment { get; set; } = null;
    }}
