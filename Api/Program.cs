using Application.Services.Client;
using System.Text;
using Application.Services.Animal;
using Application.UseCases.Animal;
using Application.UseCases.Client;
using Application.UseCases.Post;
using BackSmallBrother;
using Infrastructure.Ef;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IConnectionStringProvider, ConnectionStringProvider>();
builder.Services.AddScoped<BackSmallBrotherContextProvider>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAnimalRepository, AnimalRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IAnimalService, AnimalService>();

// Use cases clients
builder.Services.AddScoped<UseCaseFetchAllClients>();
builder.Services.AddScoped<UseCaseFetchClientById>();
builder.Services.AddScoped<UseCaseFetchClientByLogin>();
builder.Services.AddScoped<UseCaseCreateClient>();
builder.Services.AddScoped<UseCaseDeleteClient>();

// Use cases animals
builder.Services.AddScoped<UseCaseFetchAllAnimals>();
builder.Services.AddScoped<UseCaseFetchAnimalById>();
builder.Services.AddScoped<UseCaseFetchAnimalsByIdClient>();
builder.Services.AddScoped<UseCaseCreateAnimal>();
builder.Services.AddScoped<UseCaseDeleteAnimal>();
builder.Services.AddScoped<UseCaseChangeStatutDefaultAnimal>();
builder.Services.AddScoped<UseCaseChangeStatutLostAnimal>();
builder.Services.AddScoped<UseCaseFetchAnimalsByIdClientFound>();

// Use cases posts
builder.Services.AddScoped<UseCaseFetchAllPosts>();
builder.Services.AddScoped<UseCaseFetchPostById>();
builder.Services.AddScoped<UseCaseFetchLatestsPosts>();
builder.Services.AddScoped<UseCaseFetchPostsByIdAnimal>();
builder.Services.AddScoped<UseCaseFetchPostsByIdClient>();
builder.Services.AddScoped<UseCaseCreatePost>();
builder.Services.AddScoped<UseCaseDeletePost>();
builder.Services.AddScoped<UseCaseFetchAllPostsFound>();
builder.Services.AddScoped<UseCaseFetchAllPostsNotFound>();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,


        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["CookieSuper"];
            return Task.CompletedTask;
        }
    };
});


/*builder.Services.AddCors(options =>

{
    options.AddPolicy("Dev", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.WithOrigins("https://localhost:4200")
            .AllowAnyHeader()   
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}


app.UseCors("EnableCORS");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();