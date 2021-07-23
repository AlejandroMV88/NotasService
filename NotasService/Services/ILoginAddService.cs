using NotasService.Models;

namespace NotasService.Services
{
    public interface ILoginAddService
    {
        ServiceResponse Execute(User user);
    }
}