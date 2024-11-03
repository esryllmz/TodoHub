using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoHub.Core.Exceptions;
using TodoHub.Models.Dtos.Todo.Responses;

namespace TodoHub.Services.CacheService
{
    public sealed class TodoCacheService(IDistributedCache cache)
    {

        public async Task<TodoResponseDto> GetTodoByIdAsync(Guid id)
        {
            string cacheKey = $"Todo({id})";
            string cachedTodo = await cache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cachedTodo))
            {
                throw new BusinessException("İlgili todo cache'de yok.");
            }

            TodoResponseDto todo = JsonSerializer.Deserialize<TodoResponseDto>(cachedTodo);
            return todo;
        }

        public async Task<TodoResponseDto> CreateTodoAsync(TodoResponseDto todo)
        {
            string cacheKey = $"Todo({todo.Id})";
            string serializedTodo = JsonSerializer.Serialize(todo);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            };

            await cache.SetStringAsync(cacheKey, serializedTodo, options);

            return todo;
        }

        public async Task DeleteAsync(Guid id)
        {
            string cacheKey = $"Todo({id})";
            await cache.RemoveAsync(cacheKey);
        }
    }
}

