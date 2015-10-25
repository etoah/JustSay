using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Justsay.Models
{
    [MetadataType(typeof(MessageMetadata))]
    public partial class Message : IModel
    {
        public partial class MessageMetadata
        {
            public int ID { get; set; }
            public int FromID { get; set; }
            public int ToID { get; set; }
            public string FromName { get; set; }
            public string ToName { get; set; }
            public bool IsNew { get; set; }
            public System.DateTime Time { get; set; }
            [DisplayName("信息")]
            [Required(ErrorMessage = "请输入内容")]
            [StringLength(500, ErrorMessage = "请输入500个字以内")]
            public string Content { get; set; }
        }
    }
}
