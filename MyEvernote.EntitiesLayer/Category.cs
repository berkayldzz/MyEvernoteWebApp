﻿using MyEvernote.Entites;
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
    [Table("Categories")]
    public class Category : MyEntityBase
    {
        [DisplayName("Kategori"), /*Required(ErrorMessage = "{0] alanı gereklidir."),*/ StringLength(50,ErrorMessage ="{0} alanı max. {1} karakter içermelidir.")]
        public string Title { get; set; }

        [DisplayName("Açıklama"), StringLength(150, ErrorMessage = "{0} alanı max. {1} karakter içermelidir.")]
        public string Description { get; set; }

        // Bir kategorinin notları vardır (ilişkili tablo kurucaz)
        // Birden çok olduğu için list türünde tutuyoruz

        public virtual List<Note> Notes { get; set; }  // İlişkisel bir tablo old için virtual olarak tanımlıyorum

        public Category()
        {
            Notes = new List<Note>();
        }
    }
}