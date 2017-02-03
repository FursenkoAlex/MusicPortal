using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MusicPortal.Models
{
    public class MusicPortalContext:DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Ganre> Ganers { get; set; }
        public DbSet<Salt> Salts { get; set; }
    }
}