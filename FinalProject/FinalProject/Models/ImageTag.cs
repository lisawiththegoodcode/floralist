using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ImageTag
    {
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
