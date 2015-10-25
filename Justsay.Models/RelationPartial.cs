using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justsay.Models
{
    [MetadataType(typeof(RelationMetadata))]
    public partial class Relation : IModel
    {
        private partial class RelationMetadata
        {
            public int ID { get; set; }
            public int MemberID { get; set; }
            public string ShowName { get; set; }
            [DisplayName("你的姓名")]
            [RegularExpression(@"[\u4E00-\u9FA5]{2,5}(?:·[\u4E00-\u9FA5]{2,5})*",
             ErrorMessage = "请输入正确的姓名，TA才能找到你哦")]
            public string FromName { get; set; }
            [DisplayName("你的手机号")]
            [RegularExpression(@"^1[3|4|5|7|8][0-9]\d{4,8}$",
            ErrorMessage = "输入正确的手机号")]
            public string FromPhone { get; set; }
            [DisplayName("你的邮箱")]
            [Required(ErrorMessage = "你的邮箱")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "这输入正确的邮箱")]
            public string FromEmail { get; set; }
            [DisplayName("TA的姓名")]
            [RegularExpression(@"[\u4E00-\u9FA5]{2,5}(?:·[\u4E00-\u9FA5]{2,5})*",
             ErrorMessage = "请输入正确的姓名，TA才能找到你哦")]
            public string ToName { get; set; }
            [DisplayName("TA的手机号")]
            [RegularExpression(@"^1[3|4|5|7|8][0-9]\d{4,8}$",
            ErrorMessage = "输入正确的手机号")]
            public string ToPhone { get; set; }
            [DisplayName("TA的邮箱")]
            [Required(ErrorMessage = "TA的邮箱，必需")]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
            ErrorMessage = "这输入正确的邮箱")]
            public string ToEmail { get; set; }
            public System.DateTime Time { get; set; }
        }
    }
}
