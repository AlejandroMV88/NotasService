using NotasService.Models;

namespace NotasService.Services
{
    public interface INotasDeleteService
    {
        ServiceResponse Execute(Notas notas);
    }
}