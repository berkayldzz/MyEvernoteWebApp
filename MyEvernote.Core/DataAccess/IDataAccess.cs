using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Core.DataAccess
{
    public interface IDataAccess<T>
    {
        List<T> List(); // Tüm tabloyu listeleyen
        List<T> List(Expression<Func<T, bool>> where); // İsteğime göre listeleyen

        IQueryable<T> ListQueryable();
        int Insert(T obj);
        int Update(T obj);
        int Delete(T obj);
        int Save();
        T Find(Expression<Func<T, bool>> where); // Lİste olarak değil tek bir değer getirme.(bulamazsa null döner geriye)


    }
}
