using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Interfaces;

namespace TestApp.Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser, IBaseEntity, ISoftDelete,ITranslateEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string ProfileImg { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsDeleted { get ; set ; }
        public string CreatedBy { get ; set ; }
        public DateTime CreatedOn { get ; set ; }
        public string UpdatedBy { get ; set ; }
        public DateTime? UpdateOn { get ; set ; }
        public string Translate { get ; set ; }
    }
}
