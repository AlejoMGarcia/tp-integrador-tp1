using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public abstract class Articulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioCreadorId { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime FechaCreacion { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime FechaModificacion { get; set; }

        [Display(Name = "Precio Inicial")]
        public double PrecioInicial { get; set; }

        [Display(Name = "Precio Mínimo")]
        public double PrecioMinimo { get; set; }

        [Display(Name = "Precio en Puja")]
        public double PrecioEnPuja { get; set; }

        [Display(Name = "Subir Foto artículo:")]
        [Required(ErrorMessage = "Por favor suba una imagen del artículo")]
        [NotMapped]
        public IFormFile ArchivoFoto { get; set; }

        public string NombreImagen { get; set; }
        public byte[] ArchivoImagen { get; set; }
        public string TipoImagen { get; set; }

        public int? SubastaId { get; set; }
    }
}
