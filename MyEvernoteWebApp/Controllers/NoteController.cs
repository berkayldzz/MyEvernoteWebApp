﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entites;
using MyEvernoteWebApp.Filter;
using MyEvernoteWebApp.Models;

namespace MyEvernoteWebApp.Controllers
{
    [Exc]
    public class NoteController : Controller
    {
        NoteManager noteManager = new NoteManager();
        CategoryManager categoryManager = new CategoryManager();
        LikedManager likedManager = new LikedManager();

        [Auth]
        public ActionResult Index()
        {
            var notes = noteManager.ListQueryable().Include("Category").Include("Owner").Where(
                x => x.Owner.Id == CurrentSession.User.Id).OrderByDescending(
                x => x.ModifiedOn);
            return View(notes.ToList());
        }

        [Auth]
        public ActionResult MyLikedNotes()
        {
           // Login olan kullanıcıya ait likeları getir 
            
            var notes = likedManager.ListQueryable().Include("LikedUser").Include("Note").Where(
                x => x.LikedUser.Id == CurrentSession.User.Id).Select(
                x => x.Note).Include("Category").Include("Owner").OrderByDescending(
                x => x.ModifiedOn);

            return View("Index", notes.ToList());
        }

        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [Auth]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title");
            return View();
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                note.Owner = CurrentSession.User;
                noteManager.Insert(note);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }
        
        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Note db_note = noteManager.Find(x => x.Id == note.Id);
                db_note.IsDraft = note.IsDraft;
                db_note.CategoryId = note.CategoryId;
                db_note.Text = note.Text;
                db_note.Title = note.Title;

                noteManager.Update(db_note);

                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(note);
        }

        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.Id == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }

        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteManager.Find(x => x.Id == id);
            noteManager.Delete(note);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetLiked(int[] ids)
        {
            if (CurrentSession.User != null)
            {
                int userid = CurrentSession.User.Id;
                List<int> likedNoteIds = new List<int>();

                if (ids != null)
                {
                    likedNoteIds = likedManager.List(
                        x => x.LikedUser.Id == userid && ids.Contains(x.Note.Id)).Select(
                        x => x.Note.Id).ToList();
                }
                else
                {
                    likedNoteIds = likedManager.List(
                        x => x.LikedUser.Id == userid).Select(
                        x => x.Note.Id).ToList();
                }

                return Json(new { result = likedNoteIds });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }
        }


        [HttpPost]
        public ActionResult SetLikeState(int noteid, bool liked)
        {
            int res = 0;

            if (CurrentSession.User == null)
                return Json(new { hasError = true, errorMessage = "Beğenme işlemi için giriş yapmalısınız.", result = 0 });

            Liked like =
                likedManager.Find(x => x.Note.Id == noteid && x.LikedUser.Id == CurrentSession.User.Id);

            Note note = noteManager.Find(x => x.Id == noteid);

            if (like != null && liked == false)         // like dolu geldiyse ve like işlemi false ise
            {
                res = likedManager.Delete(like);        // like ı sil
            }
            else if (like == null && liked == true)
            {
                res = likedManager.Insert(new Liked()
                {
                    LikedUser = CurrentSession.User,
                    Note = note
                });
            }

            if (res > 0)  // eğer delete ya da insert yapmışsam
            {
                if (liked)  // eğer likelama yapmışsam
                {
                    note.LikeCount++;
                }
                else  // unlike
                {
                    note.LikeCount--;
                }

                res = noteManager.Update(note); // notu update ediyoruz

                return Json(new { hasError = false, errorMessage = string.Empty, result = note.LikeCount });
            }

            // işlem başarısız olursa:

            return Json(new { hasError = true, errorMessage = "Beğenme işlemi gerçekleştirilemedi.", result = note.LikeCount });
        }

        // Not detayını getirmek
        public ActionResult GetNoteText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = noteManager.Find(x => x.Id == id);

            if (note == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialNoteText", note);
        }

    }
}
