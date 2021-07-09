using NotasService.Models;

namespace NotasService.Services
{
    public interface INotasSearchByDateService
    {
        ServiceResponse Execute(Notas notas);
    }
}