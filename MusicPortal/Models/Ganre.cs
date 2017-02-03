using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MusicPortal.Models
{
    public class Ganre
    {


        public int  Id { get; set; }
        [DisplayName("Genre")]
        public string GanreName { get; set; }
    }
}