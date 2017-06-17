using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure
{
    /// <summary>
    /// 时间标签
    /// 当前时间距离目标时间的间隔：
    /// 刚刚
    /// 1分钟前
    /// 1小时前
    /// 。。。
    /// </summary>
    public class TimeLabel
    {
        private DateTime _TargrtDateTime;
        public TimeLabel(DateTime targrtDateTime)
        {
            this._TargrtDateTime = targrtDateTime;
        }
        /// <summary>
        /// 时间标签
        /// </summary>
        public string Label
        {
            get
            {
                if(this._TargrtDateTime==null)
                {
                    return string.Empty;
                }
                string label = string.Empty;
                var timeDiff = DateTime.Now - this._TargrtDateTime;
                if (timeDiff.TotalSeconds < 60)
                    label = "刚刚";
                else if (timeDiff.TotalMinutes < 60)
                    label = string.Format("{0}分钟前", (int)timeDiff.TotalMinutes);
                else if (timeDiff.TotalHours < 8)
                    label = string.Format("{0}小时前", (int)timeDiff.TotalHours);
                else
                {
                    switch(timeDiff.TotalDays.ToString())
                    {
                        case "1":
                            {
                                label = string.Format("昨天{0}", this._TargrtDateTime.ToString("hh:mm"));
                            }
                            break;
                        default:
                            {
                                label =this._TargrtDateTime.ToString("yyyy-MM-dd hh:mm") ;
                            }
                            break;
                    }
                }
                
                return label;
            }
        }
    }
}
