using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justsay.Models
{
    [MetadataType(typeof(ConfessMetadata))]
    public partial class Confess : IModel
    {
        
        class ConfessMetadata
        {
            [DisplayName("系统编号")]
            public int ID { get; set; }
            [DisplayName("用户编号")]
            public int MemberID { get; set; }
            [DisplayName("真实姓名")]
            public string ShowName { get; set; }
            [DisplayName("内容")]
            [Required(ErrorMessage = "请输入内容")]
            [StringLength(4000, ErrorMessage = "请输入4000个字以内")]

            public string Content { get; set; }
            [DisplayName("发表时间")]
            public System.DateTime Time { get; set; }
            [DisplayName("顶")]
            public int Up { get; set; }
            [DisplayName("点击")]
            public int Click { get; set; }
            [DisplayName("信息")]
            [StringLength(30, ErrorMessage = "请输入30个字以内")]
            public string Message { get; set; }
            public bool IsSms { get; set; }
            [DisplayName("视频地址")]
            public string FlashUrl { get; set; }
            [DisplayName("音乐地址")]
            public string MusicUrl { get; set; }
            [DisplayName("TA的邮箱")]
            [Required(ErrorMessage = "请输入邮箱")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "这输入正确的邮箱")]
            public string ToEmail { get; set; }
            [DisplayName("对方的电话")]
            [RegularExpression(@"^1[3|4|5|7|8][0-9]\d{4,8}$",
            ErrorMessage = "输入正确的手机号")]
            public string ToPhone { get; set; }
            [DisplayName("TA的真实姓名")]
            [Required(ErrorMessage = "请输入对方的真实姓名")]
            [RegularExpression(@"[\u4E00-\u9FA5]{2,5}(?:·[\u4E00-\u9FA5]{2,5})*",
             ErrorMessage = "请输入正确的姓名")]
            public string ToName { get; set; }
            [DisplayName("真像")]
            public string ImgUrl { get; set; }
            [DisplayName("选择模板")]
            public string ViewName { get; set; }
        }
    }
}
