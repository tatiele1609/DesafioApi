using DesafioApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DesafioContext>(opt => opt.UseInMemoryDatabase("DesafioList"));

//builder.Services.AddDbContext<DesafioContext>(opt => 
//    opt.UseSqlServer(@"Data Source=DESKTOP-H4KEUJA\SQLEXPRESS;Initial Catalog=Desafio2;Integrated Security=False;User ID=baseDev;Password=sung@pi_Goncalves;TrustServerCertificate=False"));
    //opt.UseSqlServer(@"Server=localhost\SQLEXPRESS;Initial Catalog=Desafio1;Persist Security Info=False;User ID=baseDev;Password=sung@pi_Goncalves;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
//SQLEXPRESS
//DESKTOP-H4KEUJA\SQLEXPRESS
    //Data Source=localhost\\SQLEXPRESS;Initial Catalog=Desafio1;Integrated Security=False;User ID=baseDev;Password=sung@pi_Goncalves;
    //Server=tcp:****.database.windows.net,1433;Initial Catalog=DbPokemon;Persist Security Info=False;User ID=****;Password=****;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
