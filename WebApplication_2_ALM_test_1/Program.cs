using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using WebApplication_2_ALM_test_1;
using WebApplication_2_ALM_test_1.Models;
using WebApplication_2_ALM_test_1.Repository;
using WebApplication_2_ALM_test_1.Services;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������ ����������� �� ������������
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ����������� ��������
builder.Services.AddSingleton(new Database(connectionString));
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<ProjectService>(); 
builder.Services.AddScoped<StepRepository>();
builder.Services.AddScoped<StepService>();
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<OrganisationRepository>();
builder.Services.AddScoped<OrganisationService>();
builder.Services.AddScoped<ProfileRepository>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<StatusRepository>();
builder.Services.AddScoped<StatusService>();
builder.Services.AddScoped<WorkTimeRepository>();
builder.Services.AddScoped<WorkTimeService>();

// ����������� ������������
builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// ��������� Swagger/OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My API",
        Description = "API ��� ���������� ���������",
        Contact = new OpenApiContact
        {
            Name = "Dmitry",
            Email = "Ir-Dmitry73@yandex.ru"
        }
    });

    // ��������� ������������ XML ��� ������������
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// ������ ������������ �� �����
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

// ��������� ��������� ��������� HTTP-��������
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
