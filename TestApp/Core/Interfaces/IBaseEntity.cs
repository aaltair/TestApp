using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Core.Interfaces
{
    interface IBaseEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdateOn { get; set; }
    }
}
