﻿using MyEvernote.BusinessLayer.Abstract;
using MyEvernote.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {
        // Kategori silindiğinde iliişkili verilerin de silinmesi için ;

        //public override  int Delete (Category category)
        //{
        //    NoteManager noteManager = new NoteManager();
        //    LikedManager likedManager = new LikedManager();
        //    CommentManager commentManager = new CommentManager();

        //    // Kategori ile ilişkili notların silinmesi gerekiyor.
        //    foreach (Note note in category.Notes.ToList())
        //    {
        //        // Note ile ilişkili likeların silinmesi

        //        foreach (Liked like in note.Likes.ToList())
        //        {
        //            likedManager.Delete(like);
        //        }

        //        // Not ile ilişkili commentlerin silinmesi

        //        foreach (Comment comment in note.Comments.ToList())
        //        {
        //            commentManager.Delete(comment);
        //        }


        //        noteManager.Delete(note);
        //    }

        //    return base.Delete(category);
        //}

    }
}
