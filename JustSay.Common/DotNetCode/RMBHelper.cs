using System;

namespace JustSay.Common.DotNetCode
{
    /// <summary>
    /// 人民币帮助类
    /// </summary>
    public class RMBHelper
    {
        public static string CmycurD(decimal num)
        {
            string str = "零壹贰叁肆伍陆柒捌玖";
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分";
            string str3 = "";
            string ch2 = "";
            int nzero = 0;
            num = Math.Round(Math.Abs(num), 2);
            string str4 = ((long)(num * 100m)).ToString();
            int i = str4.Length;
            string result;
            if (i > 15)
            {
                result = "溢出";
            }
            else
            {
                str2 = str2.Substring(15 - i);
                for (int j = 0; j < i; j++)
                {
                    string str5 = str4.Substring(j, 1);
                    int temp = Convert.ToInt32(str5);
                    string ch3;
                    if (j != i - 3 && j != i - 7 && j != i - 11 && j != i - 15)
                    {
                        if (str5 == "0")
                        {
                            ch3 = "";
                            ch2 = "";
                            nzero++;
                        }
                        else
                        {
                            if (str5 != "0" && nzero != 0)
                            {
                                ch3 = "零" + str.Substring(temp, 1);
                                ch2 = str2.Substring(j, 1);
                                nzero = 0;
                            }
                            else
                            {
                                ch3 = str.Substring(temp, 1);
                                ch2 = str2.Substring(j, 1);
                                nzero = 0;
                            }
                        }
                    }
                    else
                    {
                        if (str5 != "0" && nzero != 0)
                        {
                            ch3 = "零" + str.Substring(temp, 1);
                            ch2 = str2.Substring(j, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str5 != "0" && nzero == 0)
                            {
                                ch3 = str.Substring(temp, 1);
                                ch2 = str2.Substring(j, 1);
                                nzero = 0;
                            }
                            else
                            {
                                if (str5 == "0" && nzero >= 3)
                                {
                                    ch3 = "";
                                    ch2 = "";
                                    nzero++;
                                }
                                else
                                {
                                    if (i >= 11)
                                    {
                                        ch3 = "";
                                        nzero++;
                                    }
                                    else
                                    {
                                        ch3 = "";
                                        ch2 = str2.Substring(j, 1);
                                        nzero++;
                                    }
                                }
                            }
                        }
                    }
                    if (j == i - 11 || j == i - 3)
                    {
                        ch2 = str2.Substring(j, 1);
                    }
                    str3 = str3 + ch3 + ch2;
                    if (j == i - 1 && str5 == "0")
                    {
                        str3 += '整';
                    }
                }
                if (num == 0m)
                {
                    str3 = "零元整";
                }
                result = str3;
            }
            return result;
        }

        public static string CmycurD(string numstr)
        {
            string result;
            try
            {
                decimal num = Convert.ToDecimal(numstr);
                result = RMBHelper.CmycurD(num);
            }
            catch
            {
                result = "非数字形式！";
            }
            return result;
        }
    }
}