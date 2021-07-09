using NotasService.Models;

namespace NotasService.Services
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Login(User user);

        string MessageGet();
    }
}