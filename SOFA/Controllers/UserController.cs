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
using System.Net.Mail;
using System.Net;
using Microsoft.Owin.Security.DataProtection;
using System.Web.Security;
using SOFA.Helpers;
using Microsoft.AspNet.Identity.Owin;

namespace SOFA.Controllers
{
    public class UserController : Controller
    {
 /*       public UserController() : this(new DBContext())
        { }

        public UserController(DBContext dbcontext)
        {
            UserManager = new UserManager<SOFAUser>(new UserStore<SOFAUser>(dbcontext));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbcontext));
            DpapiDataProtectionProvider provider = new DpapiDataProtectionProvider("SOFA");
            UserManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<SOFAUser>(provider.Create("EmailToken"));
            db = dbcontext;
        }

        public UserManager<SOFAUser> UserManager { get; private set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }

        private DBContext db { get; set; }

        private void SetSysAdmin(SOFAUser u)
        {
            UserManager.AddToRole(u.Id, "SystemAdmin");
            UserManager.AddToRole(u.Id, "SOFAAdmin");
            UserManager.AddToRole(u.Id, "Moderator");
        }

        private void SetSOFAAdmin(SOFAUser u)
        {
            UserManager.AddToRole(u.Id, "SOFAAdmin");
            UserManager.AddToRole(u.Id, "Moderator");
        }

        private void SetModerator(SOFAUser u)
        {
            UserManager.AddToRole(u.Id, "Moderator");
        }

        private void SetTeacher(SOFAUser u)
        {
            UserManager.AddToRole(u.Id, "Teacher");
        }

        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /User/
        public ActionResult ResetPassword(string email)
        {
            var person = db.Persons.Where(p => p.Email == email).First();
            if(person != null)
            {
                SOFAUser u = person.User;
                var userip = Request.ServerVariables["REMOTE_ADDR"];
                if (u != null)
                {
                    var token = UserManager.GenerateUserToken("EmailToken", u.Id);
                    Helper.SendEmail(person.Email, "SOFA Password Reset Requested", String.Format("{0},<br />This email is being sent to you because someone(hopefully you) at {1} requested a password reset.<br />If you did not request a reset then please ignore this email, however if you did, please continue by clicking the link below<br />{2}", u.UserName, userip, String.Format("http://{0}/User/ConfirmResetPassword?u={1}&token={2}", "sofa.jinivus.com", u.Id, token)));
                }
            }

            
            return RedirectToAction("Index");
        }

        //
        // GET: /User/
        public ActionResult ConfirmResetPassword(string u,string token)
        {
            var user = UserManager.FindById(u);
            token = token.Replace(' ', '+');
            if(user != null)
            {
                if(UserManager.VerifyUserToken(u, "EmailToken", token))
                {
                    var person = db.Persons.Where(p => p.User.Id == user.Id).First();
                    if (person != null)
                    {
                        var newpassword = Membership.GeneratePassword(10, 1);
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(newpassword);
                        db.SaveChanges();
                        Helper.SendEmail(person.Email, "SOFA New Password", String.Format("{0},<br />Your password has been reset.<br />Your new password is: {1}", user.UserName, newpassword));
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /User/
        public ActionResult ActivateAccount(string u, string token)
        {
            var user = UserManager.FindById(u);
            token = token.Replace(' ', '+');
            if (user != null)
            {
                if (UserManager.VerifyUserToken(u, "activate", token))
                {
                    var person = db.Persons.Where(p => p.User.Id == u).First();
                    if(person != null)
                    {
                        person.Active = true;
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /User/EditRoles?username=joebloggs
        [Authorize(Roles="SOFAAdmin")]
        public ActionResult EditRoles(string username)
        {
            if(username != null)
            { 
                UserRoleEditViewModel view = new UserRoleEditViewModel();
                view.AvailableRoles = RoleManager.Roles;
                var u = UserManager.FindByName(username);
                if(u != null)
                {
                    view.User = u.UserName;
                    view.CurrentRole = GetHighestRole(u);
                    view.CurrentRoleId = RoleManager.FindByName(view.CurrentRole).Id;
                    return View(view);
                }
            }
            return RedirectToAction("Index");
        }

        private string GetHighestRole(SOFAUser u)
        {
           
            var roles = UserManager.GetRoles(u.Id);
            if(roles.Contains("SystemAdmin"))
            {
                return "SystemAdmin";
            }
            else if (roles.Contains("SOFAAdmin"))
            {
                return "SOFAAdmin";
            }
            else if (roles.Contains("Moderator"))
            {
                return "Moderator";
            }
            else if(roles.Count == 1)
            {
                return roles.FirstOrDefault();
            }
            else
            {
                return "None";
            }
        }

        //
        // POST: /User/EditRoles/joebloggs
        [Authorize(Roles = "SOFAAdmin")]
        [HttpPost]
        public ActionResult EditRoles(UserRoleEditViewModel m)
        {
            var u = UserManager.FindByName(m.User);
            if(u != null)
            {
                var r = RoleManager.FindById(m.SelectedRoleId);
                foreach(var role in UserManager.GetRoles(u.Id))
                {
                    UserManager.RemoveFromRole(u.Id,role);
                }
                switch(r.Name)
                {
                    case "SystemAdmin":
                        SetSysAdmin(u);
                        break;
                    case "SOFAAdmin":
                        SetSOFAAdmin(u);
                        break;
                    case "Moderator":
                        SetModerator(u);
                        break;
                    case "Teacher":
                        SetTeacher(u);
                        break;
                }
                db.SaveChanges();
            }
            return EditRoles(u.UserName);
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
                            SOFAUser user = UserManager.FindByName(person.User.UserName);
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
                view.Person = db.Persons.Where(x => x.User.UserName == User.Identity.Name).FirstOrDefault();
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
                                var user = new SOFAUser() { UserName = p.User.UserName };
                                var result = UserManager.Create(user, p.Password);
                                if (result.Succeeded)
                                {
                                    p.Person.User = user;
                                    p.Person.Active = false;
                                    db.Persons.Add(p.Person);
                                    db.SaveChanges();
                                    var activationtoken = UserManager.GenerateUserToken("activate", user.Id);
                                    Helper.SendEmail(p.Person.Email, "SOFA Account Activation Required", String.Format("{0},<br />Your account requires activation before use.<br />Click here to activate your account: {1}", user.UserName, String.Format("http://{0}/User/ActivateAccount?u={1}&token={2}", "sofa.jinivus.com", user.Id, activationtoken)));
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
                                p.Person.Active = false;
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
                            toUpdate.GivenNames = p.Person.GivenNames;
                            toUpdate.LastName = p.Person.LastName;
                            toUpdate.Email = p.Person.Email;
                            toUpdate.PhoneNumber = p.Person.PhoneNumber;
                            toUpdate.MobileNumber = p.Person.MobileNumber;
                            toUpdate.Position = p.Person.Position;
                            SOFAUser userToUpdate = toUpdate.User;
                            if (userToUpdate != null && p.Password != null)
                            {
                                userToUpdate.PasswordHash = (new PasswordHasher()).HashPassword(p.Password);
                            }
                            if(userToUpdate == null && p.User.UserName != null && p.Password != null)
                            {
                                //create user account for existing Person
                                var user = new SOFAUser() { UserName = p.User.UserName };
                                var result = UserManager.Create(user, p.Password);
                                if(result.Succeeded)
                                {
                                    toUpdate.Active = false;
                                    toUpdate.User = user;
                                    userToUpdate = user;
                                    var activationtoken = UserManager.GenerateUserToken("activate", user.Id);
                                    Helper.SendEmail(p.Person.Email, "SOFA Account Activation Required", String.Format("{0},<br />Your account requires activation before use.<br />Click here to activate your account: {1}", user.UserName, String.Format("http://{0}/User/ActivateAccount?u={1}&token={2}", "sofa.jinivus.com", user.Id, activationtoken)));
                                }
                            }
                                
                            db.SaveChanges();
                        }
                        else if(User.Identity.GetUserName() == p.User.UserName)
                        {
                            //user self editing
                            Person toUpdate = db.Persons.Where(x => x.Id == p.Person.Id).FirstOrDefault();
                            SOFAUser userToUpdate = toUpdate.User;
                            if (p.Password != null)
                                if(p.Password == p.VerifyPassword)
                                    UserManager.ChangePassword(userToUpdate.Id, p.CurrentPassword, p.Password);
                            toUpdate.GivenNames = p.Person.GivenNames;
                            toUpdate.LastName = p.Person.LastName;
                            toUpdate.Email = p.Person.Email;
                            toUpdate.PhoneNumber = p.Person.PhoneNumber;
                            toUpdate.MobileNumber = p.Person.MobileNumber;
                            toUpdate.Position = p.Person.Position;
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
        }*/
    }
}
