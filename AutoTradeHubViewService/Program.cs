using AutoTradeHubViewService.Data;
using AutoTradeHubViewService.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();
builder.Services.AddScoped<IRabbitMqGetMsgService, RabbitMqGetMsgService>();

var app = builder.Build();

MyConfig.CloudAMQPUri = builder.Configuration.GetConnectionString("CloudAMQPUri");
MyConfig.AppURL = "https://localhost:44375/"; //хз как взять из настроек

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
