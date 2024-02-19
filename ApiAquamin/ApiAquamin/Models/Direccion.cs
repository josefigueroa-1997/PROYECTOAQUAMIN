using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class Direccion
    {
        public Direccion()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Calle { get; set; } = null!;
        public int Numero { get; set; }
        public int IdComuna { get; set; }

        public virtual Comuna IdComunaNavigation { get; set; } = null!;
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
