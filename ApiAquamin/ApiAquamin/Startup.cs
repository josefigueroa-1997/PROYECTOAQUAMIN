using ApiAquamin.Models;
using ApiAquamin.Services;
using Microsoft.EntityFrameworkCore;

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
        // Add controllers and other services
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Add database context
        services.AddDbContext<AQUAMINContext>(options => options.UseSqlServer(Configuration.GetConnectionString("cadenasql")));
        // Configure CORS
        /*services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });*/
        // Add custom services
        AddCustomServices(services);
    }

    private void AddCustomServices(IServiceCollection services)
    {
        // Add custom services
        services.AddScoped<SectorService>();
        services.AddScoped<ComunaService>();
        services.AddScoped<RolService>();
        services.AddScoped<UsuarioService>();
        services.AddScoped<ProductoService>();
        services.AddScoped<VentaService>();
        services.AddScoped<EjecutarSP>();
        services.AddScoped<Conexion>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            //AL SUNIR API AL HOSTING ELIMINAR LAS 2 LINEAS SIGUIENTES Y HABILITAR LA TERCERA
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseDeveloperExceptionPage();
        }
        // Enable CORS
        //app.UseCors("AllowAnyOrigin");
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}