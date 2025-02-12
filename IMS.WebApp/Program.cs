using IMS.Plugins.InMemory;
using IMS.UseCases.Inventories;
using IMS.UseCases.Inventories.interfaces;
using IMS.UseCases.PluginInterfaces;
using IMS.WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();

builder.Services.AddSingleton<IInventoryRepository, InventoryRepository>();//单例服务的实例在应用程序的整个生命周期内只创建一次，并在所有请求之间共享

builder.Services.AddTransient<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>();//瞬时服务每次请求时都会创建一个新的实例。这种生命周期适用于轻量级无状态服务
builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();

//builder.Services.AddScoped<IViewInventoriesByNameUseCase, ViewInventoriesByNameUseCase>(); //作用域服务的实例在每个请求中创建一次，并在每个请求之间共享,生命周期与请求/信道的生命周期相同

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();