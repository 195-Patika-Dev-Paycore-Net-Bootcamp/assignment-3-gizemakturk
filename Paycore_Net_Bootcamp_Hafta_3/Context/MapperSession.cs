using Paycore_Net_Bootcamp_Hafta_3.Models;
using NHibernate;
using System.Linq;
using System.Threading.Tasks;

namespace Paycore_Net_Bootcamp_Hafta_3.Context
{

    public class MapperSession<T> : IMapperSession<T>
    {private readonly ISession session;
        private ITransaction transaction;
        public MapperSession(ISession session)
        {
            this.session = session;
        }
        
      


        public void BeginTransaction()
        {
            transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Rollback()
        {
            transaction.Rollback();
        }

        public void CloseTransaction()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        //public void Save(Vehicle entity)
        //{
        //    session.Save(entity);
        //}
        //public void Update(Vehicle entity)
        //{
        //    session.Update(entity);
        //}
        //public void Delete(Vehicle entity)
        //{
        //    session.Delete(entity);
        //}

        //public void Save(Container entity)
        //{
        //    session.Save(entity);
        //}

        //public void Update(Container entity)
        //{
        //    session.Update(entity);
        //}

        //public void Delete(Container entity)
        //{
        //    session.Delete(entity);
        //}

        public void Save(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        //public IQueryable<Vehicle> Vehicles => session.Query<Vehicle>();

        //public IQueryable<Container> Containers => session.Query<Container>();

        public IQueryable<T> Entities => session.Query<T>();
    }
}