using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OriginTechDemo.Application.Interfaces.Services;
using OriginTechDemo.Application.Mapping;
using OriginTechDemo.Application.Services;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using OriginTechDemo.Domain.ScoreCalculators;
using OriginTechDemo.Domain.ScoreRules.SharedRules;
using OriginTechDemo.Infra.Services;
using OriginTechDemo.Middlewares;

namespace OriginTechDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OriginTechDemo", Version = "v1" });
            });

            //Add AutoMapper
            services.AddSingleton(new AutoMapper.MapperConfiguration(config =>
            {

                config.AddProfile<HouseInformationViewModelToHouseInformationProfile>();
                config.AddProfile<VehicleInformationViewModelToVehicleInformationProfile>();
                config.AddProfile<UserInformationViewModelToUserInformationProfile>();

            }).CreateMapper());

            //Add LocalCache
            services.AddMemoryCache();

            //Add DI
            //Application Services
            services.AddScoped<IRiskProfileService, RiskProfileService>();

            //Infra Services
            services.AddSingleton<IExternalConfigurationService, ExternalConfigurationService>();
            services.AddSingleton<ILoggingService, ConsoleLoggingService>();

            //Calculators
            services.AddScoped<ILifeScoreCalculator, LifeScoreCalculator>();
            services.AddScoped<IDisabilityScoreCalculator, DisabilityScoreCalculator>();
            services.AddScoped<IHouseScoreCalculator, HouseScoreCalculator>();
            services.AddScoped<IVehicleScoreCalculator, VehicleScoreCalculator>();

            //LifeRules
            services.AddSingleton<IScoreRule, Domain.ScoreRules.LifeRules.AgeOver60Rule>();
            services.AddSingleton<IScoreRule, Domain.ScoreRules.LifeRules.DependentsNumberIsOneOrMoreRule>();
            services.AddSingleton<IScoreRule, Domain.ScoreRules.LifeRules.MaritalStatusIsMarriedRule>();

            //DisabilityRules
            services.AddSingleton<IScoreRule, Domain.ScoreRules.DisabilityRules.IncomeIsZeroRule>();
            services.AddSingleton<IScoreRule, Domain.ScoreRules.DisabilityRules.AgeOver60Rule>();
            services.AddSingleton<IScoreRule, Domain.ScoreRules.DisabilityRules.HouseIsMortgagedRule>();
            services.AddSingleton<IScoreRule, Domain.ScoreRules.DisabilityRules.DependentsNumberIsOneOrMoreRule>();
            services.AddSingleton<IScoreRule, Domain.ScoreRules.DisabilityRules.MaritalStatusIsMarriedRule>();

            //HouseRules
            services.AddSingleton<IScoreRule, Domain.ScoreRules.HouseRules.HouseIsPartOfUsersBelongingsRule>();
            services.AddSingleton<IScoreRule, Domain.ScoreRules.HouseRules.HouseIsMortgagedRule>();

            //VehicleRules
            services.AddSingleton<IScoreRule, Domain.ScoreRules.VehicleRules.VehicleIsPartOfUsersBelongingsRule>();
            services.AddSingleton<IScoreRule, Domain.ScoreRules.VehicleRules.VehicleIsAtLeastFiveYearsOldRule>();

            //SharedRules
            services.AddSingleton<IScoreRule, AgeUnder30Rule>();
            services.AddSingleton<IScoreRule, AgeBetween30and40Rule>();
            services.AddSingleton<IScoreRule, IncomeOver200kRule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OriginTechDemo v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
