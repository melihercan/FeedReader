using FeedReader.Application.TodoLists.Commands.CreateTodoList;
using FeedReader.Application.TodoLists.Commands.DeleteTodoList;
using FeedReader.Application.TodoLists.Commands.UpdateTodoList;
using FeedReader.Application.TodoLists.Queries.ExportTodos;
using FeedReader.Application.TodoLists.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FeedReader.WebUI.Controllers
{
    [Authorize]
    public class TodoListsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<TodosVm>> Get()
        {
            return await Mediator.Send(new GetTodosQuery());
        }

        [HttpGet("{id}")]
        public async Task<FileResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateTodoListCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, UpdateTodoListCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoListCommand { Id = id });

            return NoContent();
        }
    }
}
