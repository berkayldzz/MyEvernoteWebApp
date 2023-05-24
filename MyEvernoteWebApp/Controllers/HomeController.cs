using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.BusinessLayer.Results;
using MyEvernote.Entites;
using MyEvernote.Entites.ValueObject;
using MyEvernote.Entities.Messages;
using MyEvernote.EntitiesLayer.Messages;
using MyEvernoteWebApp;
using MyEvernoteWebApp.Filter;
using MyEvernoteWebApp.Models;
using MyEvernoteWebApp.ViewModels;

namespace MyEvernoteWebApp.Controllers
{
    [Exc]
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private EvernoteUserManager evernoteuserManager = new EvernoteUserManager();
        public ActionResult Index()
        {

            return View(noteManager.ListQueryable().Where(x=>x.IsDraft==false).OrderByDescending(x => x.ModifiedOn).ToList());
        }

        // Bir kategoriye tıkladığımızda o kategoriye ait notları getiren kısım.
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Category cat = categoryManager.Find(x => x.Id == id.Value);

            //if (cat == null)
            //{
            //    return HttpNotFound();
            //    //return RedirectToAction("Index", "Home");
            //}

            //List<Note> notes = cat.Notes.Where(
            //    x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList()

            List<Note> notes = noteManager.ListQueryable().Where(
                x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(
                x => x.ModifiedOn).ToList();

            return View("Index", notes);
        }

        // En Beğenilenler..
        public ActionResult MostLiked()
        {

            return View("Index", noteManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        // Hakkımızda
        public ActionResult About()
        {
            return View();
        }

        // Profil Sayfası işlemleri
        [Auth]
        public ActionResult ShowProfile()
        {

            BusinessLayerResult<EvernoteUser> res = evernoteuserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            return View(res.Result);

        }

        [Auth]
        public ActionResult EditProfile()
        {
            BusinessLayerResult<EvernoteUser> res = evernoteuserManager.GetUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }

        [Auth]
        [HttpPost]
        public ActionResult EditProfile(EvernoteUser model, HttpPostedFileBase ProfilImage)
        {
            ModelState.Remove("ModifiedUsername");
            ModelState.Remove("Id");

            if (ModelState.IsValid)
            {
                if (ProfilImage != null &&
                  (ProfilImage.ContentType == "image/jpeg" ||
                  ProfilImage.ContentType == "image/jpg" ||
                  ProfilImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfilImage.ContentType.Split('/')[1]}";

                    ProfilImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;
                }
                BusinessLayerResult<EvernoteUser> res = EvernoteUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Home/EditProfile"
                    };

                    return View("Error", errorNotifyObj);
                }

                CurrentSession.Set<EvernoteUser>("login", res.Result);  // Profil güncellendiği için session güncellendi.

                return RedirectToAction("ShowProfile");
            }
            return View(model);


        }
        
        [Auth]
        public ActionResult DeleteProfile()
        {


            BusinessLayerResult<EvernoteUser> res =
               EvernoteUserManager.RemoveUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profil Silinemedi.",
                    RedirectingUrl = "/Home/ShowProfile"
                };

                return View("Error", errorNotifyObj);
            }

            Session.Clear();

            return RedirectToAction("Index");


        }

        // Giriş,üye olma İşlemleri
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                BusinessLayerResult<EvernoteUser> res = evernoteuserManager.LoginUser(model);

                if (res.Errors.Count > 0)
                {
                    if (res.Errors.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {
                        ViewBag.SetLink = "http://Home/Activate/1234-4567-78980";
                    }


                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                CurrentSession.Set<EvernoteUser>("login", res.Result);  // Sessiona kullanıcı bilgisi saklama
                return RedirectToAction("Index");  // yönlendirme
            }

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {


                BusinessLayerResult<EvernoteUser> res = evernoteuserManager.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                // EvernoteUser user = null;

                //try
                //{
                //  user=  eum.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{

                //    ModelState.AddModelError("", ex.Message);
                //}




                //    if (model.Username == "aaa")
                //    {
                //        ModelState.AddModelError("", "Kullanıcı adı kullanılıyor.");
                //    }

                //    if (model.Email == "aaa@aa.com")
                //    {
                //        ModelState.AddModelError("", "E-posta adresi kullanılıyor.");
                //    }

                //    foreach (var item in ModelState)
                //    {
                //        if (item.Value.Errors.Count > 0)
                //        {
                //            return View(model);
                //        }
                //    }

                OkViewModel notifyobj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login",
                };
                notifyobj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon linkine tıklayarak hesabınızı aktive ediniz.Hesabınızı aktive etmeden  not ekleyemez ve beğenme işlemi yapamazsınız.");


                return View("Ok", notifyobj);  // Hata yoksa çalşacak kısım
            }

            //if (User == null)
            //{
            //    return View(model);
            //}


            return View(model);
        }
        public ActionResult UserActivate(Guid id)
        {

            BusinessLayerResult<EvernoteUser> res = evernoteuserManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                // İşlem hatalıysa
                ErrorViewModel errorNotifyobj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };

                return View("Error", errorNotifyobj);
            }

            // İşlem başarılıysa

            OkViewModel okNotifyobj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login"
            };

            okNotifyobj.Items.Add("Hesabınız aktifleştirildi. Artık not paylaşabilir ve beğenme yapabilirsiniz.");

            return View("Ok", okNotifyobj);  // Açacağın sayfa: Ok Modelin: okNotifyobj

        }
        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }


        public ActionResult HasError()
        {
            return View();
        }

    }
}
