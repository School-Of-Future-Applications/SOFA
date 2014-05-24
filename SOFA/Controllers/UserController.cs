using SOFA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System.Web.Mvc;
using SOFA.Models.ViewModels;
using System.Threading.Tasks;

namespace SOFA.Controllers
{
    public class UserController : Controller
    {
        public UserController() : this(new DBContext())
        { }

        public UserController(DBContext dbcontext)
        {
            UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(dbcontext));
            db = dbcontext;
        }

        public UserManager<IdentityUser> UserManager { get; private set; }

        private DBContext db { get; set; }

        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/Create
        public ActionResult CreateEdit(int? id)
        {
            if(id != null && id != 0)
            {
                UserPersonCreateEditViewModel view = new UserPersonCreateEditViewModel();
                Person person = db.Persons.Where(x => x.Id == id).FirstOrDefault();
                if (person != null)
                {
                    if (person.User != null)
                    {
                        IdentityUser user = UserManager.FindByName(person.User.UserName);
                        if (user != null)
                            view.User = user;
                    }
                    view.Person = person;
                    return View(view);

                }
            }
            return View("CreateEdit");
        }

        //
        // POST: /User/Create
        [HttpPost]
        public ActionResult CreateEdit(UserPersonCreateEditViewModel p)
        {
                if (ModelState.IsValid)
                {
                    if (p.Person.Id == 0)
                    {
                        //creating
                        if (p.User.UserName != null)
                        {
                            var user = new IdentityUser() { UserName = p.User.UserName };
                            var result = UserManager.Create(user, p.Password);
                            if (result.Succeeded)
                            {
                                p.Person.User = user;
                                db.Persons.Add(p.Person);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ModelState.AddModelError("", result.Errors.FirstOrDefault());
                                return View("CreateEdit", p);
                            }
                        }
                        else
                        {
                            db.Persons.Add(p.Person);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        Person toUpdate = db.Persons.Where(x => x.Id == p.Person.Id).FirstOrDefault();
                    }
                    
                }
                //db.SaveChanges();
                return RedirectToAction("Index");
                //return View("UserCreateEdit");
        }

        //
        // GET: /User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
