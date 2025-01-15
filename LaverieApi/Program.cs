using LaverieApi.Models.Business;
using LaverieApi.Models.IDAO;
using LaverieApi.Infrastructure.DAO;
using LaverieApi.Models.Services;
using LaverieApi.Infrastructure;
using LaverieApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // React app URL
              .AllowAnyHeader()                    // Allow any HTTP headers
              .AllowAnyMethod()                    // Allow any HTTP methods (GET, POST, PUT, DELETE, etc.)
              .AllowCredentials();                 // Allow credentials if needed
    });
});

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddSingleton<ConnectionDb>(ConnectionDb.GetInstance());
builder.Services.AddScoped<IProprietaireDAO, ProprietaireDAOImp>();
builder.Services.AddScoped<ILaverieDAO, LaverieDAOImp>();
builder.Services.AddScoped<IMachineDAO, MachineDAOImp>();
builder.Services.AddScoped<ICycleTarif, CycleTarifDAOImp>();
builder.Services.AddScoped<IConfigService, GereConfig>();
builder.Services.AddScoped<GererMachine>();
builder.Services.AddScoped<GererRecette>();
builder.Services.AddScoped<IRecetteDAO, RecetteDAOImp>();
builder.Services.AddSingleton<JWTTokenManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure WebSocket options
var webSocketOptions = new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(120)
};
app.UseWebSockets(webSocketOptions);

// Add custom WebSocket middleware
app.UseWebSocketMiddleware();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply the CORS policy
app.UseCors("AllowReactApp");

app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
