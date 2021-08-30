using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Core.Interfaces
{
    interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
