using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public List<ImageTag> ImageTags { get; set; }
    }
}
