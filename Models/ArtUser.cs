using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace ArtGalleryAPI.Models
{
    public partial class ArtUser
    {
        public int Uid { get; set; }
        public string Uname { get; set; }
        public string Password { get; set; }
    }
}
