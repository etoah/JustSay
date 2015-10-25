using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justsay.Models
{
    [MetadataType(typeof(FunnyMetadata))]
    public partial class Funny : IModel
    {
        public class FunnyMetadata
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
            [DisplayName("标题")]
            [StringLength(30, ErrorMessage = "请输入30个字以内")]
            public string Title { get; set; }
            [DisplayName("顶")]
            public int Up { get; set; }
            [DisplayName("发表时间")]
            public System.DateTime Time { get; set; }
            [DisplayName("评论数")]
            public int CommentCount { get; set; }
            [DisplayName("图片地址")]
            public string ImgUrl { get; set; }
            [DisplayName("表白ID")]
            public Nullable<int> ConfessID { get; set; }

            public virtual ICollection<Comment> Comments { get; set; }
            public virtual Member Member { get; set; }
        }
    }
}
