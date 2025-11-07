using Common.API.Helpers.JwtHelpers;
using Common.API.Helpers.Utils;
using Common.Helpers.Class;
using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Suppliers.BL.Interfaces;

namespace Suppliers.BL.Bl;

public class AuthBl : IAuthBl
{
    private readonly DescargaCfdiGfpContext _context;
    private readonly Jwt _settings;
    private readonly HttpContext _httpContext;

    public AuthBl(IOptions<Jwt> jwt, DescargaCfdiGfpContext context, IHttpContextAccessor httpContext)
    {
        _settings = jwt.Value;
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _httpContext = httpContext.HttpContext;
    }

    public Task<ApiResponse<UserResponseRT<User>>> Auth(UserLogin userLogin)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<string>> GetToken(uint usrId)
    {
        throw new NotImplementedException();
    }
}
