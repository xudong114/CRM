using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utility
    {
        /// <summary>
        /// 生成随机数（数字）
        /// </summary>
        /// <param name="length">随机数长度</param>
        /// <returns></returns>
        public static string GetRandomNo(int length)
        {
            var arr = new string[length];
            var rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                arr[i] = rnd.Next(0, 10).ToString(CultureInfo.InvariantCulture);
            }
            return string.Join("", arr);
        }

        /// <summary>
        /// 生成随机字符串（数字和字母）
        /// </summary>
        /// <param name="length">随机字符长度</param>
        /// <returns></returns>
        public static string GetRandomCode(int length)
        {
            var randMembers = new int[length];
            var validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值
            var seekSeek = unchecked((int)DateTime.Now.Ticks);
            var seekRand = new Random(seekSeek);
            int beginSeek = seekRand.Next(0, Int32.MaxValue - length * 10000);
            var seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                var rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString(CultureInfo.InvariantCulture);
                int numLength = numStr.Length;
                var rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString(CultureInfo.InvariantCulture);
            }
            return validateNumberStr;
        }
    }
}
