using MyEvernote.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccessLayer.EntityFramework
{
    // Singletorn Pattern : Bir nesnenin proje çalışırken sadece bir kopyasının olmasını istediğimizde 
    // (şu an databasecontext nesnemizde bunun olmasını istiyoruz) herkes o kopyayı kullansın o nesne 
    // her class için ayrı ayrı newlenip oluşmasın sadece bir kere oluşsun ve hepsinde aynısını kullanmak
    // istediğimizde kullanmamız gereken yapı.
    public class RepositoryBase
    {
        protected static DatabaseContext context;
        private static object _lockSync = new object();
        protected RepositoryBase()
        {
             CreateContext();
        }
        private static void CreateContext()
        {
            if (context == null)
            {
                lock (_lockSync) // lock 2 iş parçacığı aynı anda buraya girmiş olsa bile lockı gördüğünde biri o işi bitirmeden diğer işi yapmıyor olacak.
                {
                    if (context == null)
                    {
                        context = new DatabaseContext();
                    }
                }
            }
           


        }

    }
}
