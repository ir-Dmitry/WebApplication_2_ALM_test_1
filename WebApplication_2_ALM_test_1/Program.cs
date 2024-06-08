using WebApplication_2_ALM_test_1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//���������� ����������� ������������ ��� Swagger � ������������
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "API ��� ���������� ���������",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Dmitry",
            Email = "Ir-Dmitry73@yandex.ru"/*,
            Url = new Uri("https://your-website.com")*/
        }
    });

    // ��������� ������������ XML ��� ������������
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ����������� ��������
builder.Services.AddSingleton(new Database(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
