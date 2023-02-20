using EventsAPI.Service.Dto;
using EventsAPI.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAPI.Service.Interface
{
    public interface ICityEventService
    {
        Task<bool> AddEvent(CityEventDto cityEvent);
        Task<bool> EditEvent(CityEventDto cityEvent, double idEvent);
        Task<bool> RemoveEvent(double idEvent);
        Task<List<CityEventDto>> SearchEventByTitle(string title);
        Task<List<CityEventDto>> SearchEventByLocalAndDate(string local, DateTime dateHourEvent);
        Task<List<CityEventDto>> SearchEventByPriceRangeAndDate(decimal minPrice, decimal maxPrice, DateTime date);
    }
}
