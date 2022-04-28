using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TodoApiPractise.DTOs;
using TodoApiPractise.Services;
using Microsoft.AspNetCore.Http;


namespace TodoApiPractise.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoListRepository _toDoListRepository;

        //If it's private and readonly, the benefit is that you can't inadvertently change it from another part of that class after it is initialized. The readonly modifier ensures the field can only be given a value during its initialization or in its class constructor.
        private readonly IMapper _mapper;
        public ToDoController(IToDoListRepository toDoListRepository, IMapper mapper)
        {
            _toDoListRepository = toDoListRepository ??
                throw new ArgumentNullException(nameof(toDoListRepository));
            _mapper = mapper ??
                 throw new ArgumentNullException(nameof(mapper));


        }

        //Controller for getting all the todolists
        [HttpGet("/GetAlltodo")]
        
        public async Task<ActionResult<IEnumerable<ToDoListDto>>> GetTodLists()
        {


            var toDoEntities = await _toDoListRepository.GetToDoListAsync();

            return Ok(_mapper.Map<IEnumerable<ToDoListDto>>(toDoEntities));
        }

        //Controller for getting a single todolist by id 
        [HttpGet("/GetSingleTodo/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ToDoListDto>> GetTodList(int id)           
        {
            var todo = await _toDoListRepository.GetSpecificTodoAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ToDoListDto>(todo));
        }


        //Controller for getting  todos by the start dateandtime 

        [HttpGet("/GetIncomingtodo/{date}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ToDoListDto>> GetTodListOfDates(DateTime date)
          
        {
            var todo = await _toDoListRepository.GetIncomingToDoAsync(date);
            if (todo == null)
            {
                return NotFound();
            }            
            return Ok(todo.Select(item => _mapper.Map<ToDoListDto>(item)));
        }

        //Controller for creating todos
        [HttpPost("/CreateTodo/")]
        public async Task<ActionResult<ToDoListDto>> CreateToDoListDto(ToListForCreationDto toListForCreationDto)
        {
            
            var finalToList = _mapper.Map<Entities.ToDoList>(toListForCreationDto);
           
            if (DateTime.Compare(finalToList.StartDate, DateTime.Now) < 0 )
            {
                return BadRequest(" Choose differnt date as It can not earlier than the current time  ");
            }
            if (DateTime.Compare(finalToList.EndDate,finalToList.StartDate) <=  0 )
            {
                return BadRequest(" Choose differnt date as End date can not be earlier  than Start Date  ");
            }
            if (finalToList.CompletedPercentage < 100)
            {
                finalToList.Done= false;
            }
            await _toDoListRepository.AddToDoListAsync(finalToList); 
            await _toDoListRepository.SaveChangesAsync();
            var createdToDoListToReturn =
                _mapper.Map<DTOs.ToDoListDto>(finalToList);

            return CreatedAtAction(nameof(GetTodList), new { id = createdToDoListToReturn.Id}, createdToDoListToReturn);
        }

        //Controller for updating todos
        [HttpPut("/Update/{toDoListId}")]
        public async Task<ActionResult> UpdateTodList( int toDoListId , ToDoListForUpdateDto toDoListForUpdateDto)
        {
            var toDoListEntity = await _toDoListRepository.GetSpecificTodoAsync(toDoListId);
            if (toDoListEntity == null)
            {
                return NotFound();
            }
            _mapper.Map(toDoListForUpdateDto, toDoListEntity);
            await _toDoListRepository.SaveChangesAsync();
            return NoContent();

        }
        //Controller for partially updating the  todos
        [HttpPatch("/PartialUpdate/{toDoListId}")]
        public async Task<ActionResult> PartiallyUpdateTodList(int toDoListId, JsonPatchDocument<ToDoListForUpdateDto> patchDocument)
        {
            var toDoListEntity = await _toDoListRepository.GetSpecificTodoAsync(toDoListId);

            if (toDoListEntity == null)
            {
                return NotFound();
            }
            var toDoListToPatch = _mapper.Map<ToDoListForUpdateDto>(toDoListEntity);
            patchDocument.ApplyTo(toDoListToPatch,ModelState);
                
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(toDoListToPatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(toDoListToPatch, toDoListEntity);
            await _toDoListRepository.SaveChangesAsync();
            return NoContent();
        }

        //Controller for deleting todolist by Id

        [HttpDelete("/Delete/{toDoListId}")]
        public async Task<ActionResult> DeleteToDoList(int toDoListId)
        {

            var toDoListEntity = await _toDoListRepository.GetSpecificTodoAsync(toDoListId);
                
            if (toDoListEntity == null)
            {
                return NotFound();
            }
            _toDoListRepository.DeleteToDoList(toDoListEntity); 
            
            await _toDoListRepository.SaveChangesAsync();
            return NoContent();

        }

    }
}