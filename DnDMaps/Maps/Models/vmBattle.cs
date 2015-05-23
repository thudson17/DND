using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maps.Models
{
    public class vmBattle
    {
        public vmBattle()
        {
            this.PlayerTeam = new List<Encounter_Character>();
            this.MonsterTeam =  new List<Encounter_Monster>();
        }

        [DisplayName("Encounter Identifier")]
        public int Encounter_ID { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Encounter_Character> PlayerTeam { get; set; }
        public List<Encounter_Monster> MonsterTeam { get; set; }
    }
}