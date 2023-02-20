using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using EventsAPI.Service;
using EventsAPI.Service.Entity;
using EventsAPI.Service.Interface;
using MySqlConnector;

namespace EventsAPI.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository
    {
        private string _stringConnection { get; set; }

        public CityEventRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }

        public async Task<bool> AddEvent(CityEventEntity cityEvent)
        {
            string query = "INSERT INTO CityEvent (title, description, dateHourEvent, local, address, price, status)" +
                "VALUES (@title, @description, @dateHourEvent, @local, @address, @price, @status)";

            DynamicParameters parametros = new(cityEvent);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<bool> EditEvent(CityEventEntity cityEvent, double id)
        {
            string query = "UPDATE CityEvent SET title = @title, description = @description, dateHourEvent = @dateHourEvent," +
                "local = @local, address = @address, price = @price, status = @status) WHERE id = @idEvent";

            var parameters = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price,
                cityEvent.Status,
                idEvent = id
            });

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parameters);

            return linhasAfetadas > 0;
        }

        public async Task<bool> RemoveEvent(double idEvent)
        {
            string query = "DELETE FROM CityEvent WHERE idEvent = @idEvent";

            DynamicParameters parametros = new(idEvent);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, parametros);

            return linhasAfetadas > 0;
        }

        public async Task<List<CityEventEntity>> SearchEventByTitle(string title)
        {
            string query = $"SELECT * FROM CityEvent WHERE title = @title";

            DynamicParameters parametro = new();
            parametro.Add("title", title);

            using MySqlConnection conn = new(_stringConnection);

            return (await conn.QueryAsync<CityEventEntity>(query)).ToList();
        }

        public async Task<List<CityEventEntity>> SearchEventByLocalAndDate(string local, DateTime dateHourEvent)
        {
            string query = $"SELECT * FROM CityEvent WHERE local = @local AND date = @dateHourEvent";

            DynamicParameters parametro = new();
            parametro.Add("local", local);
            parametro.Add("date", dateHourEvent);

            using MySqlConnection conn = new(_stringConnection);

            return (await conn.QueryAsync<CityEventEntity>(query)).ToList();
        }

        public async Task<List<CityEventEntity>> SearchEventByPriceRangeAndDate(decimal minPrice, decimal maxPrice, DateTime date)
        {
            string query = "SELECT * FROM CityEvent where Date(dateHourEvent) = @date and price between @minPrice and @maxPrice";
            DynamicParameters parametros = new();
            parametros.Add("date", date);
            parametros.Add("minPrice", minPrice);
            parametros.Add("maxPrice", maxPrice);
            using MySqlConnection conn = new(_stringConnection);
            return (await conn.QueryAsync<CityEventEntity>(query, parametros)).ToList();
        }
    }
}
