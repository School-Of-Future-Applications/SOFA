using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SOFA.Models.Prefab.StudentDetails;

namespace SOFA.Models.Prefab
{
    public class PrefabFieldFactory
    {
        public  PrefabField Get(string fieldId)
        {
            switch (fieldId)
            {
                case PrefabField.FIRSTNAME:
                    return new FirstName();
                case PrefabField.LASTNAME:
                    return new LastName();
                case PrefabField.PHONE_NUMBER:
                    return new PhoneNumber();
                case PrefabField.MOBILE_NUMBER:
                    return new MobileNumber();
                case PrefabField.STUDENT_EMAIL:
                    return new Email();
                default:
                    throw new ArgumentOutOfRangeException("Unknown prefab field");

            }

        }
    }
}