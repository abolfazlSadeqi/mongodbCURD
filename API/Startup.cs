
using API.Dto;
using AutoMapper;
using DAL.Model;
using DAL.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();



            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.Configure<MgDatabaseSettings>(
                            Configuration.GetSection(nameof(MgDatabaseSettings)));

            services.AddSingleton<IMgDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MgDatabaseSettings>>().Value);

            services.AddSingleton<IMongoClient>(s =>
                    new MongoClient(Configuration.GetValue<string>("MgDatabaseSettings:ConnectionString")));

            services.AddScoped<IPersonRepository, PersonRepository>();



            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);



        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();


            app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "";
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Configure the HTTP request pipeline.

        }
    }
}
