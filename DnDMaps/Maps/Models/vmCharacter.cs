using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maps.Models
{
    public class vmCharacter
    {
        [DisplayName("Character Identifier")]
        public int Character_ID { get; set; }

        [DisplayName("Player")]
        [Required]
        public int Player_ID { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Avatar")]
        public string Avatar_IMG { get; set; }

        [Required]
        [DisplayName("Player ID")]
        public int PlayerID { get; set; }


    }
}