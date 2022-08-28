using Paycore_Net_Bootcamp_Hafta_3.Models;
using System.Linq;

namespace Paycore_Net_Bootcamp_Hafta_3.Context
{
    public interface IMapperSession
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void CloseTransaction();
        //CRUD operations for vehicle
        void Save(Vehicle entity);
        void Update(Vehicle entity);
        void Delete(Vehicle entity);

        //CRUD operations for container

        void Save(Container entity);
        void Update(Container entity);
        void Delete(Container entity);


        IQueryable<Vehicle> Vehicles { get; }
        IQueryable<Container> Containers { get; }

       
    }
}
