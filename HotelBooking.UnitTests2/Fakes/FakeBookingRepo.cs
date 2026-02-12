using HotelBooking.Core;

namespace HotelBooking.UnitTests2.Fakes;

public class FakeBookingRepo : IRepository<Booking>
{
    public Task<IEnumerable<Booking>> GetAllAsync()
    {
        throw new NotImplementedException();
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