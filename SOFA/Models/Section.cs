using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

using SOFA.Infrastructure;

namespace SOFA.Models
{
    public class Section
    {
        public static string COURSE_SECTION_ID = "a9bd91b1-29b5-11e4-8c21-0800200c9a66";
        public static string COURSE_SECTION_NAME = "Course Select";
        public static string STUDENT_SECTION_ID = "a9bd91b0-29b5-11e4-8c21-0800200c9a66";
        public static string STUDENT_SECTION_NAME = "Student Information";

        public static List<string> DEFAULT_SECTION_IDS = new List<string>()
            {
                 COURSE_SECTION_ID
                ,STUDENT_SECTION_ID
            };

        public Section()
        {
            Id = UUIDUtil.NewUUID();
            Fields = new List<Field>();
            DateCreated = DateTime.Now;
        }

        public Section(String sectionName)
            : this()
        {
            Name = sectionName;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public string DateCreatedString()
        {
            string date = "";
            if (DateCreated != null)
                date = DateCreated.ToString(DateTimeUtil.DATE_FORMAT);
            return date;
        }

        [Required]
        [Display(Name = "Section Name")]
        public string Name { get; set; }

        public ICollection<Field> Fields;
    }
}
