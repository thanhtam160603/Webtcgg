﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webtcgg.Models.Common
{
    public class Filter
    {
        private static readonly string strCheck = "áàạảãâấầậẩẫăắằặẳẵÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴéèẹẻẽêếềệểễÉÈẸẺẼÊẾỀỆỂỄ" +
            "óòọỏõôốồộổỗơớờợởỡÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠúùụủũưứừựửữÚÙỤỦŨƯỨỪỰỬỮíìịỉĩÍÌỊỈĨđĐýỳỵỷỹÝỲỴỶỸ~!@#$%^&*()-[{]}|\\/'\"\\.,><;:";

        private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        public static string FilterChar(string str)
        {
            str = str.Trim();
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            str = str.Replace(" ", "-");
            str = str.Replace("--", "-");
            str = str.Replace("?", "");
            str = str.Replace("&", "");
            str = str.Replace(",", "");
            str = str.Replace(":", "");
            str = str.Replace("!", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("%", "");
            str = str.Replace("#", "");
            str = str.Replace("$", "");
            str = str.Replace("*", "");
            str = str.Replace("`", "");
            str = str.Replace("~", "");
            str = str.Replace("@", "");
            str = str.Replace("^", "");
            str = str.Replace(".", "");
            str = str.Replace("/", "");
            str = str.Replace(">", "");
            str = str.Replace("<", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace(";", "");
            str = str.Replace("+", "");
            return str.ToLower();
        }

        public static string ChuyenCoDauThanhKhongDau(string str)
        {
            str = str.Trim();
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            //str = str.Replace(" ", "-");
            str = str.Replace("--", "-");
            str = str.Replace("?", "");
            str = str.Replace("&", "");
            str = str.Replace(",", "");
            str = str.Replace(":", "");
            str = str.Replace("!", "");
            str = str.Replace("'", "");
            str = str.Replace("\"", "");
            str = str.Replace("%", "");
            str = str.Replace("#", "");
            str = str.Replace("$", "");
            str = str.Replace("*", "");
            str = str.Replace("`", "");
            str = str.Replace("~", "");
            str = str.Replace("@", "");
            str = str.Replace("^", "");
            str = str.Replace(".", "");
            str = str.Replace("/", "");
            str = str.Replace(">", "");
            str = str.Replace("<", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace(";", "");
            str = str.Replace("+", "");
            return str.ToLower();
        }
        public static string FormatNumber(object value, int SoSauDauPhay = 2)
        {
            bool isNumber = IsNumeric(value);
            decimal GT = 0;
            if (isNumber)
            {
                GT = Convert.ToDecimal(value);
            }
            string str = "";
            string thapPhan = "";
            for (int i = 0; i < SoSauDauPhay; i++)
            {
                thapPhan += "#";
            }
            if (thapPhan.Length > 0) thapPhan = "." + thapPhan;
            string snumformat = string.Format("0:#,##0{0}", thapPhan);
            str = String.Format("{" + snumformat + "}", GT);

            return str;
        }
        private static bool IsNumeric(object value)
        {
            return value is sbyte
                       || value is byte
                       || value is short
                       || value is ushort
                       || value is int
                       || value is uint
                       || value is long
                       || value is ulong
                       || value is float
                       || value is double
                       || value is decimal;
        }
    }
}