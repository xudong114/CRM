using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace API.Go.Controllers
{
    /// <summary>
    /// 消息服务
    /// </summary>
    public class MessageController : BaseController
    {
        public MessageController() { }

        private readonly IF_ActivityService _IF_ActivityService;
        public MessageController(IF_ActivityService iF_ActivityService)
        {
            this._IF_ActivityService = iF_ActivityService;
        }

        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns>返回验证码</returns>
        [AllowAnonymous]
        public string GetSecurityCode(string phoneNo)
        {
            var code = Utility.GetRandomNo(6);
            var result = MessageService.SMSSend(phoneNo,code);
            if(result.Status)
            {
                HttpContext.Current.Session[GlobalMessage.API_Session_SecurityCode] = new SMSSession { Code = code, ExpireTime = DateTime.Now.AddMinutes(1), PhoneNo = phoneNo };
            }

            return code;
        }

        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns>返回验证码</returns>
        [AllowAnonymous]
        public string Get(string phoneNo)
        {
            return this.GetSecurityCode(phoneNo);
        }

        /// <summary>
        /// 根据外部标识获取消息
        /// </summary>
        /// <param name="referenceId">外部标识</param>
        /// <returns></returns>
        public IHttpActionResult GetMessage(Guid referenceId)
        {
            var list = this._IF_ActivityService.GetAll(referenceId, null, null);
            return Json(new MessageResult { Status=true,Data=list.OrderByDescending(item=>item.CreatedDate)});
        }
        /// <summary>
        /// 根据外部标识获取新消息数量，最近7天之内
        /// </summary>
        /// <param name="referenceId">外部标识</param>
        /// <returns></returns>
        public IHttpActionResult GetMessageCount(Guid referenceId)
        {
            var count = this._IF_ActivityService.GetMessageCount(referenceId);
            return Json(new MessageResult { Status = true, Data = count });
        }
        /// <summary>
        /// 根据外部标识获取新消息数量，最近7天之内
        /// </summary>
        /// <param name="referenceId">外部标识</param>
        /// <returns></returns>
        public IHttpActionResult GetMessageCount()
        {
            var count = this._IF_ActivityService.GetMessageCount(this.User.Id);
            return Json(new MessageResult { Status = true, Data = count });
        }
        /// <summary>
        /// 设置消息状态已读
        /// </summary>
        /// <param name="id">消息标识</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult SetMessageStatus(Guid id)
        {
            var message = this._IF_ActivityService.GetByKey(id);
            if(message==null)
            {
                return Json(new MessageResult { Status = false, Message="消息无效" });
            }
            message.IsRead = true;
            this._IF_ActivityService.Update(new List<F_ActivityDTO> { message});
            return Json(new MessageResult { Status = true });
        }
    }
}