using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Go.Controllers
{
    /// <summary>
    /// 文件管理
    /// </summary>
    public class FilesController : BaseController
    {
        private readonly IF_FileService _IF_FileService;
        public FilesController(IF_FileService iF_FileService)
        {
            this._IF_FileService = iF_FileService;
        }
        /// <summary>
        /// 根据ReferenceId获取图片列表
        /// </summary>
        /// <param name="referenceId"></param>
        /// <returns></returns>
        public IHttpActionResult GetFiles(Guid referenceId)
        {
            var list = this._IF_FileService.GetFilesByReferenceId(referenceId);

            return Json<dynamic>(new { Status = true, List = list });
        }

        /// <summary>
        /// 根据ReferenceId和Code获取图片列表
        /// </summary>
        /// <param name="referenceId"></param>
        /// <param name="code">文件类型</param>
        /// <returns></returns>
        public IHttpActionResult GetFiles(Guid referenceId, string code)
        {
            var list = this._IF_FileService.GetFilesByReferenceId(referenceId, code);

            return Json<dynamic>(new { Status = true, List = list });
        }
        /// <summary>
        /// 根据图片Id删除图片
        /// </summary>
        /// <param name="id">图片Id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Delete(Guid id)
        {
            this._IF_FileService.Delete(new List<F_FileDTO> { new F_FileDTO { Id = id } });
            return Json(new MessageResult { Status = true, Message = "删除成功" });
        }

        [HttpPost]
        public IHttpActionResult Create()
        {
            //return Json(new MessageResult { Status = false, Message = Request.Headers.Authorization.Parameter });
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Json(new MessageResult { Status = false, Message = "文件格式错误" });
            }
            var resultList = new List<object>();
            var provider = new MultipartFilesMemoryStreamProvider();

            try
            {
                var result = Task.Run(async () => await Request.Content.ReadAsMultipartAsync(provider)).Result;

                string refId = result.FormData.GetValues("referenceId").FirstOrDefault();
                string code = result.FormData.GetValues("code").FirstOrDefault();
                Guid referenceId = Guid.Empty;
                Guid.TryParse(refId, out referenceId);

                foreach (var stream in result.Contents)
                {
                    var filename = stream.Headers.ContentDisposition.FileName;
                    if (string.IsNullOrWhiteSpace(filename))
                        continue;
                    var tempFileName = filename.Substring(filename.LastIndexOf(".")).Replace("\"", "");

                    var model = new F_FileDTO
                    {
                        Name = string.Format("{0}{1}", DateTime.Now.Ticks.ToString(), tempFileName),
                        Data = stream.ReadAsStreamAsync().Result,
                        ReferenceId = referenceId,
                        Code = code,
                        CreatedBy = Guid.NewGuid(),//this.User.Id,
                        ModifiedBy =Guid.NewGuid(), //this.User.Id,
                        Path = System.Web.HttpContext.Current.Server.MapPath("~/uploads")
                    };
                    model = this._IF_FileService.Create(model);
                    resultList.Add(new { Id = model.Id, Path = model.Path, Code = model.Code });
                }
            }
            catch (Exception e)
            {
                return Json(new MessageResult { Status = false, Message = e.Message });
            }

            return Json<dynamic>(new { Status = true, Message = "文件上传成功", File = resultList.Count() > 0 ? resultList[0] : null });
        }
    }
    public class MultipartFilesMemoryStreamProvider : MultipartMemoryStreamProvider
    {
        private readonly Collection<bool> _isFormData = new Collection<bool>();
        private readonly NameValueCollection _formData = new NameValueCollection(StringComparer.OrdinalIgnoreCase);

        public NameValueCollection FormData
        {
            get { return _formData; }
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            if (parent == null) throw new ArgumentNullException("parent");
            if (headers == null) throw new ArgumentNullException("headers");

            var contentDisposition = headers.ContentDisposition;

            if (contentDisposition != null)
            {
                _isFormData.Add(String.IsNullOrEmpty(contentDisposition.FileName));
                return base.GetStream(parent, headers);
            }

            throw new InvalidOperationException("Did not find required 'Content-Disposition' header field in MIME multipart body part.");
        }

        public override async Task ExecutePostProcessingAsync()
        {
            for (var index = 0; index < Contents.Count; index++)
            {
                if (IsStream(index))
                    continue;

                var formContent = Contents[index];
                var contentDisposition = formContent.Headers.ContentDisposition;
                var formFieldName = UnquoteToken(contentDisposition.Name) ?? string.Empty;
                var formFieldValue = await formContent.ReadAsStringAsync();
                FormData.Add(formFieldName, formFieldValue);
            }
        }

        private static string UnquoteToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return token;

            if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
                return token.Substring(1, token.Length - 2);

            return token;
        }

        public bool IsStream(int idx)
        {
            return !_isFormData[idx];
        }
    }


}
