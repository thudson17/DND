using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Maps.Models
{

    //simple view model for the maps poco
    public class vmMap
    {

        [DisplayName("Map Identifier")]
        public int Map_ID { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Background IMG")]
        public string Background_IMG_Path { get; set; }

        [DisplayName("Width (Squares)")]
        [Required]
        public int Width_Squares { get; set; }

        [DisplayName("Height (Squares)")]
        [Required]
        public int Height_Squares { get; set; }

        [DisplayName("Sort Order")]
        [Required]
        public int SortOrder { get; set; }

        [Required]
        public bool Active { get; set; }

        public HttpPostedFileBase Background_IMG_Upload { get; set; }

    }
}