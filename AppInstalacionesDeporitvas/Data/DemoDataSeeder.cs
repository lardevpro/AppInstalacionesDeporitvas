using AppInstalacionesDeporitvas.Enums; // Importamos los Enums para usarlos aquí
using Microsoft.EntityFrameworkCore;
using AppInstalacionesDeporitvas.Models;

namespace AppInstalacionesDeporitvas.Data;

public static class DemoDataSeeder
{
    public static async Task InicializarAsync(DeporteDbContext db)
    {
        if (await db.Reservas.AnyAsync())
        {
            return;
        }

        var carlos = new Cliente
        {
            NombreCompleto = "Carlos Mendoza",
            CorreoElectronico = "carlos.mendoza@email.com",
            Telefono = "600123456"
        };

        var lucia = new Cliente
        {
            NombreCompleto = "Lucía Gómez",
            CorreoElectronico = "lucia.gomez@email.com",
            Telefono = "611987654"
        };

        var padel = new InstalacionDeportiva
        {
            Nombre = "Pista de Pádel Cristal 1",
            TipoDeporte = TipoDeporte.Padel, // Usando el Enum TipoDeporte
            CapacidadMaxima = 4,
            PrecioPorHora = 12.50m,
            Disponible = true
        };

        var tenis = new InstalacionDeportiva
        {
            Nombre = "Pista de Tenis Rápida",
            TipoDeporte = TipoDeporte.Tenis, // Usando el Enum TipoDeporte
            CapacidadMaxima = 2,
            PrecioPorHora = 15.00m,
            Disponible = true
        };

        var futbol = new InstalacionDeportiva
        {
            Nombre = "Campo de Fútbol 7 Césped",
            TipoDeporte = TipoDeporte.Futbol, // Usando el Enum TipoDeporte
            CapacidadMaxima = 14,
            PrecioPorHora = 45.00m,
            Disponible = true
        };

        db.Clientes.AddRange(carlos, lucia);
        db.InstalacionesDeportivas.AddRange(padel, tenis, futbol);
        await db.SaveChangesAsync();

        db.Reservas.AddRange(
            new Reserva
            {
                ClienteId = carlos.Id,
                InstalacionId = padel.Id,
                Fecha = new DateTime(2026, 11, 01),
                HoraInicio = new TimeSpan(18, 0, 0),
                HoraFinalizacion = new TimeSpan(19, 30, 0),
                ImporteTotal = 18.75m,
                EstadoReserva = EstadoReseva.Confirmada // Usando el Enum EstadoReseva
            },
            new Reserva
            {
                ClienteId = lucia.Id,
                InstalacionId = futbol.Id,
                Fecha = new DateTime(2026, 11, 02),
                HoraInicio = new TimeSpan(20, 0, 0),
                HoraFinalizacion = new TimeSpan(21, 0, 0),
                ImporteTotal = 45.00m,
                EstadoReserva = EstadoReseva.Confirmada // Usando el Enum EstadoReseva
            },
            new Reserva
            {
                ClienteId = carlos.Id,
                InstalacionId = tenis.Id,
                Fecha = new DateTime(2026, 11, 03),
                HoraInicio = new TimeSpan(10, 0, 0),
                HoraFinalizacion = new TimeSpan(12, 0, 0),
                ImporteTotal = 30.00m,
                EstadoReserva = EstadoReseva.Pendiente // Usando el Enum EstadoReseva
            }
        );
        await db.SaveChangesAsync();

        // Recuperamos la reserva específica usando el Enum en los filtros
        var reservaTenisCarlos = await db.Reservas
            .SingleAsync(r =>
                r.ClienteId == carlos.Id
                && r.InstalacionId == tenis.Id
                && r.EstadoReserva == EstadoReseva.Pendiente);

        // Modificación segura utilizando el Enum
        reservaTenisCarlos.EstadoReserva = EstadoReseva.Confirmada;

        await db.SaveChangesAsync();
    }
}
