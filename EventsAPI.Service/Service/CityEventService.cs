using AutoMapper;
using EventsAPI.Service.Dto;
using EventsAPI.Service.Entity;
using EventsAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAPI.Service.Service
{
    public class CityEventService : ICityEventService
    {
        private ICityEventRepository _repository;
        private IMapper _mapper;

        public CityEventService(ICityEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AddEvent(CityEventDto cityEvent)
        {
            CityEventEntity entity = _mapper.Map<CityEventEntity>(cityEvent);
            return await _repository.AddEvent(entity);
        }

        public async Task<bool> EditEvent(CityEventDto cityEvent, double idEvent)
        {
            CityEventEntity entity = _mapper.Map<CityEventEntity>(cityEvent);
            return await _repository.EditEvent(entity, idEvent);
        }

        public async Task<bool> RemoveEvent(double idEvent)
        {
            return await _repository.RemoveEvent(idEvent);
        }

        public async Task<List<CityEventDto>> SearchEventByTitle(string title)
        {
            List<CityEventEntity> entity = await _repository.SearchEventByTitle(title);
            if (entity == null)
            {
                return null;
            }

            List<CityEventDto> cityEvents = _mapper.Map<List<CityEventDto>>(entity);
            return cityEvents;
        }

        public async Task<List<CityEventDto>> SearchEventByLocalAndDate(string local, DateTime dateHourEvent)
        {
            List<CityEventEntity> entity = await _repository.SearchEventByLocalAndDate(local, dateHourEvent);
            if (entity == null)
            {
                return null;
            }

            List<CityEventDto> cityEvents = _mapper.Map<List<CityEventDto>>(entity);
            return cityEvents;
        }

        public async Task<List<CityEventDto>> SearchEventByPriceRangeAndDate(decimal minPrice, decimal maxPrice, DateTime date)
        {
            List<CityEventEntity> entity = await _repository.SearchEventByPriceRangeAndDate(minPrice, maxPrice, date);
            if (entity == null)
            {
                return null;
            }

            List<CityEventDto> cityEvents = _mapper.Map<List<CityEventDto>>(entity);
            return cityEvents;
        }
    }
}
