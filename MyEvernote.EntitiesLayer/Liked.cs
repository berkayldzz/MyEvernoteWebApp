using MyEvernote.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entites
{
    [Table("Likes")]
    public class Liked
    {
        // Kullanıcı birden fazla notu likelayabilir , bir not birden fazla kişi tarafından like alabilir. Yani çoka çok ilişki var o yüzden bunları ayrı bir like tablosunda tutuyoruz.

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Note Note { get; set; }
        public virtual EvernoteUser LikedUser { get; set; }

    }
}
