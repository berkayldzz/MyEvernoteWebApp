using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entites;

namespace MyEvernote.Entites
{

    public class MyEntityBase
    {
        // Bu sınıf diğer bütün sınıflarda olacak propertyleri barındırdığı için diğer sınıflar buradan miras alacak ve her sınıfa tekrar tekrar yazmayacağım.
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Bir kategoriyi veya başka bir şeyi adminler ekleyip düzenleyebilicek.

        [DisplayName("Oluşturma Tarihi"), Required]
        public DateTime CreatedOn { get; set; } // Oluşturulduğu tarih

        [DisplayName("Güncelleme Tarihi"), Required]
        public DateTime ModifiedOn { get; set; } // Değiştirildiği tarih

        [DisplayName("Güncelleyen"), Required, StringLength(30)]
        public string ModifiedUsername { get; set; } // Kim tarafından değiştirildi

    }
}