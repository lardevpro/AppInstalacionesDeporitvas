using AppInstalacionesDeporitvas.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ReservaInstalaciones.DTOs
{
    // DTO de Salida para Detalles de Instalaciones
    public class InstalacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public TipoDeporte TipoDeporte { get; set; } 
        public int CapacidadMaxima { get; set; }
        public decimal PrecioPorHora { get; set; }
        public bool Disponible { get; set; }
        public int TotalReservasAsociadas { get; set; }
    }

    // DTO de Entrada para Guardar/Modificar Instalación
    public class GuardarInstalacionDto
    {
        /*
            [Required] hace que rellenar ese campo sea obligatorio
            [StringLength(tamaño)] le dará un máximo de carácteres a ese campo
        */
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        /*
            RegularExpresion() hará que solo se puedan rellenar los campos con unos carácteres concretos 
            en este caso (pádel|tenis|fútbol|natación) y en caso de no sera ninguna enviará un mensaje de error
            ErrorMessage = "Deporte no válido."
        */
        [Required]
        [RegularExpression("^(pádel|tenis|fútbol|natación)$", ErrorMessage = "Deporte no válido.")]
        public TipoDeporte TipoDeporte { get; set; }

        //[Range(min, max)] hará que el rango esté entre 1 y 500
        [Range(1, 500)]
        public int CapacidadMaxima { get; set; }

        [Range(0.01, 1000.00)]
        public decimal PrecioPorHora { get; set; }
        public bool Disponible { get; set; } = true;
    }

    // DTO de Salida Completo para Reservas
    public class ReservaDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public int InstalacionId { get; set; }
        public string NombreInstalacion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string HoraInicio { get; set; } = string.Empty;
        public string HoraFinalizacion { get; set; } = string.Empty;
        public decimal ImporteTotal { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    // DTO de Entrada Estricto para Creación de Reservas
    public class CrearReservaDto
    {
        [Range(1, int.MaxValue)]
        public int ClienteId { get; set; }

        [Range(1, int.MaxValue)]
        public int InstalacionId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string HoraInicio { get; set; } = string.Empty; // Formato "HH:mm"

        [Required]
        public string HoraFinalizacion { get; set; } = string.Empty; // Formato "HH:mm"
    }

    // DTO de Entrada para Cambiar Estado
    public class CambiarEstadoReservaDto
    {
        [Required]
        [RegularExpression("^(Pendiente|Confirmada|Cancelada|Finalizada)$")]
        public string Estado { get; set; } = string.Empty;
    }
}