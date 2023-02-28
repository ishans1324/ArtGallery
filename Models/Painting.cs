using System;
using System.Collections.Generic;

#nullable disable

namespace ArtGalleryAPI.Models
{
    public partial class Painting
    {
        public int Pid { get; set; }
        public int? ArtistId { get; set; }
        public DateTime? Year { get; set; }
        public string Title { get; set; }
        public int? Price { get; set; }
        public string StyleOfArt { get; set; }

        public virtual Artistishan Artist { get; set; }
    }
}
