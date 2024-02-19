using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class Sector
    {
        public Sector()
        {
            Comunas = new HashSet<Comuna>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Comuna> Comunas { get; set; }
    }
}
