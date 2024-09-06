using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeApi.Data.Models;
using HomeApi.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace HomeApi.Data.Repos
{
    /// <summary>
    /// Репозиторий для операций с объектами типа "Room" в базе
    /// </summary>
    public class RoomRepository : IRoomRepository
    {
        private readonly HomeApiContext _context;
        
        public RoomRepository (HomeApiContext context)
        {
            _context = context;
        }
        
        /// <summary>
        ///  Найти комнату по имени
        /// </summary>
        public async Task<Room> GetRoomByName(string name)
        {
            return await _context.Rooms.Where(r => r.Name == name).FirstOrDefaultAsync();
        }
        
        /// <summary>
        ///  Добавить новую комнату
        /// </summary>
        public async Task AddRoom(Room room)
        {
            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
                await _context.Rooms.AddAsync(room);
            
            await _context.SaveChangesAsync();
        }

        public async Task<List<string>> GetAllRoomNames()
        {
            return await _context.Rooms.Select(r => r.Name).ToListAsync();
        }

        public async Task UpdateRoom(Room room, UpdateRoomQuery query)
        {
            if(query.Name != null)
            {
                room.Name = query.Name;
            }
            if (query.Area != null)
            {
                room.Area = query.Area.Value;
            }
            if (query.GasConnected != null)
            {
                room.GasConnected = query.GasConnected.Value;
            }
            if (query.Voltage!= null)
            {
                room.Voltage = query.Voltage.Value;
            }
            var entry = _context.Entry(room);
            if (entry.State == EntityState.Detached)
            {
                _context.Rooms.Update(room);
            }
            await _context.SaveChangesAsync();

        }
       
    }
}