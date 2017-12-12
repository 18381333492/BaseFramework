using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Tools
{

    /// <summary>
    ///对常用数据类型的扩展的封装
    /// </summary>
    public  class ConvertHelper
    {
        /// <summary>
        /// 返回Int16值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short ToInt16(object value)
        {
            short res = 0;
            Int16.TryParse(Convert.ToString(value), out res);
            return res;
        }

        /// <summary>
        /// 返回Int32值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt32(object value)
        {
            int res = 0;
            Int32.TryParse(Convert.ToString(value), out res);
            return res;
        }

        /// <summary>
        /// 返回Int64值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToInt64(object value)
        {
            long res = 0;
            Int64.TryParse(Convert.ToString(value), out res);
            return res;
        }

        /// <summary>
        /// 返回一个decimal类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object value)
        {
            decimal res = 0;
            decimal.TryParse(Convert.ToString(value), out res);
            return res;
        }

        /// <summary>
        /// 返回一个float类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ToFloat(object value)
        {
            float res = 0;
            float.TryParse(Convert.ToString(value), out res);
            return res;
        }

        /// <summary>
        /// 返回一个double类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(object value)
        {
            double res = 0;
            double.TryParse(Convert.ToString(value), out res);
            return res;
        }

        /// <summary>
        /// 返回一个bool类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(object value)
        {
            bool res =false;
            bool.TryParse(Convert.ToString(value), out res);
            return res;
        }

        /// <summary>
        /// 返回一个DateTime类型的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object value)
        {
            DateTime res;
            DateTime.TryParse(Convert.ToString(value), out res);
            return res;
        }

        /// <summary>
        /// 返回一个string的值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToString(object value)
        {
            return Convert.ToString(value);
        }
    }
}
