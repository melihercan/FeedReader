using FeedReader.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace FeedReader.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
