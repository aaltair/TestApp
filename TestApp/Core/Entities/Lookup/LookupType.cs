using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Interfaces;

namespace TestApp.Core.Entities.Lookup
{
    public class LookupType : IBaseEntity , ISoftDelete, ITranslateEntity
    {
   
         public int LookupTypeId { get; set; }
         public string LookupTypeName { get; set; }
        public ICollection<LookupItem> LookupItems { get; set; }
        public string LookupTypeDescription { get; set; }
        public int? Order { get; set; }
        public string CreatedBy { get  ; set  ; }
        public DateTime CreatedOn { get  ; set  ; }
        public string UpdatedBy { get  ; set  ; }
        public DateTime? UpdateOn { get  ; set  ; }
        public bool IsDeleted { get  ; set  ; }
        public string Translate { get  ; set  ; }
    }
}
