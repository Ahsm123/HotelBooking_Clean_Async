using HotelBooking.Core;
using HotelBooking.UnitTests2.Fakes;

namespace HotelBooking.UnitTests2;

public class BookingManagerTests
{
    private IBookingManager _bookingManager;
    IRepository<Booking> _fakeBookingRepo;
    IRepository<Room> _fakeRoomRepo;
    

    public BookingManagerTests()
    {
        _fakeBookingRepo = new FakeBookingRepo();
        _fakeRoomRepo = new FakeRoomRepo();
        _bookingManager = new BookingManager(_fakeBookingRepo, _fakeRoomRepo);
    }

    [Fact]
    public async Task FindAvailableRoom_IfStartDateIsBeforeToday_ReturnArgumentException()
    {
        //Arrange
        DateTime startDate = DateTime.Today.AddDays(-1);
        DateTime endDate = DateTime.Today;

        //Act
        Task result() => _bookingManager.FindAvailableRoom(startDate, endDate);

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(result);

    }
    
    [Fact]
    public async Task FindAvailableRoom_IfStartDateIsAfterEndDate_ReturnArgumentException()
    {
        //Arrange
        DateTime startDate = DateTime.Today.AddDays(1);
        DateTime endDate = DateTime.Today;

        //Act
        Task result() => _bookingManager.FindAvailableRoom(startDate, endDate);

        //Assert
        await Assert.ThrowsAsync<ArgumentException>(result);

    }
    
    
}
