using System;
using System.Text;

namespace JustSay.Common.DotNetCode
{
    /// <summary>
    /// 转换类
    /// </summary>
    public sealed class ConvertHelper
    {
        public static string ConvertBase(string value, int from, int to)
        {
            if (!ConvertHelper.isBaseNumber(from))
            {
                throw new ArgumentException("参数from只能是2,8,10,16四个值。");
            }
            if (!ConvertHelper.isBaseNumber(to))
            {
                throw new ArgumentException("参数to只能是2,8,10,16四个值。");
            }
            int intValue = Convert.ToInt32(value, from);
            string result = Convert.ToString(intValue, to);
            if (to == 2)
            {
                switch (result.Length)
                {
                    case 3:
                        result = "00000" + result;
                        break;

                    case 4:
                        result = "0000" + result;
                        break;

                    case 5:
                        result = "000" + result;
                        break;

                    case 6:
                        result = "00" + result;
                        break;

                    case 7:
                        result = "0" + result;
                        break;
                }
            }
            return result;
        }

        private static bool isBaseNumber(int baseNumber)
        {
            return baseNumber == 2 || baseNumber == 8 || baseNumber == 10 || baseNumber == 16;
        }

        public static byte[] StringToBytes(string text)
        {
            return Encoding.Default.GetBytes(text);
        }

        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }

        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static int BytesToInt32(byte[] data)
        {
            int result;
            if (data.Length < 4)
            {
                result = 0;
            }
            else
            {
                int num = 0;
                if (data.Length >= 4)
                {
                    byte[] tempBuffer = new byte[4];
                    Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);
                    num = BitConverter.ToInt32(tempBuffer, 0);
                }
                result = num;
            }
            return result;
        }

        public static int ToInt32<T>(T data, int defValue)
        {
            int result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToInt32(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static int ToInt32(string data, int defValue)
        {
            int result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                int temp = 0;
                if (int.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static int ToInt32(object data, int defValue)
        {
            int result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToInt32(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static bool ToBoolean<T>(T data, bool defValue)
        {
            bool result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToBoolean(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static bool ToBoolean(string data, bool defValue)
        {
            bool result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                bool temp = false;
                if (bool.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static bool ToBoolean(object data, bool defValue)
        {
            bool result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToBoolean(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static float ToFloat<T>(T data, float defValue)
        {
            float result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToSingle(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static float ToFloat(object data, float defValue)
        {
            float result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToSingle(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static float ToFloat(string data, float defValue)
        {
            float result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                float temp = 0f;
                if (float.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble<T>(T data, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDouble(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble<T>(T data, int decimals, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Math.Round(Convert.ToDouble(data), decimals);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble(object data, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDouble(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble(string data, double defValue)
        {
            double result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                double temp = 0.0;
                if (double.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble(object data, int decimals, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Math.Round(Convert.ToDouble(data), decimals);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble(string data, int decimals, double defValue)
        {
            double result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                double temp = 0.0;
                if (double.TryParse(data, out temp))
                {
                    result = Math.Round(temp, decimals);
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static object ConvertTo(object data, Type targetType)
        {
            object result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = null;
            }
            else
            {
                Type type2 = data.GetType();
                if (targetType == type2)
                {
                    result = data;
                }
                else
                {
                    if ((targetType == typeof(Guid) || targetType == typeof(Guid?)) && type2 == typeof(string))
                    {
                        if (string.IsNullOrEmpty(data.ToString()))
                        {
                            result = null;
                        }
                        else
                        {
                            result = new Guid(data.ToString());
                        }
                    }
                    else
                    {
                        if (targetType.IsEnum)
                        {
                            try
                            {
                                result = Enum.Parse(targetType, data.ToString(), true);
                                return result;
                            }
                            catch
                            {
                                result = Enum.ToObject(targetType, data);
                                return result;
                            }
                        }
                        if (targetType.IsGenericType)
                        {
                            targetType = targetType.GetGenericArguments()[0];
                        }
                        result = Convert.ChangeType(data, targetType);
                    }
                }
            }
            return result;
        }

        public static T ConvertTo<T>(object data)
        {
            T result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = default(T);
            }
            else
            {
                object obj = ConvertHelper.ConvertTo(data, typeof(T));
                if (obj == null)
                {
                    result = default(T);
                }
                else
                {
                    result = (T)((object)obj);
                }
            }
            return result;
        }

        public static decimal ToDecimal<T>(T data, decimal defValue)
        {
            decimal result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDecimal(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static decimal ToDecimal(object data, decimal defValue)
        {
            decimal result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDecimal(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static decimal ToDecimal(string data, decimal defValue)
        {
            decimal result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                decimal temp = 0m;
                if (decimal.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static DateTime ToDateTime<T>(T data, DateTime defValue)
        {
            DateTime result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDateTime(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static DateTime ToDateTime(object data, DateTime defValue)
        {
            DateTime result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDateTime(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static DateTime ToDateTime(string data, DateTime defValue)
        {
            DateTime result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                DateTime temp = DateTime.Now;
                if (DateTime.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static string ConvertToSBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == ' ')
                {
                    c[i] = '\u3000';
                }
                else
                {
                    if (c[i] < '\u007f')
                    {
                        c[i] += 'ﻠ';
                    }
                }
            }
            return new string(c);
        }

        public static string ConvertToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == '\u3000')
                {
                    c[i] = ' ';
                }
                else
                {
                    if (c[i] > '＀' && c[i] < '｟')
                    {
                        c[i] -= 'ﻠ';
                    }
                }
            }
            return new string(c);
        }
    }
}