using SOFA.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOFA.Models.ViewModels.EnrolmentViewModels
{
    public class FileViewModel : File
    {

        public FileViewModel()
        {
        }

        public FileViewModel(File file)
            : base()
        {
            this.FileID = file.FileID;
            this.Filename = file.Filename;
            this.Location = file.Location;
        }

        public File toFile()
        {
            var file = new File()
            {
                FileID = this.FileID,
                Filename = this.Filename,
                Location = this.Location

            };

            return file;
        }

    }
}