using Common.API.Helpers.Utils;

namespace Suppliers.BL.Interfaces;

public interface IAuthBl
{
    //public Task<ApiResponse<UserResponseRT<User>>> Auth(UserLogin userLogin);
    public Task<ApiResponse<string>> GetToken(uint usrId);
}