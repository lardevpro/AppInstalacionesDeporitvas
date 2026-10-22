using System;
using System.Collections.Generic;
using System.Text;
using AppInstalacionesDeporitvas.Enums;

namespace AppInstalacionesDeporitvas.Models
{
    public class Cliente 
    {
        public int Id { get; set; } //clave primaria
        public string NombreCompleto { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        /// Propiedad de navegación inversa (Un cliente puede realizar muchas reservas)
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

    public class InstalacionDeportiva
    {
        public int Id { get; set; } //clave primaria
        public string Nombre { get; set; }
        public TipoDeporte TipoDeporte { get; set; } // Padel, Tenis, Futbol, Natacion
        public int CapacidadMaxima { get; set; }
        public decimal PrecioPorHora { get; set; }
        public Boolean Disponible { get; set; }

        // Propiedad de navegación inversa (Una instalación puede tener muchas reservas)
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

    public class Reserva
    {
        public int Id { get; set; } //clave primaria
        public int ClienteId { get; set; } //clave foranea
        public int InstalacionId { get; set; } //clave foranea
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFinalizacion { get; set; }
        public decimal ImporteTotal { get; set; }
        public EstadoReseva EstadoReserva { get; set; } // Pendiente, Confirmada, Cancelada, Finalizada

        // Propiedades de navegación hacia las entidades maestras
        public Cliente Cliente { get; set; } = null!;
        public InstalacionDeportiva Instalacion { get; set; } = null!;
    }
}
