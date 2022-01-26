using ContactsAPI.Services;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using ContactsAPI.Data;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSingleton<ContactsAPI.Services.IUserService, UserService>();



builder.Services.AddDbContext<ContactsAPI.Data.ContactsdbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsDB"));
    });


var app = builder.Build();

app.MapGet("/", () => "Hello World!")
    .ExcludeFromDescription();

app.MapGet("/users", ([FromServices] ContactsdbContext db) =>
{
    return db.Users.ToList();

});

app.MapPost("/login",
    (UserLogin user, [FromServices] ContactsdbContext db) => Login(user, db))
    .Accepts<UserLogin>("application/json")
    .Produces<string>();

app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();



IResult Login(UserLogin user, ContactsdbContext db)
{
    if (!string.IsNullOrEmpty(user.Username) &&
        !string.IsNullOrEmpty(user.Password))
    {
        var loggedInUser = db.Users.FirstOrDefault(o => o.Username.Equals
             (user.Username) && o.Password.Equals
             (user.Password));


        //var loggedInUserOld = service.Get(user);

        if (loggedInUser is null) return Results.NotFound("User not found.");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
            new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
            new Claim(ClaimTypes.GivenName, loggedInUser.GivenName),
            new Claim(ClaimTypes.Surname, loggedInUser.Surname),
            new Claim(ClaimTypes.Role, loggedInUser.Role)
        };

        var token = new JwtSecurityToken
        (
           issuer: builder.Configuration["Jwt:Issuer"],
           audience: builder.Configuration["Jwt:Audience"],
           claims: claims,
           expires: DateTime.UtcNow.AddDays(60),
           notBefore: DateTime.UtcNow,
           signingCredentials: new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
               SecurityAlgorithms.HmacSha256)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(tokenString);
    }
    return Results.BadRequest("Invalid user credentials");
}



//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwaggerUI();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();





app.Run();
