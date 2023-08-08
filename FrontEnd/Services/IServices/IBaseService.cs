using FrontEnd.Models;

namespace FrontEnd.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDTO>? SendAsync(RequestDTO requestDTO);
    }
}
