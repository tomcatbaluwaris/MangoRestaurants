using AutoMapper;
using Mango.Services.ProductAPI;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddControllers();

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {

                    options.Authority = "http://localhost:17809";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false
                    };

                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "mango");
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mango.Services.ProductAPI", Version = "v1" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Enter 'Bearer' [space] and your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="oauth2",
                            Name="Bearer",
                            In=ParameterLocation.Header
                        },
                        new List<string>()
                    }

                });
            });

            
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
 var app = builder.Build();
            // if (env.IsDevelopment())
            // {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mango.Services.ProductAPI v1"));
            // }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
       
//////
// var builder = WebApplication.CreateBuilder(args);
// var mapper = MappingConfig.RegisterMaps().CreateMapper();
// builder.Services.AddSingleton(mapper);
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// // Add services to the container.
// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// builder.Services.AddControllers();
// builder.Services.AddScoped<IProductRepository, ProductRepository>();
// // Add services to the container.
//
// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//
// var app = builder.Build();
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         // base-address of your identityserver
//         options.Authority = "http://localhost:17809";
//         // audience is optional, make sure you read the following paragraphs
//         // to understand your options
//         options.TokenValidationParameters.ValidateAudience = false;
//         // it's recommended to check the type header to avoid "JWT confusion" attacks
//         options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
//     });
// //Add authorization for testing Authorization
// // builder.Services.AddAuthorization(options => {
// //     options.AddPolicy("ApiScope", policy =>
// //     {
// //         policy.RequireAuthenticatedUser();
// //         policy.RequireClaim("scope", "mango");
// //     });
// // });
// // Add services to the container.
// builder.Services?.AddControllersWithViews();
// builder.Services.AddSwaggerGen(swager =>
// {
//     swager.SwaggerDoc("v1", new OpenApiInfo());
//     swager.EnableAnnotations();
//     swager.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
//     {
//         Description = "@Enter Bearer [space] and token",
//         Name = "Authorization",
//         In = ParameterLocation.Header,
//         Type = SecuritySchemeType.ApiKey,
//         Scheme = "Bearer"
//     });
//     var requirement = new OpenApiSecurityRequirement();
//     requirement.Add(new OpenApiSecurityScheme
//     {
//         Reference = new OpenApiReference
//         {
//             Id = "Bearer",
//             Type = ReferenceType.SecurityScheme
//         },
//         Scheme = "auth2",
//         Name = "Bearer",
//         In = ParameterLocation.Header
//     }, new List<string>());
//     swager.AddSecurityRequirement(requirement);
// });
//
// app.UseHttpsRedirection();
//
// app.UseStaticFiles();
//
// app.UseRouting();
//
// app.UseAuthorization();
//
// app.UseAuthentication();
//
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");
//
// app.Run();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
//
// app.UseAuthorization();
// app.UseAuthentication();
// app.MapControllers();
// app.Run();
