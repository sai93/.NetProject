//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErrorLogModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class UsersAndApplication
    {
        [Key]
        public int ApplicationId { get; set; }
        [Key]
        public int UserId { get; set; }
        [Required]
        public System.DateTime TimeOfAssignment { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
