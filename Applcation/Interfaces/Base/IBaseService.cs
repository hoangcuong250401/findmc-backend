using Application.Dtos.User;
using Domain.Entities;
using Domain.Entities.Paging;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IBaseService<T, TDto> where T : BaseEntity where TDto : BaseEntity
    {
        Task<TDto> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(TDto entity);
        Task<int> UpdateAsync(TDto entity);
        Task<int> DeleteAsync(int id);
        Task<PagedResponse<TDto>> GetPagedAsync(PagedRequest pagingRequest);
        //Task<bool> AddMultipleAsync(IEnumerable<T> entities);
        //Task<bool> UpdateMultipleAsync(IEnumerable<T> entities);
        //Task<bool> DeleteMultipleAsync(IEnumerable<int> ids);
        //Task<(IEnumerable<T> Items, int TotalCount)> GetPagedDataAsync(
        //    string filter,
        //    int pageIndex = 0,
        //    int pageSize = 10);
        Task<TDto> FindByFieldAsync(string fieldName, object value);
    }
}
