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
using System.Web.Mvc;
using SOFA.Infrastructure;
using SOFA.Models;

namespace SOFA.Controllers
{
    public class FileController : Controller
    {

        [HttpPost]
        public ActionResult UploadFile()
        {
            foreach(String file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                return Json(FileManager.SaveFile(hpf));

            }
            return null;
        }

        public FileResult GetFile(string fileId)
        {
            DBContext dbCon = new DBContext();
            var f = dbCon.Files.Where(x=> x.FileID==fileId).FirstOrDefault();
            return File(FileManager.GetFile(fileId), System.Net.Mime.MediaTypeNames.Application.Octet, "SOFADownload"+System.IO.Path.GetExtension(f.Location));
        }

        //
        // GET: /Upload/
        public ActionResult Index()
        {
            return View();
        }
	}
}