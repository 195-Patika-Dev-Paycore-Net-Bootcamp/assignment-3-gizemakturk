using System.Collections.Generic;

namespace Paycore_Net_Bootcamp_Hafta_3.Models
{
    public class Vehicle
    {
        public virtual  int Id { get; set; }
        public virtual string VehicleName { get; set; }
        public virtual string VehiclePlate { get; set; }
        //public virtual List<Container> Containers { get; set; }

        //public Vehicle()
        //{
        //    Containers = new List<Container>();
        //}

    }
}
