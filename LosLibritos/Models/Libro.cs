namespace LosLibritos.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Libro")]
    public partial class Libro
    {
        [Key]
        public int Id_Libro { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name ="Codigo Libro")]
        public string CodigoLibro { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Display(Name ="Fecha de prestamo")]
        public DateTime? FechaPrestamo { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Fecha de entrega")]
        public DateTime? FechaEntrega { get; set; }

        public bool? Estado { get; set; }
        [Display(Name = "Genero")]
        public int Id_Genero { get; set; }

        public virtual Genero Genero { get; set; }
    }
}
