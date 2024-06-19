using DynamicApplication.CORE.Services.Implementation;
using DynamicApplication.CORE.Services.Interface;
using DynamicApplication.INFRA;
using DynamicApplication.INFRA.Context;
using DynamicApplication.INFRA.Repository.Implementation;
using DynamicApplication.INFRA.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CosmosDbContext>(options =>
options.UseCosmos(connString,
                "DemoDB"
            ));

builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

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

//this seed code is meant to seed the questions types to a working db
//await Seeder.Run(app);

app.Run();
