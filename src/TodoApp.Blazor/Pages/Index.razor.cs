using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp.Blazor.Pages;

public partial class Index
{
    [Inject]//Blazor 'ın bir attribute 'u ,Dependency Injection ile almamızı sağlıyor .
    private ITodoAppService _todoAppService { get; set; }

    private List<TodoItemDto> TodoItems { get; set; } = new();
    private string NewTodoText { get; set; }

    //Initial olurken otomatik olarak çağrılan kod
    protected override async Task OnInitializedAsync()
    {
        TodoItems = await _todoAppService.GetListAsync();
    }
    private async Task Create()
    {
        try
        {
        var result= await _todoAppService.CreateAsync(NewTodoText);
        TodoItems.Add(result);
        NewTodoText = null;
        }
        catch (Exception ex)
        {
           await HandleErrorAsync(ex);
        }
    }
    private async Task Delete(TodoItemDto todoItem)
    {
        await _todoAppService.DeleteAsync(todoItem.Id);
        TodoItems.Remove(todoItem);

        await Notify.Info("Deleted todo item:" + todoItem.Text); //abp 'nın
    }
}
