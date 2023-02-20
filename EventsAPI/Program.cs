using AutoMapper;
using EventsAPI.Infra.Data.Repository;
using EventsAPI.Service.Dto;
using EventsAPI.Service.Entity;
using EventsAPI.Service.Interface;
using EventsAPI.Service.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //builder.Services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(GeneralExceptionFilter));
            //});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
            builder.Services.AddScoped<ICityEventService, CityEventService>();
            builder.Services.AddScoped<IGenerateTokenService, GenerateTokenService>();

            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.CreateMap<CityEventEntity, CityEventDto>().ReverseMap();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            //app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}