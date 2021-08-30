using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Interfaces;

namespace TestApp.Core.Entities.Lookup
{
    public class LookupItem : IBaseEntity, ISoftDelete, ITranslateEntity
    {
        public int LookupItemId { set; get; }
        public string LookupItemName { set; get; }

        public string LookupItemCode { set; get; }

        public string Description { set; get; }
        public int? ParentLookupItemId { set; get; }
        public LookupItem ParentLookupItem { set; get; }
        public int LookupTypeId { set; get; }
        public LookupType LookupType { set; get; }
        public int? Order { get; set; }
        public string Translate { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateOn { get; set; }
    }
}
