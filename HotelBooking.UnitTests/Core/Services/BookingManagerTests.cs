using HotelBooking.Core;
using HotelBooking.UnitTests.Infrastructure;
using Moq;

namespace HotelBooking.UnitTests.Core.Services;

public class BookingManagerTests
{
    //Moq
    private readonly Mock<IRepository<Booking>> _mockBookingRepo;
    private readonly Mock<IRepository<Room>> _mockRoomRepo;
    private readonly IBookingManager _bookingManager;
    
    //Fakes
    private readonly IRepository<Booking> _bookingRepo;
    private readonly IRepository<Room> _roomRepo;
    private readonly IBookingManager _bookingManagerFake;
    

    public BookingManagerTests()
    {
        //Moq
        _mockBookingRepo = new Mock<IRepository<Booking>>();
        _mockRoomRepo = new Mock<IRepository<Room>>();
        _bookingManager = new BookingManager(_mockBookingRepo.Object, _mockRoomRepo.Object);
        
        //Fake
        _roomRepo = new FakeRoomRepo();
        _bookingRepo = new FakeBookingRepo();
        _bookingManagerFake = new BookingManager(_bookingRepo, _roomRepo);
    }

    #region FindAvailableRoom
    
    [Theory]
    [InlineData(5, 10, 2)]
    [InlineData(15, 20, 1)]
    public async Task FindAvailableRoomAsync_ValidPeriod_ReturnsRoomId(
        int startDaysFromToday,
        int endDaysFromToday,
        int expectedRoomId
        )
    {
        //Arrange
        var today = DateTime.Today;
        var startDate = today.AddDays(startDaysFromToday);
        var endDate = today.AddDays(endDaysFromToday);

        //Act
        var result = await _bookingManagerFake.FindAvailableRoom(startDate, endDate);
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
        //Er det dårlig praksis at tilføje en booking i Arrange, istedet for at den er hardcoded i 
        //booking repo? Så er vi afhængig af den metode. Eller er det selve logikken i at have FindAvailableRoom public
        //når create booking underliggende er afhængig af FindAvailableRoom. (Encapsulation)
        
        //Arrange
        
        //Med Moq
         // _mockBookingRepo.Setup(x => x.GetAllAsync())
         //     .ReturnsAsync(new List<Booking> { new Booking
         //     {
         //         RoomId = 1, 
         //         StartDate =  DateTime.Today.AddDays(1), 
         //         EndDate = DateTime.Today.AddDays(5),
         //         IsActive = true
         //     } });
         //
         // _mockRoomRepo.Setup(x => x.GetAllAsync())
         //     .ReturnsAsync(new List<Room> { new Room { Id = 1 } });
         
         //Act
        // var result = await _bookingManager.FindAvailableRoom(DateTime.Today.AddDays(2), DateTime.Today.AddDays(6));
        
        var result = await _bookingManagerFake.FindAvailableRoom(DateTime.Today.AddDays(9), DateTime.Today.AddDays(16));
        
        Assert.Equal(-1, result);
    }
    
    #endregion
    
    #region CreateBooking
    [Fact]
    public async Task CreateBooking_BrokenLogic()
    {
        var bookingOne = new Booking
        {
            StartDate = DateTime.Now.AddDays(22),
            EndDate = DateTime.Now.AddDays(25),
            RoomId = 2,
            IsActive = true,
        };
        
        var result = await  _bookingManagerFake.CreateBooking(bookingOne);
        
        var availableRoomId = await _bookingManagerFake.FindAvailableRoom(bookingOne.StartDate, bookingOne.EndDate);
        
        Assert.Equal(2, availableRoomId);
        Assert.True(result);
    }
    
    
    
    #endregion



}

