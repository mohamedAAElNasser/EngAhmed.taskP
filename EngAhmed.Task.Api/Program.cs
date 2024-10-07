using EngAhmed.TaskP.Application.Contracts.IAppService;
using EngAhmed.TaskP.Application.Contracts.IPersistence;
using EngAhmed.TaskP.Application.Contracts.IRepostories;
using EngAhmed.TaskP.Application.Services;
using EngAhmed.TaskP.Infrastructure;
using EngAhmed.TaskP.Infrastructure.Repositories;
using EngAhmed.TaskP.TaskIdentity;
using EngAhmed.TaskP.TaskIdentity.Extends;
using EngAhmed.TaskP.TaskIdentity.Seedings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EngAhmed.TaskP.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            #region IntegratedDb & IdentityDb & JWT Configuration
            builder.Services.AddDbContextPool<TaskIdentityDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("TaskCon")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
               
                options.Password.RequireDigit = true; 
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false; 
                options.Password.RequireLowercase = false; 
                                                           
            })
            .AddEntityFrameworkStores<TaskIdentityDbContext>()
            .AddDefaultTokenProviders();

            
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");

            var key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });



            builder.Services.AddDbContextPool<TaskDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("TaskCon")));


            #endregion

            #region Regiser Repostories
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            #endregion

            #region Regiser AppServices
            builder.Services.AddScoped<IProductAppServiceAsync, ProductAppServiceAsync>();
            builder.Services.AddScoped<IProductDevExpressReportService, ProductDevExpressReportService>();
            builder.Services.AddScoped<ICustomerAppServiceAsync, CustomerAppServiceAsync>();
            builder.Services.AddScoped<IIdentityAppServiceAsync, IdentityAppServiceAsync>();
            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                SeedAdminUser.SeedAsync(services).Wait();
            }
            app.UseHttpsRedirection();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
