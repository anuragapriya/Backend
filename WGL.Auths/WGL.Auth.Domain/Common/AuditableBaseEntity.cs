using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGL.Auth.Domain.Common
{
    public abstract class AuditableBaseEntity
    {
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
