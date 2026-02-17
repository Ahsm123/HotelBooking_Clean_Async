using HotelBooking.Core;
using HotelBooking.UnitTests.Infrastructure;
using Moq;

namespace HotelBooking.UnitTests.Core.Services;

public class BookingManagerTests
{
    private readonly Mock<IRepository<Booking>> _mockBookingRepo;
    private readonly Mock<IRepository<Room>> _mockRoomRepo;
    private readonly IBookingManager _bookingManager;

    public BookingManagerTests()
    {
        _mockBookingRepo = new Mock<IRepository<Booking>>();
        _mockRoomRepo = new Mock<IRepository<Room>>();
        _bookingManager = new BookingManager(_mockBookingRepo.Object, _mockRoomRepo.Object);
    }

    [Theory]
    [InlineData(1, 2, 1)]
    [InlineData(1, 1, 1)]
    public async Task FindAvailableRoomAsync_ValidPeriod_ReturnsRoomId(
        int startDaysFromToday,
        int endDaysFromToday,
        int expectedRoomId)
    {
        //Arrange
        var today = DateTime.Today;
        var startDate = today.AddDays(startDaysFromToday);
        var endDate = today.AddDays(endDaysFromToday);
        
        _mockBookingRepo.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Booking> {});
        _mockRoomRepo.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Room> { new Room { Id = 1 } });

        //Act
        var result = await _bookingManager.FindAvailableRoom(startDate, endDate);
        //Assert
        Assert.Equal(expectedRoomId, result);
    }

    [Theory]
    [InlineData(0, 1)] //startDate = idag
    [InlineData(-1, 1)] // startDate = igår
    [InlineData(2, 1)] // startDate > endDate
    public async Task FindAvailableRoomAsync_InvalidPeriod_ThrowsArgumentException(
        int startDaysFromToday,
        int endDaysFromToday)
    {
        //Arrange
        var today = DateTime.Today;
        var startDate = today.AddDays(startDaysFromToday);
        var endDate = today.AddDays(endDaysFromToday);
        
        //Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _bookingManager.FindAvailableRoom(startDate, endDate));
    }

    [Fact]
    public async Task FindAvailableRoomAsync_NoRoomsAvailable_ReturnsMinusOne()
    {
        //Arrange
        _mockBookingRepo.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Booking> { new Booking
            {
                RoomId = 1, 
                StartDate =  DateTime.Today.AddDays(1), 
                EndDate = DateTime.Today.AddDays(5),
                IsActive = true
            } });
        
        _mockRoomRepo.Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<Room> { new Room { Id = 1 } });
        
        var result = await _bookingManager.FindAvailableRoom(DateTime.Today.AddDays(2), DateTime.Today.AddDays(7));
        
        Assert.Equal(-1, result);
    }
}