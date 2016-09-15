using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CrarftedFood.Extentions
{
    public static class EnumExtentions
    {
        public static string ToDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());

            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != attrs && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return enumeration.ToString();
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        
        public static List<SelectListItem> CreateSelectListItem(this Enum enumeration)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var val in Enum.GetValues(enumeration.GetType()))
            {

                FieldInfo fi = val.GetType().GetField(val.ToString());

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                string description;
                if (attributes != null &&
                    attributes.Length > 0)
                    description = attributes[0].Description;
                else
                    description = val.ToString();

                list.Add(new SelectListItem
                {
                    Text = description,
                    Value = ((int)val).ToString()
                });
            }
            return list;
        } 



       
    }
}