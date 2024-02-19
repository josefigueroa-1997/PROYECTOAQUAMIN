using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class Comuna
    {
        public Comuna()
        {
            Direccions = new HashSet<Direccion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdSector { get; set; }

        public virtual Sector IdSectorNavigation { get; set; } = null!;
        public virtual ICollection<Direccion> Direccions { get; set; }
    }
}
