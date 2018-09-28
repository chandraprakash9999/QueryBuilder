using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Services
{
    interface IEmpRepository<T> where T:class
    {

        IEnumerable<T> getModel();
        T getModelByID(int id);
        void insertModel(T model);
        void deleteModel(int id);
        void updateModel(T model);
        void save();


    }
}
