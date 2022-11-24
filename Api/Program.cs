var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Police1",
        policy  =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});


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

app.UseCors("Police1");

app.MapControllers();

app.Run();
