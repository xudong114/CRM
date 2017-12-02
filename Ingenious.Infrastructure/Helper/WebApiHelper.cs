using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Helper
{
    /// <summary>
    /// WebApi助手
    /// </summary>
    public class WebApiHelper
    {
        /// <summary>
        /// 用POST方式调用WebApi
        /// </summary>
        /// <param name="url">WebApi URL</param>
        /// <param name="data">需要传递的参数</param>
        /// <returns></returns>
        public static async Task<string> Post(string url, Dictionary<string, string> data)
        {
            string result = string.Empty;
            //设置HttpClientHandler的AutomaticDecompression  
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //创建HttpClient（注意传入HttpClientHandler）  
            using (var http = new HttpClient(handler))
            {
                //使用FormUrlEncodedContent做HttpContent  
                var content = new FormUrlEncodedContent(data);
                //await异步等待回应
                var response = await http.PostAsync(url, content);
                //确保HTTP成功状态值
                response.EnsureSuccessStatusCode();
                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）  
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }
        /// <summary>
        ///  用GET方式调用WebApi
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> Get(string url)
        {
            //创建HttpClient（注意传入HttpClientHandler）  
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            using (var http = new HttpClient(handler))
            {
                //await异步等待回应  
                var response = await http.GetAsync(url);
                //确保HTTP成功状态值  
                response.EnsureSuccessStatusCode();

                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）  
                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
