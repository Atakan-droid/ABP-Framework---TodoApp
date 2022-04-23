using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Localization;
using TodoApp.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace TodoApp
{//Crud kullanılabilir. CrudAppService
    [Authorize(TodoAppPermissions.ManageTodoItems)]
    public class TodoAppService : TodoAppAppService, ITodoAppService
    {
        private readonly IRepository<TodoItem, Guid> _repository;
        //basic localization in .net
        private readonly IStringLocalizer<TodoAppResource> localizer;

        public TodoAppService(IRepository<TodoItem,Guid> repository,IStringLocalizer<TodoAppResource> localizer)
        {
           _repository = repository;
            this.localizer = localizer;
        }
        [UnitOfWork(isTransactional:false)] //when its false rollback not works ex:when 3/5 failed transaction other 2 success are recorded
        public async Task<TodoItemDto> CreateAsync(string text)
        {
            if (await _repository.AnyAsync(i => i.Text == text))
            {
                 DuplicateError(text);
                //we can use both but one of them for static exception
                throw new UserFriendlyException(
                    localizer["SameItemError", text]);
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
            var todoItem = new TodoItem
            {  Text = text,
               TenantId=CurrentTenant.Id
            };
            await _repository.InsertAsync(todoItem);

            return ObjectMapper.Map<TodoItem, TodoItemDto>(todoItem);
        }
        public static void DuplicateError(string error)
        {
           throw new BusinessException("TodoApp:SameItemError").WithData("Text", error);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<TodoItemDto>> GetListAsync()
        {
            var items = await _repository.GetListAsync();
            /*return items
                .Select(item => new TodoItemDto 
                { Id = item.Id, Text = item.Text })
                .ToList();*/
            return ObjectMapper.Map<List<TodoItem>,List<TodoItemDto>>(items);
        }

        public async Task<TodoItemDto> UpdateAsync(string text)
        {
            throw new NotImplementedException();
        }
    }
}
