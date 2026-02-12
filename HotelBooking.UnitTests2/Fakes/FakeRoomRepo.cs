using HotelBooking.Core;

namespace HotelBooking.UnitTests2.Fakes;

public class FakeRoomRepo : IRepository<Room>
{
    public Task<IEnumerable<Room>> GetAllAsync()
    {
        var rooms = new List<Room>()
        {
            new Room(Id = 1, Description = "Pres. Suit"),
            new Room(Id = 2, Decsriptoin = "Frederiks Suit")
        };

        return rooms;
    }

    public Task<Room> GetAsync(int id)
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