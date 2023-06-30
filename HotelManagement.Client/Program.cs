using Blazored.LocalStorage;
using HotelManagement.Client;
using HotelManagement.Client.Services.Abstracts;
using HotelManagement.Client.Services.Implementations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient 
    { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiUrl")!) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IHotelRoomService, HotelRoomService>();
builder.Services.AddScoped<IHotelAmenityService, HotelAmenityService>();

await builder.Build().RunAsync();
