﻿using apisHotel.Models;

namespace apisHotel.Interfaces
{
    public interface IHabitacionRepository
    {
        void AgregarHabitacionHotel(int HotelId, Habitacion hotel);
        Habitacion ObtenerDetalleHabitacion(int id);
        void ActualizarHabitacion(Habitacion hotel);
    }
}