using NotasService.Models;

namespace NotasService.Services
{
    public interface INotasUpdateService
    {
        ServiceResponse Execute(Notas notas);
    }
}