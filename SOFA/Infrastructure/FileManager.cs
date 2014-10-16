/*
 *  School Of Future Applications
 *
 *  Copyright (C) 2014  Terminal Coding
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

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
            if(!System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Uploads/")))
                System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Uploads/"));
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