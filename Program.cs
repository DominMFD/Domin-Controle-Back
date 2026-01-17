using Microsoft.EntityFrameworkCore;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// 🔐 Firebase configuration
// ===============================

var firebaseSection = builder.Configuration.GetSection("Firebase");

var firebaseProjectId = firebaseSection["ProjectId"];
var firebaseCredentialsPath = firebaseSection["CredentialsPath"];

if (string.IsNullOrEmpty(firebaseCredentialsPath))
{
    throw new Exception("Firebase CredentialsPath não configurado.");
}
var credentialsPath = Path.Combine(
    builder.Environment.ContentRootPath,
    firebaseCredentialsPath
);

var googleCredential = GoogleCredential.FromFile(credentialsPath);

// Firebase Admin (opcional — mantenho para futuro uso)
if (FirebaseApp.DefaultInstance == null)
{
    FirebaseApp.Create(new AppOptions
    {
        Credential = googleCredential,
        ProjectId = firebaseProjectId
    });
}

// Google Cloud Storage Client (ESSENCIAL)
builder.Services.AddSingleton(_ =>
{
    return StorageClient.Create(googleCredential);
});

// ===============================
// 🗄 Database
// ===============================

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ControlContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// ===============================
// 🧠 Services (DI)
// ===============================

builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IOxygenationService, OxygenationService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();

// ===============================
// 🌐 Controllers & Swagger
// ===============================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===============================
// 🚦 Build app
// ===============================

var app = builder.Build();

// ===============================
// 🧪 Middleware
// ===============================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
