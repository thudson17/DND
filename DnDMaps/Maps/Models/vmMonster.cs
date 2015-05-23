using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maps.Models
{
    public class vmMonster
    {
        [DisplayName("Monster Identifier")]
        public int Monster_ID { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayName("Avatar")]
        public string Avatar_IMG { get; set; }


        public HttpPostedFileBase Avatar_File { get; set; }
    }
}