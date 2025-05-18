using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Dtos.User;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Paging;

namespace Application.Services
{
    public class BaseService<T, TDto> : IBaseService<T, TDto> where T : BaseEntity where TDto : BaseEntity
    {
        private readonly IBaseRepository<T, TDto> _repository;

        public BaseService(IBaseRepository<T, TDto> repository)
        {
            _repository = repository;
        }

        public virtual async Task<TDto> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<int> AddAsync(TDto entity)
        {
            return await _repository.AddAsync(entity);
        }

        public virtual async Task<int> UpdateAsync(TDto entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public virtual async Task<PagedResponse<TDto>> GetPagedAsync(PagedRequest pagingRequest)
        {
            return await _repository.GetPagedAsync(pagingRequest);
        }

        public virtual async Task<TDto> FindByFieldAsync(string fieldName, object value)
        {
            return await _repository.FindByFieldAsync(fieldName, value);
        }
    }
}
