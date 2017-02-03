using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace MusicPortal.Models
{
    public class Music
    {
        public int Id { get; set; }

        public string RelativePath { get; set; }

        public string AbsolutePath { get; set; }

        public string FileName { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public int GanersId { get; set; }

        public virtual Ganre Ganers { get; set; }

        public virtual User User { get; set; }
    }
}