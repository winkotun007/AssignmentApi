using System;
using AssignmentAPI.Interface;
using AssignmentAPI.Repository;

namespace AssignmentAPI.Shared
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddBuildingRepository(this IServiceCollection services)
        {
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<ILevelRepository,LevelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IVisitorsRepository,VisitorsRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IGuestAcessRepository, GuestAccesssRepository>();
            services.AddScoped<IUserIdProvider, UserIdProvider>();
            return services;
        }

        // Add other extension methods as needed
    }
}

