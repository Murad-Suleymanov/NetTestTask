using AutoMapper;
using Microsoft.Extensions.Configuration;
using NetTestTask.DataAccess.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using NetTestTask.DataAccess.Persistence.DBContexts;
using NetTestTask.DataAccess.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using NetTestTask.DataAccess.Abstractions.Repositories;
using NetTestTask.DataAccess.Concrete.Repositories;
using NetTestTask.DataAccess.Persistence.Repositories;
using NetTestTask.Application.Abstraction.Presentation;
using NetTestTask.Services.Implementation.Presentation;
using NetTestTask.Application.Abstraction.Services;
using NetTestTask.Services.Implementation.Services;

namespace NetTestTask.Utility.Mappings
{
    public static class DependencyInjection
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //Application
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonControllerService, PersonControllerService>();


            //Other
            var sp = services.BuildServiceProvider();

            //DataAccess
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TestDb"));
                options.EnableSensitiveDataLogging();
            }); 
            services.AddTransient<AppDbContext>();
            services.AddScoped<IPersonRepository, PersonRepository>();



            //CommonTech
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
