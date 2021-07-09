using NotasService.Models;

namespace NotasService.Services
{
    public interface INotasAddService
    {
        ServiceResponse Execute(Notas notas);
    }
}