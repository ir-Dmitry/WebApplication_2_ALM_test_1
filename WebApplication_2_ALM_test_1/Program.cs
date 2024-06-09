using Microsoft.OpenApi.Models;
using WebApplication_2_ALM_test_1;
using WebApplication_2_ALM_test_1.Repository;
using WebApplication_2_ALM_test_1.Services;

var builder = WebApplication.CreateBuilder(args);

// Получение строки подключения из конфигурации
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Регистрация сервисов
builder.Services.AddSingleton(new Database(connectionString));
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<ProjectService>();

// Регистрация контроллеров
builder.Services.AddControllers();

// Настройка Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "API для управления проектами",
        Contact = new OpenApiContact
        {
            Name = "Dmitry",
            Email = "Ir-Dmitry73@yandex.ru"
        }
    });

    // Включение комментариев XML для документации
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Чтение конфигурации из файла
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// Настройка конвейера обработки HTTP-запросов
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
