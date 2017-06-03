using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;

namespace Ingenious.Infrastructure.AOP
{
    public class LoggingBehavior : IInterceptionBehavior
    {

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var methodReturn = getNext().Invoke(input, getNext);

            
            //记录操作日志
            //System.IO.File.AppendAllText("C://track.txt", string.Format("类型：{2}，方法名称：{0}，操作时间：{1}，URL:{3}", input.MethodBase.Name, DateTime.Now, input.MethodBase.ReflectedType.Name, System.Web.HttpContext.Current.Request.UrlReferrer.AbsolutePath));

            return methodReturn;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
