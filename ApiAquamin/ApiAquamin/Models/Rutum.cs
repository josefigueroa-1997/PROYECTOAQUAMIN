using System;
using System.Collections.Generic;

namespace ApiAquamin.Models
{
    public partial class Rutum
    {
        public Rutum()
        {
            RutaDespachos = new HashSet<RutaDespacho>();
        }

        public int Id { get; set; }
        public string Tiporuta { get; set; } = null!;

        public virtual ICollection<RutaDespacho> RutaDespachos { get; set; }
    }
}
