namespace Paycore_Net_Bootcamp_Hafta_3.Models
{
    public class Container
    {
        public virtual int Id { get; set; }
        public virtual string ContainerName { get; set; }
        //for determine location latitude and longitude
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }

        public virtual int VehicleId { get; set; }


    }

}
