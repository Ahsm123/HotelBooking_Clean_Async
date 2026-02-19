
using HotelBooking.Core;

namespace HotelBooking.UnitTests.Infrastructure;

public class FakeRoomRepo : IRepository<Room>
{
    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        var rooms =  new List<Room>();
        rooms.Add(new Room { Id = 1, Description = "Room 1" });
        rooms.Add(new Room { Id = 2, Description = "Room 2" });
        return await Task.FromResult(rooms.AsEnumerable());
    }

    public async Task<Room> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Room entity)
    {
        throw new NotImplementedException();
    }

    public Task EditAsync(Room entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }
}