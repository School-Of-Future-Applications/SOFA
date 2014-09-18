using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOFA.Infrastructure;
using SOFA.Models;
using SOFA.Models.ViewModels;

namespace SOFA.Controllers
{
    public class SectionController : DashBoardBaseController
    {
        //
        // GET: /Section/
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult Index()
        {
            return View(this.DBCon().Sections.OrderBy(x => x.DateCreated).ToList());
        }

        //
        // GET: /Section/Create
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult Edit(String sectionID = null)
        {
            SectionEditViewModel sevm = new SectionEditViewModel();
            sevm.Section = this.DBCon().Sections.Where(s => s.Id == sectionID).FirstOrDefault();
            var ofids = this.DBCon().SectionFieldOrders.Where(sfo => sfo.Section.Id == sevm.Section.Id).OrderBy(sfo => sfo.Order).Select(x => x.Field.Id).ToList();
            sevm.OrderedFields = new List<Field>();
            foreach(string id in ofids)
            {
                sevm.OrderedFields.Add(this.DBCon().Fields.Where(x => x.Id == id).FirstOrDefault());
            }
            return View(sevm);
        }

        //
        // POST: /Section/Create
        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult CreateEdit(Section section)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult CreateSection()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult CreateSection(Section section)
        {
            if (ModelState.IsValid)
            {
                this.DBCon().Sections.Add(section);
                this.DBCon().SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult AddField(string sectionId, string type, string prompt)
        {
            Section s = this.DBCon().Sections.Where(x => x.Id == sectionId).FirstOrDefault();
            Field f = new Field(type);
            f.PromptValue = prompt;
            f.Section = s;
            FieldOption fo = new FieldOption(FieldOption.OPT_MANDATORY);
            f.FieldOptions.Add(fo);
            switch(f.FieldType)
            {
                case Field.TYPE_TEXT_MULTI:
                    break;
                case Field.TYPE_TEXT_SINGLE:
                    FieldOption num = new FieldOption(FieldOption.OPT_NUMERIC);
                    f.FieldOptions.Add(num);
                    break;
                case Field.TYPE_FILE:
                    break;
                case Field.TYPE_DATE:
                    break;
                case Field.TYPE_DROPDOWN:
                    FieldOption drop = new FieldOption(FieldOption.OPT_RESPONSE);
                    f.FieldOptions.Add(drop);
                    break;
            }
            this.DBCon().Fields.Add(f);
            this.DBCon().SaveChanges();
            return Json(f.Id);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult EditFieldPrompt(string fieldId, string prompt)
        {
            Field f = this.DBCon().Fields.Where(x => x.Id == fieldId).FirstOrDefault();
            f.PromptValue = prompt;
            this.DBCon().SaveChanges();
            return Json(f.Id);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult GetFieldOptionsForId(string id)
        {
            Field f = this.DBCon().Fields.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("EditFieldValidation", f);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult SetSectionFieldOrder(List<string> FieldIds)
        {
            for(int i = 0;i<FieldIds.Count();i++)
            {
                var fid = FieldIds[i];
                var usfo = this.DBCon().SectionFieldOrders.Where(sfo => sfo.FieldID == fid).FirstOrDefault();
                if(usfo != null)
                {
                    usfo.Order = i;
                }
                else
                {
                    usfo = new SectionFieldOrder();
                    usfo.FieldID = fid;
                    usfo.Order = i;
                    usfo.SectionID = this.DBCon().Fields.Where(f => f.Id == fid).FirstOrDefault().Section.Id;
                    this.DBCon().SectionFieldOrders.Add(usfo);
                }
                this.DBCon().SaveChanges();
            }
            return Json(1);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult FieldOptionsSubView(string id)
        {
            Field f = this.DBCon().Fields.Where(x => x.Id == id).FirstOrDefault();
            return PartialView("FieldOptionsSubView", f);
        }

        [HttpPost]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult SetFieldValidationOptions(string fieldID, string[] data, string[] names)
        {
            var field = this.DBCon().Fields.Where(x => x.Id == fieldID).FirstOrDefault();
            for(int i = 0;i<names.Length;i++)
            {
                var name = names[i];
                if(name == "Mandatory")
                {
                    field.FieldOptions.Where(x => x.OptionType == FieldOption.OPT_MANDATORY).FirstOrDefault().OptionValue = data[i].ToUpper();
                }
                else if(name == "Numeric")
                {
                    field.FieldOptions.Where(x => x.OptionType == FieldOption.OPT_NUMERIC).FirstOrDefault().OptionValue = data[i].ToUpper();
                }
                else if(name == "Responses")
                {
                    var responses = data[i].Split(',');
                    while(field.FieldOptions.Where(x => x.OptionType == FieldOption.OPT_RESPONSE).Count() > 0)
                    {
                        field.FieldOptions.Remove(field.FieldOptions.Where(x => x.OptionType == FieldOption.OPT_RESPONSE).FirstOrDefault());
                    }
                    foreach (string response in responses)
                    {
                        FieldOption fo = new FieldOption(FieldOption.OPT_RESPONSE);
                        fo.OptionValue = response;
                        field.FieldOptions.Add(fo);
                    }
                }
            }
            this.DBCon().SaveChanges();
            //jsonData
            /*for (int i = 0; i < FieldIds.Count(); i++)
            {
                var fid = FieldIds[i];
                var usfo = this.DBCon().SectionFieldOrders.Where(sfo => sfo.FieldID == fid).FirstOrDefault();
                if (usfo != null)
                {
                    usfo.Order = i;
                }
                else
                {
                    usfo = new SectionFieldOrder();
                    usfo.FieldID = fid;
                    usfo.Order = i;
                    usfo.SectionID = this.DBCon().Fields.Where(f => f.Id == fid).FirstOrDefault().Section.Id;
                    this.DBCon().SectionFieldOrders.Add(usfo);
                }
                this.DBCon().SaveChanges();
            }*/
            return Json(1);
        }


        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult Delete(string id)
        {
            DeleteConfirmationViewModel dcvm = new DeleteConfirmationViewModel()
            {
                DeleteAction = "Delete",
                DeleteController = "Section",
                HeaderText = "Confirm Section Deletion",
                ConfirmationText = "Are you sure you wish to delete this Section?"
            };
            dcvm.RouteValues.Add("id", id);

            return PartialView("DeleteConfirmationViewModel", dcvm); 
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult DeletePost(string id)
        {
            Section sec = null;
            try
            {
                if (!Section.DEFAULT_SECTION_IDS.Contains(id))
                {
                    sec = this.DBCon().Sections.Where(x => x.Id == id).First();
                    this.DBCon().Entry(sec).State = EntityState.Deleted;
                    this.DBCon().SaveChanges();
                }
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = SOFARole.AUTH_SOFAADMIN)]
        public ActionResult IndexPartial()
        {
            var sections = this.DBCon().Sections.ToList();
            return View(new SectionSelectViewModel(sections));
        }

        [Authorize(Roles = SOFARole.AUTH_MODERATOR)]
        public ActionResult SectionPartial(string SectionId)
        {
            return View();
        }


        public override Enum NavProviderTerm()
        {
            return DashboardNavTerms.Sections;
        }
    }
}
