using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models.Prefab.StudentDetails;
using SOFA.Models.Prefab.CourseSelect;
using SOFA.Models.Prefab.StandardSection;

namespace SOFA.Models.Prefab
{
    public class PrefabFieldFactory
    {
        public Field Get(string fieldId)
        {
            switch (fieldId)
            {
                case PrefabField.FIRSTNAME:
                    return new FirstName().GetField();
                case PrefabField.LASTNAME:
                    return new LastName().GetField();
                case PrefabField.PHONE_NUMBER:
                    return new PhoneNumber().GetField();
                case PrefabField.MOBILE_NUMBER:
                    return new MobileNumber().GetField();
                case PrefabField.STUDENT_EMAIL:
                    return new Email().GetField();
                case PrefabField.CLASS_SELECT:
                    return new CourseSelectField().GetField();
                case PrefabField.TESTFIELD_A:
                    return new TestField_A().GetField();
                case PrefabField.TESTDATEFIELD_A:
                    return new TestDateField_A().GetField();
                default:
                    throw new ArgumentOutOfRangeException("Unknown prefab field");

            }

        }
    }
}