using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var rabbitHost = builder.Configuration["RabbitMqSettings:Host"];

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitHost, "/");

        // Docker loglarında hata görmemek için
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();

app.UseHttpsRedirection();

app.Run();