using System.Collections.Generic;
using AutoMapper;
using TodoList.Api.Models.Responses;
using TodoList.Data.Models;
using X.PagedList;

namespace TodoList.Api.Mappings
{
    public class TodoItemsMappings : Profile
    {
        public TodoItemsMappings()
        {
            CreateMap<TodoItem, TodoItemResponse>();
        }
    }
}


