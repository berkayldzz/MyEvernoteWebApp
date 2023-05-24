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
    [Table("EvernoteUsers")]
    public class EvernoteUser : MyEntityBase
    {
        // Admin kullanıcıyı düzenleyebilir veya kullanıcı kendi adını vs düzenleyebilir o yüzden myentitybase sınıfındakileri burda da kullanıyoruz.

        [DisplayName("İsim"),StringLength(25,ErrorMessage ="{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyisim"), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("E-Posta"), Required(ErrorMessage = "{0} alanı gereklidir."), StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Şifre"), Required(ErrorMessage ="{0} alanı gereklidir."), StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [StringLength(30),ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [Required, ScaffoldColumn(false)]
        public Guid ActivatedGuid { get; set; }

        [DisplayName("Is Admin")]
        public bool IsAdmin { get; set; }

        // Bir kullanıcının birden çoku notu olucak (ilişkili tablo)
        public virtual List<Note> Notes { get; set; } // Kullanıcının kendi notları
        public virtual List<Comment> Comments { get; set; } // Kullanıcının yorumları
        public virtual List<Liked> Likes { get; set; } // Bir kullanıcının birden çok like vardır

    }
}