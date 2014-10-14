using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models;
using System.Web.Mvc;

namespace SOFA.Infrastructure
{
    public class FileManager
    {

        public static string SaveFile(HttpPostedFileBase file) 
        {
            DBContext dbCon = new DBContext();
            File f = new File();
            f.FileID = UUIDUtil.NewUUID();
            f.Filename = file.FileName;
            f.Location = HttpContext.Current.Server.MapPath("~/Uploads/" + file.FileName);
            file.SaveAs(f.Location);
            dbCon.Files.Add(f);
            dbCon.SaveChanges();
            return f.FileID;
        }

        public static byte[] GetFile(string id)
        {
            DBContext dbCon = new DBContext();
            var file = dbCon.Files.Where(x => x.FileID == id).FirstOrDefault();
            return System.IO.File.ReadAllBytes(file.Location);
            /*
            System.IO.Path.GetExtension(file.Location);
            return System.Web.Mvc.Controller.File(f,System.Net.Mime.MediaTypeNames.Application.Octet,"SOFAFile");*/
        }
    }
}