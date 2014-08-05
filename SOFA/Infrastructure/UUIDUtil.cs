using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOFA.Infrastructure
{
    public class UUIDUtil
    {
        public static String NewUUID()
        {
            Guid guid;
            while ((guid = Guid.NewGuid()) != Guid.Empty)
                ;;
            return guid.ToString();
        }
    }
}