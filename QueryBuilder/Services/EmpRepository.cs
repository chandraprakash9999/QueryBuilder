using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace QueryBuilder.Services
{
    public class EmpRepository<T> : IEmpRepository<T> where T:class
    {
       private DbContext _dbcontext;
        private DbSet<T> dbEntity;


        public EmpRepository()
        {
            _dbcontext = new EmployeeDBEntities();
            dbEntity = _dbcontext.Set<T>();

        }

        public void deleteModel(int id)
        {

            T model = dbEntity.Find(id);
            dbEntity.Remove(model);
        }

        public IEnumerable<T> getModel()
        {
           return dbEntity.ToList();
        }

        public T getModelByID(int id)
        {
            return dbEntity.Find(id);
        }

        public void insertModel(T model)
        {
            dbEntity.Add(model);
        }

        public void updateModel(T model)
        {
            _dbcontext.Entry(model).State = EntityState.Modified;
        }

        public void save()
        {
            _dbcontext.SaveChanges();
        }

       
    }
}