using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Helper
{
    public class OCRHelper
    {
        /// <summary>
        /// 获取图片base64字符串
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetBase64(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            //|| !File.Exists(filename))
            {
                return string.Empty;
            }
            WebRequest myrequest = WebRequest.Create(filename);
            WebResponse myresponse = myrequest.GetResponse();
            Stream imgstream = myresponse.GetResponseStream();

            System.IO.MemoryStream m = new System.IO.MemoryStream();
            System.Drawing.Bitmap bp = new System.Drawing.Bitmap(imgstream);
            bp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = m.GetBuffer();
            return Convert.ToBase64String(bytes);
        }

        private const String host = "https://dm-51.data.aliyun.com";
        private const String method = "POST";
        private const String appcode = "41ed7fa233984565bc64902620a34616";

        /// <summary>
        /// 根据图片路径，获取身份证信息
        /// </summary>
        /// <param name="filename">身份证图片</param>
        /// <returns></returns>
        public static string GetIdcardInfoUrl(string filename)
        {
            string path = "/rest/160601/ocr/ocr_idcard.json";

            string base64 = GetBase64(filename);
            String querys = "";
            //#身份证正反面类型:face/back
            String bodys = "{\"inputs\": [{\"image\": {\"dataType\": 50,\"dataValue\": \"" + base64 + "\"},\"configure\": {\"dataType\": 50,\"dataValue\": \"{\\\"side\\\":\\\"face\\\"}\"  }}]}";
            String url = host + path;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/octet-stream; charset=UTF-8";
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            //Console.WriteLine(httpResponse.StatusCode);
            //Console.WriteLine(httpResponse.Method);
            //Console.WriteLine(httpResponse.Headers);
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 根据图片base64编码，获取身份证信息
        /// </summary>
        /// <param name="base64">身份证图片base64编码</param>
        /// <returns></returns>
        public static string GetIdcardInfoBase64(string base64)
        {
            string path = "/rest/160601/ocr/ocr_idcard.json";

            String querys = "";
            //#身份证正反面类型:face/back
            String bodys = "{\"inputs\": [{\"image\": {\"dataType\": 50,\"dataValue\": \"" + base64 + "\"},\"configure\": {\"dataType\": 50,\"dataValue\": \"{\\\"side\\\":\\\"face\\\"}\"  }}]}";
            String url = host + path;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/octet-stream; charset=UTF-8";
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            //Console.WriteLine(httpResponse.StatusCode);
            //Console.WriteLine(httpResponse.Method);
            //Console.WriteLine(httpResponse.Headers);
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 获取营业执照信息
        /// </summary>
        /// <param name="fileName">营业执照图片</param>
        /// <returns>
        /// </returns>
        public static string GetBusinessLicenseInfo(string fileName)
        {
            const String host = "https://dm-58.data.aliyun.com";
            const String path = "/rest/160601/ocr/ocr_business_license.json";

            string base64 = GetBase64(fileName);

            String querys = "";
            String bodys = "{\"inputs\":[{\"image\":{\"dataType\":50,\"dataValue\":\"" + base64 + "\"}}]}";
            String url = host + path;
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;

            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = method;
            httpRequest.Headers.Add("Authorization", "APPCODE " + appcode);
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/octet-stream; charset=UTF-8";
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

    }
}
