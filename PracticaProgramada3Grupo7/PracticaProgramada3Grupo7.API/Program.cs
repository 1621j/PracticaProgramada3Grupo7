using Microsoft.EntityFrameworkCore;
using PracticaProgramada3Grupo7.DAL.Data;
using PracticaProgramada3Grupo7.DAL.Repositorios;
using PracticaProgramada3Grupo7.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<VotanteRepositorio>();
builder.Services.AddScoped<PartidoPoliticoRepositorio>();
builder.Services.AddScoped<VotoRepositorio>();

builder.Services.AddScoped<VotanteService>();
builder.Services.AddScoped<PartidoPoliticoService>();
builder.Services.AddScoped<VotacionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
