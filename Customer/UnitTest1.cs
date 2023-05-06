//using AutoMapper;
//using DiscountSystem.Data.Context;
//using DiscountSystem.Data.Dto;
//using DiscountSystem.Data.IRepository;
//using DiscountSystem.Data.Model;
//using DiscountSystem.Service.IServices;
//using DiscountSystem.Service.Services;
//using Moq;

//namespace DiscountSystem.Test.Services
//{
//  public class CustomerServiceTest
//  {
//    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
//    private readonly Mock<IMapper> _mapperMock;
//    private readonly DataContext _context;
//    private readonly ICustomerService _customerService;

//    public CustomerServiceTest()
//    {
//      _unitOfWorkMock = new Mock<IUnitOfWork>();
//      _mapperMock = new Mock<IMapper>();
//      _context = new DataContext(); // Replace with a mock if necessary
//      _customerService = new CustomerService(_context, _unitOfWorkMock.Object, _mapperMock.Object);
//    }

//    [Fact]
//    public async Task AddCustomerAsync_Should_Return_Successful_Result_When_Adding_New_Customer()
//    {
//      // Arrange
//      var customerDto = new CustomerDto
//      {
//        Address = "6, Ayayi street",
//        DiscountId = 1,
//        PhoneNumber = "555-1234"
        
//      };

//      var customer = new Customer
//      {
//        Address = customerDto.Address,
//        DiscountId = customerDto.DiscountId,
//        PhoneNumber = customerDto.PhoneNumber,
//        Id = 1,
//        CreatedDate = DateTime.UtcNow
//      };

//      await _unitOfWorkMock.Setup(u => u.CustomerRepository.Add(It.IsAny<Customer>())).Returns(Task.CompletedTask);

//      // Act
//      var result = await _customerService.AddCustomerAsync(customerDto);

//      // Assert
//      Assert.True(result.IsSuccess);
//      _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
//    }

//    [Fact]
//    public async Task AddCustomerAsync_Should_Return_Failure_Result_When_Customer_Already_Exists()
//    {
//      // Arrange
//      var customerDto = new CustomerDto
//      {
//        Address = "6, Ayayi street",
//        DiscountId = 1,
//        PhoneNumber = "555-1234"

//      };

//      _context.Customers.Add(new Customer
//      {
//        Address = customerDto.Address,
//        DiscountId = customerDto.DiscountId,
//        PhoneNumber = customerDto.PhoneNumber,
//        Id = 1,
//        CreatedDate = DateTime.UtcNow
//      });

//      // Act
//      var result = await _customerService.AddCustomerAsync(customerDto);

//      // Assert
//      Assert.True(result.IsFailure);
//    }

//    [Fact]
//    public async Task GetAllCustomerAsync_Should_Return_All_Customers()
//    {
//      // Arrange
//      var customers = new List<Customer>
//            {
//                new Customer { Id = 1, Address = "John", DiscountId = 1, PhoneNumber = "555-1234", CreatedDate = DateTime.UtcNow },
//                new Customer { Id = 2, Address = "Jane", DiscountId = 1, PhoneNumber = "555-1234", CreatedDate = DateTime.UtcNow },
                
//            };
//      _unitOfWorkMock.Setup(u => u.CustomerRepository.GetAll()).ReturnsAsync(customers);
//      _mapperMock.Setup(m => m.Map<IEnumerable<CustomerDto>>(It.IsAny<IEnumerable<Customer>>())).Returns((IEnumerable<Customer> source) => source.Select(c => new CustomerDto
//      {
//        Address = c.Address,
//        PhoneNumber = c.PhoneNumber,
//        DiscountId = c.DiscountId ?? 0
//      }));
//    }
//  }
//}