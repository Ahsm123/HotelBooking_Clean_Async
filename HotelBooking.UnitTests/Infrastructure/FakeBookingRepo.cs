using HotelBooking.Core;

namespace HotelBooking.UnitTests.Infrastructure;

public class FakeBookingRepo : IRepository<Booking>
{
    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        List<Booking> bookings = new();
        return await Task.FromResult(bookings);
    }

    public Task<Booking> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Booking entity)
    {
        throw new NotImplementedException();
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