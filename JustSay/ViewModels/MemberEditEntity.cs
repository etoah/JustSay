using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JustSay.ViewModels
{
    public class MemberEditEntity
    {
        [DisplayName("系统编号")]
        public int ID { get; set; }
        [Required(ErrorMessage = "请输入邮箱")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        ErrorMessage = "这输入正确的邮箱")]
        public string Email { get; set; }
        [DisplayName("密码")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "请输入密码")]
        [StringLength(50, ErrorMessage = "请输入1~50个字")]
        public string Password { get; set; }
        [DisplayName("注册时间")]
        public System.DateTime JoinDate { get; set; }
        [DisplayName("马甲")]
        [StringLength(6, ErrorMessage = "请输入6个字以内")]
        public string ShowName { get; set; }
        [DisplayName("最后访问时间")]
        public Nullable<System.DateTime> LastVisit { get; set; }
        [DisplayName("QQ")]
        [RegularExpression(@"^[1-9](\d){4,9}$",
        ErrorMessage = "输入正确的QQ号")]
        public string QQ { get; set; }
        [DisplayName("手机号")]
        [RegularExpression(@"^1[3|4|5|7|8][0-9]\d{4,8}$",
        ErrorMessage = "输入正确的手机号")]
        public string Phone { get; set; }
        [DisplayName("真实姓名")]
        [RegularExpression(@"[\u4E00-\u9FA5]{2,5}(?:·[\u4E00-\u9FA5]{2,5})*",
         ErrorMessage = "请输入正确的姓名")]
        public string RealName { get; set; }
        [DisplayName("角色的时间")]
        public Nullable<System.DateTime> RoleDeadLine { get; set; }
        [DisplayName("积分")]
        public int Score { get; set; }
        [DisplayName("金币")]
        public int Money { get; set; }
        [DisplayName("最后一次发贴时间时间")]
        public Nullable<System.DateTime> LastPostTime { get; set; }
        [DisplayName("新消息数")]
        public int MessageCount { get; set; }
        [DisplayName("角色ID")]
        public int RoleID { get; set; }
    }
}