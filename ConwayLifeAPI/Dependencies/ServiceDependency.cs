using ConwayLifeAPI.Data.Repository;
using ConwayLifeAPI.Services;
using ConwayLifeAPI.Services.Interfaces;

namespace ConwayLifeAPI.Dependencies
{
    public static class ServiceDependency
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection services)
        {
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IBoardRepository, BoardRepository>();


            //ToDo: Uncomment the following lines when the respective classes are implemented

            //services.AddScoped<IMapper, Mapper>();
            //services.AddScoped<IBoardValidator, BoardValidator>();

            return services;
        }
    }
}
