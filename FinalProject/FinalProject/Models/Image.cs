using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public Designer Designer { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
