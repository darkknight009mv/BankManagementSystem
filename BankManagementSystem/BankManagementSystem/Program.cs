using BankManagementSystem;
using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using BankManagementSystem.Repository;
using BankManagementSystem.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankDbContext>(options => options.UseSqlServer(@"Data Source = 192.168.2.185\beta; Initial Catalog = SmallOffice; Integrated Security = True; Encrypt = False"));
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();


builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services .AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();

//public delegate IAccountRepo ServiceResolver(AccountType accountType);
builder.Services.AddScoped<SavingUtil>();
builder.Services.AddScoped<CurrentUtil>();
builder.Services.AddScoped<Func<AccountType,IAccountUtils>>(ServiceProvider => accountType =>
{
    switch (accountType)
    {
        case AccountType.Savings:
            return ServiceProvider.GetService<SavingUtil>();
        case AccountType.Current:
            return ServiceProvider.GetService<CurrentUtil>();
        default:
            return ServiceProvider.GetService<CurrentUtil>();
    }
}
);
builder.Services.AddAutoMapper(typeof(BankManagementProfile));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
      builder =>
      {
          builder.WithOrigins("http://localhost:44351", "http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
      });
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
