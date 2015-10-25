using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justsay.Models
{
    [MetadataType(typeof(CommentMetadata))]
    public partial class Comment : IModel
    {

        private partial class CommentMetadata
        {
            [DisplayName("系统编号")]
            public int ID { get; set; }
            [DisplayName("用户编号")]
            public int MemberID { get; set; }
            [DisplayName("真实姓名")]
            public string ShowName { get; set; }
            [DisplayName("评论时间")]
            public System.DateTime Time { get; set; }
            [DisplayName("内容")]
            [Required(ErrorMessage = "请输入内容")]
            [StringLength(1000,MinimumLength=5, ErrorMessage = "5个字以上")]
            public string Content { get; set; }
            [DisplayName("文章编号")]
            public int FunnyID { get; set; }
            [DisplayName("顶")]
            public int Up { get; set; }

            public virtual Funny Funny { get; set; }
            public virtual Member Member { get; set; }
        }

    }
}
