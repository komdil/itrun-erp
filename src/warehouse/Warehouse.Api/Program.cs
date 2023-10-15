using Application;
using Infrastructure;
using Warehouse.Api.Extensions;
using Warehouse.Api.Filters;
using Warehouse.Api.AuthMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string appSettingsToken = "MyKeaskJKASJDKJASDUKKJSA781273,44$";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
    .GetBytes(appSettingsToken!)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Services.AddAuthorization(op =>
{
    op.AddPolicy("admin", p => p.RequireRole("admin"));
    op.AddPolicy("superadmin", p => p.RequireRole("superadmin"));
});


builder.Services.AddControllers(opt =>
    opt.Filters.Add<ApiValidationFilterAttribute>()
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsapp", builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();
app.Services.Migrate();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseMiddleware<AuthMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
