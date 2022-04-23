using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace TodoApp
{
    public class TodoItemDto:EntityDto<Guid>
    {
        [StringLength(TodoItemConsts.MaxTextLength)]
        public string Text { get; set; }
    }
}