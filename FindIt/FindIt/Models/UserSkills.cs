//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FindIt.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserSkills
    {
        public System.Guid UserSkillId { get; set; }
        public System.Guid UserInfoId { get; set; }
        public System.Guid SkillId { get; set; }
        public bool Activated { get; set; }
    
        public virtual Skills Skills { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
