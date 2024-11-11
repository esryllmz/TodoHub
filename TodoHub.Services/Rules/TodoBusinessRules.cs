using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoHub.Core.Exceptions;
using TodoHub.DataAccess.Abstracts;
using TodoHub.DataAccess.Concretes;
using TodoHub.Services.Constants;

namespace TodoHub.Services.Rules
{
    public class TodoBusinessRules(ITodoRepository _todoRepository)
    {
        public async Task IsTodoExistAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);

            if (todo == null)
            {
                throw new NotFoundException($"{id} numaralı yapılacak iş bulunamadı.");
            }
        }

        public void ValidateDates(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                throw new BusinessException("Yapılacak işin başlangıç tarihi bitiş tarihinden geç olamaz.");
            }
        }

        private const int maxTodos = 5;

        public async Task CheckMaxTodosPerUserAsync(string userId)
        {
            var userToDosCount = await _todoRepository.GetToDosCountByUserAsync(userId);

            if (userToDosCount > maxTodos)
            {
                throw new BusinessException("Bir kullanıcı maksimum 5 işe sahip olabilir.");
            }
        }
    }
}
