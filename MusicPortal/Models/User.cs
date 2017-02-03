using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MusicPortal.Models
{
    public class User
    {

        //public User()
        //{
        //    this.MusicTracks = new HashSet<Music>();
        //}
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required (ErrorMessage = "Title is requered")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The string must be contain min 3 symbol")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Title is requered")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "The string must be contain min 3 symbol")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Title is requered")]
        [StringLength(70, MinimumLength = 3, ErrorMessage = "The string must be contain min 3 symbol")]
        [Compare("Password", ErrorMessage = "Confirm passwords doesn't match. Type again!")]
        public string Password2 { get; set; }

        public virtual Salt Salt { get; set; }

        public bool StatusAdmin { get; set; }

        public bool StatusUser { get; set; }

        //public virtual ICollection<Music> MusicTracks { get; set; }
    }
}