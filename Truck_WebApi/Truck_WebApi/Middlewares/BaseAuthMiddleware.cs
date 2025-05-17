using System.Security.Claims;
using System.Text;
using Truck_DataAccess.Repositories;
using Truck_Shared.Entities;
using Truck_Shared.Helpers;

public class BaseAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserRepository _repository;

    public BaseAuthMiddleware(RequestDelegate next, IUserRepository repository)
    {
        _next = next;
        _repository = repository;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var authHeader = context.Request.Headers.Authorization;
            if (string.IsNullOrWhiteSpace(authHeader))
            {
                await Respond(context, "Authorization header is empty", 400);
                return;
            }

            var base64 = authHeader.ToString().Replace("Basic ", "");
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            var parts = decoded.Split(':');
            if (parts.Length != 2)
            {
                await Respond(context, "Invalid auth format", 400);
                return;
            }

            var username = parts[0];
            var password = parts[1];

            var user = await _repository.GetByUserNameAsync(username);
            if (user == null)
            {
                await Respond(context, "Wrong credentials", 401);
                return;
            }

            var hash = PasswordHasher.GenerateHash(password, user.PasswordSalt);
            if (!hash.SequenceEqual(user.PasswordHash))
            {
                await Respond(context, "Wrong credentials", 401);
                return;
            }

            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var identity = new ClaimsIdentity(claims, "BaseAuth");
            context.User = new ClaimsPrincipal(identity);

            await _next(context);
        }
        catch
        {
            await Respond(context, "Technical error", 400);
        }
    }

    private async Task Respond(HttpContext context, string msg, int code)
    {
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(new ServerResult
        {
            Success = false,
            Message = msg,
            ResponseCode = code
        });
    }
}
