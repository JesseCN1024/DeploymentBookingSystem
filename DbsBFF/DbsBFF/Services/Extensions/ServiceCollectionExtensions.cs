using DbsBFF.Services.Interfaces;

namespace DbsBFF.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBffServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add User Service
            services.AddHttpClient<IUserService, UserService>(client =>
            {
                client.BaseAddress = new Uri(configuration["ServiceEndpoints:UserService:BaseUrl"]!);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());

            // Add Environment Service
            //services.AddHttpClient<IEnvironmentService, EnvironmentService>(client =>
            //{
            //    client.BaseAddress = new Uri(configuration["ServiceUrls:EnvironmentService"]!);
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //})
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());

            //// Add Booking Service
            //services.AddHttpClient<IBookingService, BookingService>(client =>
            //{
            //    client.BaseAddress = new Uri(configuration["ServiceUrls:BookingService"]!);
            //    client.DefaultRequestHeaders.Add("Accept", "application/json");
            //})
            //.AddPolicyHandler(GetRetryPolicy())
            //.AddPolicyHandler(GetCircuitBreakerPolicy());

            return services;
        }

        //private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        //{
        //    return HttpPolicyExtensions
        //        .HandleTransientHttpError()
        //        .WaitAndRetryAsync(3, retryAttempt =>
        //            TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        //}

        //private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        //{
        //    return HttpPolicyExtensions
        //        .HandleTransientHttpError()
        //        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
        //}
    }
}
