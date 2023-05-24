using MyEvernote.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entites
{
    [Table("Notes")]
    public class Note : MyEntityBase
    {
        [DisplayName("Not Başlığı"), Required, StringLength(60)]
        public string Title { get; set; }

        [DisplayName("Not Metni"), Required, StringLength(2000)]
        public string Text { get; set; }

        [DisplayName("Taslak")]
        public bool IsDraft { get; set; }  // Taslak mı

        [DisplayName("Beğenilme")]
        public int LikeCount { get; set; }

        [DisplayName("Kategori")]
        public int CategoryId { get; set; }
        public virtual EvernoteUser Owner { get; set; }  // Bir notun bir tane userı vardır o yüzden list kullanmıyoruz
        public virtual List<Comment> Comments { get; set; }   // Bir notun birden çok yorumları vardır
        public virtual Category Category { get; set; } // her notun bir kategorisi vardır
        public virtual List<Liked> Likes { get; set; } // Bir notun birden çok like vardır

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}