using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicClassGeneration
{
    public static class AccessModifierExtensions
    {
        public static string GetString(this AccessModifierEnum self)
        {
            switch(self)
            {
                case AccessModifierEnum.PUBLIC:
                    return "public";
                case AccessModifierEnum.PROTECTED:
                    return "protected";
                case AccessModifierEnum.INTERNAL:
                    return "internal";
                case AccessModifierEnum.PRIVATE:
                    return "private";
                default:
                    return string.Empty;
            }

        }
    }

    public enum AccessModifierEnum
    {
        PUBLIC, 
        PRIVATE, 
        INTERNAL, 
        PROTECTED,
        NONE
    }
}
