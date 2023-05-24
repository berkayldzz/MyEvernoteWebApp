using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        //private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        //private Repository<Category> repo_category = new Repository<Category>();
        //private Repository<Comment> repo_comment = new Repository<Comment>();
        //private Repository<Note> repo_note = new Repository<Note>();
        //public Test()
        //{

        //    List<Category> categories = repo_category.List();
        //}
        //public void InsertTest()
        //{

        //    int result = repo_user.Insert(new EvernoteUser()
        //    {
        //        Name = "aaa",
        //        Surname = "bbb",
        //        Email = "yildizberkay359@gmail.com",
        //        ActivatedGuid = Guid.NewGuid(),
        //        IsActive = true,
        //        IsAdmin = true,
        //        Username = "aabb",
        //        Password = "111",
        //        CreatedOn = DateTime.Now,
        //        ModifiedOn = DateTime.Now.AddMinutes(5),
        //        ModifiedUsername = "aabb"
        //    });
        //}
        //public void UpdateTest()
        //{
        //    // Update edilcek kullanıcıyı bulalım
        //    EvernoteUser user = repo_user.Find(x => x.Username == "aabb");

        //    if (user != null)
        //    {
        //        user.Username = "xxx";

        //        int result = repo_user.Update(user);
        //    }
        //}
        //public void DeleteTest() // Kendi içinde save metodu var.
        //{
        //    EvernoteUser user = repo_user.Find(x => x.Username == "xxx");
        //    if (user != null)
        //    {
        //        int result = repo_user.Delete(user);
        //    }


        //}

        //public void CommentTest()
        //{
        //    EvernoteUser user = repo_user.Find(x => x.Id == 1);
        //    Note note = repo_note.Find(x => x.Id == 3);

        //    Comment comment = new Comment()
        //    {
        //        Text = "Bu bir testtir",
        //        CreatedOn = DateTime.Now,
        //        ModifiedOn = DateTime.Now,
        //        Note = note,
        //        Owner = user
        //    };
        //    repo_comment.Insert(comment);
        //}
    }
}
