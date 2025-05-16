using Joke.Server.Services;

namespace Joke.Server.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<JokeService>();
            return services;
        }
    }
}
