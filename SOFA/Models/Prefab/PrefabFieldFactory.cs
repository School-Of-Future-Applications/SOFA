using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models.Prefab.StudentDetails;

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
                default:
                    throw new ArgumentOutOfRangeException("Unknown prefab field");

            }

        }
    }
}