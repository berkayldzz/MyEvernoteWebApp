using MyEvernote.Entities.Messages;
using MyEvernote.EntitiesLayer.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyEvernote.BusinessLayer.Results
{
    public class BusinessLayerResult<T> where T: class
    {
        public List<ErrorMessageObj> Errors { get; set; } // Hata mesajlarını burada tutacağız
        public T Result { get; set; }  // İşlem başarılıysa sonucu bu propertyde vereceğiz.

        public BusinessLayerResult()  // Error listesi null gelirse sıkıntı yaşamayalım diye
        {
            Errors = new List<ErrorMessageObj>();
        }

        public void AddError(ErrorMessageCode code,string message)
        {
            Errors.Add(new ErrorMessageObj() {Code=code,Message=message });
        }
    }
}
