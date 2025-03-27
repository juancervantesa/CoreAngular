using BEseguridad.Models;
using BEseguridad.Utilidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SeguridadInformaticaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SeguridadInformatica")));

#region Implementacion de CORS

builder.Services.AddCors(options =>
{

    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });

});

#endregion

//Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = builder.Configuration["Jwt:Issuer"],
                   ValidAudience = builder.Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                   ClockSkew = TimeSpan.Zero
               });



// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        opt.JsonSerializerOptions.WriteIndented = true;
    })
                ;
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new() { Title = "BEseguridad", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name= "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat= "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    });
   c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
   {
       {
           new Microsoft.OpenApi.Models.OpenApiSecurityScheme
           {
               Reference = new Microsoft.OpenApi.Models.OpenApiReference
               {
                   Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                   Id = "Bearer"
               }
           },
           new string[] { }
       }
   });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


var app = builder.Build();

//app.MapGet("/", (HttpContext context) =>
//{
//    context.Response.Redirect("/swagger/index.html", permanent: false);
//}
//);

app.Use(async (context, next) =>
{
    if (context.Request.Path=="/")
    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return;
    }
    await next();

});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BEseguridad v1"));
    app.UseSwagger();


}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
