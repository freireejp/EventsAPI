using EventsAPI.Service.Dto;
using EventsAPI.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAPI.Service.Interface
{
    public interface ICityEventRepository
    {
        Task<bool> AddEvent(CityEventEntity cityEvent);
        Task<bool> EditEvent(CityEventEntity cityEvent, double idEvent);
        Task<bool> RemoveEvent(double idEvent);
        Task<List<CityEventEntity>> SearchEventByTitle(string title);
        Task<List<CityEventEntity>> SearchEventByLocalAndDate(string local, DateTime dateHourEvent);
        Task<List<CityEventEntity>> SearchEventByPriceRangeAndDate(decimal minPrice, decimal maxPrice, DateTime date);
    }
}
