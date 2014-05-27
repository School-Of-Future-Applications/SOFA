﻿using SOFA.Models;
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
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
            db = dbcontext;
        }

        public UserManager<IdentityUser> UserManager { get; private set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }

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
        [Authorize]
        public ActionResult CreateEdit(int? id)
        {
            UserPersonCreateEditViewModel view = new UserPersonCreateEditViewModel();
            if (User.IsInRole("SOFAAdmin"))
            {
                if (id != null && id != 0)
                {
                    //admin editing
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
                        if (User.IsInRole("SOFAAdmin"))
                            return View(view);
                        else
                            return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //admin creating
                    view.Person = new Person();
                    view.Person.Id = 0;
                    return View("CreateEdit", view);
                }
            }
            else
            {
                //personal editing
                view.Person = db.Persons.Where(x => x.User.UserName == User.Identity.GetUserName()).FirstOrDefault();
                view.User = view.Person.User;
                return View("UserCreateEdit", view);
            }
        }

        //
        // POST: /User/Create
        [HttpPost]
        [Authorize]
        public ActionResult CreateEdit(UserPersonCreateEditViewModel p)
        {
                if (ModelState.IsValid)
                {
                    if (p.Person.Id == 0)
                    {
                        //creating
                        if (User.IsInRole("SOFAAdmin"))
                        {
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
                            ModelState.AddModelError("", "You do not have permission to create a user.");
                            return View("CreateEdit", p);
                        }
                    }
                    else
                    {
                        if (User.IsInRole("SOFAAdmin"))
                        {
                            //admin editing
                            Person toUpdate = db.Persons.Where(x => x.Id == p.Person.Id).FirstOrDefault();
                            IdentityUser userToUpdate = toUpdate.User;
                            toUpdate.FirstName = p.Person.FirstName;
                            toUpdate.LastName = p.Person.LastName;
                            toUpdate.Email = p.Person.Email;
                            toUpdate.PhoneNumber = p.Person.PhoneNumber;
                            toUpdate.MobileNumber = p.Person.MobileNumber;
                            toUpdate.Position = p.Person.Position;
                            toUpdate.Title = p.Person.Title;
                            userToUpdate.PasswordHash = (new PasswordHasher()).HashPassword(p.Password);
                            db.SaveChanges();
                        }
                        else if(User.Identity.GetUserName() == p.User.UserName)
                        {
                            //user self editing
                            Person toUpdate = db.Persons.Where(x => x.Id == p.Person.Id).FirstOrDefault();
                            IdentityUser userToUpdate = toUpdate.User;
                            if(p.Password == p.VerifyPassword)
                                UserManager.ChangePassword(userToUpdate.Id, p.CurrentPassword, p.Password);
                            toUpdate.FirstName = p.Person.FirstName;
                            toUpdate.LastName = p.Person.LastName;
                            toUpdate.Email = p.Person.Email;
                            toUpdate.PhoneNumber = p.Person.PhoneNumber;
                            toUpdate.MobileNumber = p.Person.MobileNumber;
                            toUpdate.Position = p.Person.Position;
                            toUpdate.Title = p.Person.Title;
                            db.SaveChanges();
                        }
                        else
                        {
                            ModelState.AddModelError("", "You do not have permission to edit this.");
                            return View(p);
                        }
                    }
                    
                }
                //db.SaveChanges();
                return RedirectToAction("Index");
                //return View("UserCreateEdit");
        }
    }
}
