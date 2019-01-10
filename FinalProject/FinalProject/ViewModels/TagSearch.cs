using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.ViewModels
{
    public class TagSearch
    {
        public int ImageId { get; set; }
        public int TagId { get; set; }
        public List<ImageTag> ImageTags { get; set; }
        public Tag tag { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public List<string> Types { get; set; }

    }
}
