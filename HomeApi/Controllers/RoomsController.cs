using System.Threading.Tasks;
using AutoMapper;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using HomeApi.Data.Repos;
using Microsoft.AspNetCore.Mvc;

namespace HomeApi.Controllers
{
    /// <summary>
    /// Контроллер комнат
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomRepository _repository;
        private IMapper _mapper;
        
        public RoomsController(IRoomRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //TODO: Задание - добавить метод на получение всех существующих комнат
        
        /// <summary>
        /// Добавление комнаты
        /// </summary>
        [HttpPost] 
        [Route("")] 
        public async Task<IActionResult> Add([FromBody] AddRoomRequest request)
        {
            var existingRoom = await _repository.GetRoomByName(request.Name);
            if (existingRoom == null)
            {
                var newRoom = _mapper.Map<AddRoomRequest, Room>(request);
                await _repository.AddRoom(newRoom);
                return StatusCode(201, $"Комната {request.Name} добавлена!");
            }
            
            return StatusCode(409, $"Ошибка: Комната {request.Name} уже существует.");
        }
        [HttpPut]
        [Route("{roomName}")]
        public async Task<IActionResult> Edit([FromRoute] string roomName, [FromBody] EditRoomRequeest request)
        {
            Room room = await _repository.GetRoomByName(roomName);
            if(room == null)
            {
                return StatusCode(400, $"Ошибка: Комната с именем {roomName} не найдена. Введите корректное имя!");
            }
            var roomNames = await _repository.GetAllRoomNames();
            if (roomNames.Contains(request.Name))
            {
                return StatusCode(400, $"Ошибка: Комната с именем {request.Name} уже существует. Введите другое имя!");
            }
           await _repository.UpdateRoom(room, new UpdateRoomQuery(request.Name, request.Area, request.GasConnected, request.Voltage ));
            return StatusCode(200, $"Комната с имененм {room.Name} успешно обновлена!");

        }


    }
}