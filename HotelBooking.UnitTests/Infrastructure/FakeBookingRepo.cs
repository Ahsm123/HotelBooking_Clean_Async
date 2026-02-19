using HotelBooking.Core;

namespace HotelBooking.UnitTests.Infrastructure;

public class FakeBookingRepo : IRepository<Booking>
{
    private readonly List<Booking> _bookings = new();

    public FakeBookingRepo()
    {
        _bookings.Add(new Booking
        {
            Id = 1, 
            StartDate = DateTime.Now.AddDays(5), 
            EndDate = DateTime.Now.AddDays(10), 
            IsActive =  true,
            RoomId = 1,
        });
        _bookings.Add(new Booking
        {
            Id = 2, 
            StartDate = DateTime.Now.AddDays(15), 
            EndDate = DateTime.Now.AddDays(20), 
            IsActive =  true,
            RoomId = 2,
        });
    }
    
    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return  _bookings;
    }

    public Task<Booking> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Booking entity)
    {
        _bookings.Add(entity);
        return Task.CompletedTask;
    }

    public Task EditAsync(Booking entity)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }
}