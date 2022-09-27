using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AUS2.Core.DBObjects;
using AUS2.Core.DAL.IRepository;
using AUS2.Core.DAL.Repository;
using AUS2.Core.Utilities;
using AUS2.Core.ViewModels;
using AUS2.Core.Helper.AutoMapperSettings;
using AUS2.Core.Helper.SerilogService.Account;
using AUS2.Core.DAL.Repository.Services.ElpsService;
using AUS2.Core.Helper.Notification;
using AUS2.Core.DAL.Repository.Services.Account;
using AUS2.Core.Repository.Services.Admin;
using AUS2.AUS2.Core.DAL.Repository.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Filters;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using AUS2.Core.Repository.Services.Account;
using AUS2.Core.DAL.Repository.Services.Application;
using System.Net.Http;
using System.Net;

namespace AUS2
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

            services.AddCors();
            services.AddControllersWithViews();
            //services.AddControllers();
            //services.AddDbContext<AUS2DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AUSIIConnectionString")));
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AUSIIConnectionString"), m => m.MigrationsAssembly("AUS2")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationContext>();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);//You can set Time   
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;

            });

            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            });


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            services.AddDistributedMemoryCache();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));


            services.AddAutoMapper(typeof(MappingProfiles));

            services.AddSingleton<GeneralLogger>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //services.AddSingleton<EncryptData>();
            services.AddScoped<IElpsService, ElpsServiceHelper>();
            services.AddScoped<INotification, Notification>();
            services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ApplicationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            //services.AddScoped<ICompanyService<WebApiResponse>, CompanyService>();
            services.AddScoped<ApplicationWorkflowService>();
            //services.AddVersionedApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.ExampleFilters();

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "AUSII API",
                    Description = "AUSII Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "AUSII API",

                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                    }
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xml), true);


            });
            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
            services.AddFluentValidationRulesToSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationContext _db, IServiceProvider serviceProvider, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            _db.Database.EnsureCreated();

            app.UseSwagger();
            //var descriptionProvider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v2/swagger.json", "AUS II");
                //foreach (var description in descriptionProvider.ApiVersionDescriptions)
                //    s.SwaggerEndpoint("/swagger/v2/swagger.json", description.GroupName.ToUpperInvariant());
            });

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials()); // allow credentials

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            CreateRoles(serviceProvider, roleManager).Wait();
            CreateSuperAdmin(serviceProvider, userManager).Wait();
            CreateStates(serviceProvider).Wait();
        }

        async Task CreateRoles(IServiceProvider serviceProvider, RoleManager<ApplicationRole> roleManager)
        {
            var roles = new List<string>
            {
                Roles.Staff,
                Roles.SuperAdmin,
                Roles.Company,
                Roles.ICT,
                Roles.Reviewer,
                Roles.Supervisor,
                Roles.Manager,
                Roles.HeadUMR,
                Roles.Support
            };
            roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            foreach (var role in roles)
            {
                var name = role.Split('|');
                if (!await roleManager.RoleExistsAsync(name.FirstOrDefault()))
                    await roleManager.CreateAsync(new ApplicationRole { Name = name.FirstOrDefault(), Description = name[1] });
            }

        }

        async Task CreateSuperAdmin(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
           
            userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByEmailAsync("damilare.olanrewaju@brandonetech.com");
            if(user == null)
            {
                user = new ApplicationUser
                {
                    Email = "damilare.olanrewaju@brandonetech.com",
                    UserName = "damilare.olanrewaju@brandonetech.com",
                    PhoneNumber = "07038618978",
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now,
                    EmailConfirmed = true,
                    FirstName = "Damilare",
                    LastName = "Olanrewaju",
                    LoginCount = 1,
                    PhoneNumberConfirmed = true,
                    Status = true,
                    UpdatedBy = "system",
                    UserType = "Staff",
                    UpdatedOn = DateTime.Now,
                    LastLogin = DateTime.Now
                };
                var result = await userManager.CreateAsync(user);

                if (result.Succeeded)
                    await userManager.AddToRolesAsync(user, new[] { "Staff", "SuperAdmin" });
            }

        }

        async Task CreateStates(IServiceProvider serviceProvider)
        {

            var _context = serviceProvider.GetRequiredService<ApplicationContext>();
            if (!_context.States.Any())
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://nigeria-states-and-lga.p.rapidapi.com/lgalists"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "e712f2d5b1msh45d446e78c42f3bp14570fjsn8c5ab5bc4545" },
                        { "X-RapidAPI-Host", "nigeria-states-and-lga.p.rapidapi.com" },
                    },
                };
                using (var response = await client.SendAsync(request))
                {
                    if (response.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var obj = content.Parse<List<StateObj>>().OrderBy(x => x.State);

                        var states = obj.GroupBy(x => x.State).Select(x => new State { StateName = x.Key }).OrderBy(x => x.StateName).ToList();

                        await _context.States.AddRangeAsync(states);
                        await _context.SaveChangesAsync("system");

                        states.ForEach(x =>
                        {
                            var lgas = obj.Where(y => y.State.Equals(x.StateName)).Select(z => new LGA { LgaName = z.LGA, StateId = x.Id }).OrderBy(x => x.LgaName).ToList();

                            _context.LGAs.AddRange(lgas);

                            _context.SaveChanges();
                        });
                    }
                }
                await _context.SaveChangesAsync();
            }

            if (!_context.ApplicationTypes.Any())
            {
                var apptypes = new[] { "New", "Renew" };

                foreach(var t in apptypes)
                    _context.ApplicationTypes.Add(new ApplicationType { Name = t });

                _context.SaveChanges();
            }
        }
    }
}
