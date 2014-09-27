using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models;

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
    }
}