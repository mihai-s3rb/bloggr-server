using Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggr.Domain.Entities
{
    public class Message : BaseEntity
    {
        public User Sender { get; set; }

        public int SenderId { get; set; }

        public User Receiver { get; set; }

        public int ReceiverId { get; set; }

        public string Content { get; set; }
    }
}
