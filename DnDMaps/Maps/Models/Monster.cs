//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Maps.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Monster
    {
        public Monster()
        {
            this.Encounter_Monster = new HashSet<Encounter_Monster>();
        }
    
        public int Monster_ID { get; set; }
        public string Name { get; set; }
        public string Avatar_IMG { get; set; }
    
        public virtual ICollection<Encounter_Monster> Encounter_Monster { get; set; }
    }
}