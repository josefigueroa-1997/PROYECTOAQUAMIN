﻿namespace ApiAquamin.Models.Formularios
{
    public class IngresarProducto
    {
        public int IdUsuario { get; set; }
        public string Tipo_Producto { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
