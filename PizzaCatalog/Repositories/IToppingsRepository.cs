using PizzaCatalog.WebApi.Model.DTOs;

namespace PizzaCatalog.WebApi.Repositories
{
    public interface IToppingsRepository
    {
        Task<List<ToppingsDTO>> GetAllToppingsAsync();

        Task<ToppingsDTO> GetToppingsByIdAsync(int id);

        Task<ToppingsDTO> InsertToppings(ToppingsDTO toppingsDTO);
    }
}
