﻿using Avalonia;

using System.ComponentModel;
using System.Reflection;

namespace SmartFamily.MVVM
{
    public static class Extensions
    {
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();

            // Tries to find a DescriptionAttribute for a potential friendly name
            // for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).ToArray();

                if (attrs != null && attrs.Length > 0)
                {
                    // Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            // If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }

        public static double GetDistance(this Point start, Point point)
        {
            var a2 = Math.Pow(Math.Abs(start.X - point.X), 2);
            var b2 = Math.Pow(Math.Abs(start.Y - point.Y), 2);

            return Math.Sqrt(a2 + b2);
        }
    }
}