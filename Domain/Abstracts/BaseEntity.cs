using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstracts
{
    public abstract class BaseEntity
    {
        private Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public User? CreatedBy { get; set; }
    }
}
