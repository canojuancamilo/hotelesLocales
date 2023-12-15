﻿using apisHotel.Interfaces;
using apisHotel.Models;
using apisHotel.Repositorys;

namespace apisHotel.Services
{
    public class HabitacionService : IHabitacionService
    {
        private readonly IHabitacionRepository _habitacionRepository;

        public HabitacionService(IHabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }

        public void AgregarHabitacionHotel(int HotelId, Habitacion habitacion)
        {
            _habitacionRepository.AgregarHabitacionHotel(HotelId, habitacion);
        }

        public Habitacion ObtenerDetalleHabitacion(int id)
        {
            return _habitacionRepository.ObtenerDetalleHabitacion(id);
        }

        public void ActualizarHabitacion(Habitacion habitacion)
        {
            _habitacionRepository.ActualizarHabitacion(habitacion);
        }
    }
}
