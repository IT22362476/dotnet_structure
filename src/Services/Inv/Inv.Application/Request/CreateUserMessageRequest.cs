using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Application.Request
{
    public class CreateUserMessageRequest
    {
        public int SenderUserSerialID { get; set; }
        public List<int>? UserMsgGrpSerialIDs { get; set; }
        public List<int>? ReceiverUserSerialIDs { get; set; }
        public string? MessageText { get; set; }
        [Required]
        public int MessageTypeId { get; set; }
    }
}
