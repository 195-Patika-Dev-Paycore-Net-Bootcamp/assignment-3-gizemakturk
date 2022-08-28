using  Paycore_Net_Bootcamp_Hafta_3.Models;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Paycore_Net_Bootcamp_Hafta_3.Mappings
{
    public class VehicleMap:ClassMapping<Vehicle>
    {
        public VehicleMap()
        {
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("id");
                x.UnsavedValue(0);
               x.Generator(Generators.Increment);
               
            });

            Property(b => b.VehicleName, x =>
            {
                x.Length(50);
                x.Column("vehicleName");
                x.Type(NHibernateUtil.String);
                x.NotNullable(false);
            });
            Property(b => b.VehiclePlate, x =>
            {
                x.Length(14);
                x.Column("vehiclePlate");
                x.Type(NHibernateUtil.String);
                x.NotNullable(false);
            });
            Table("vehicle");
        }

    }
}
