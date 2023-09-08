using Application.Features.Commands.CreateClientCommand;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Moq;
using FluentAssertions;

namespace UnitTests.Clients.Commands
{
    public class CreateClientCommandHandlerTests
    {
        private readonly Mock<IRepositoryAsync<Client>> _mockClientRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateClientCommandHandler _handler;
        public CreateClientCommandHandlerTests()
        {
            _mockClientRepository = new Mock<IRepositoryAsync<Client>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new CreateClientCommandHandler(_mockClientRepository.Object, _mockMapper.Object);
        }

        //For set Method TestName

        //1->what do we test :HandlerCreateClient_
        //2->The scenario :WhenCreateClientIsOk_
        //3-> The Return :ShouldReturnClientId

        [Fact]
        public async void HandlerCreateClient_WhenCreateClientIsOk_ShouldReturnClientId()
        {
            //Arrange : Configure in parameters of our unit test

            CreateClientCommand commandSource = new()
            {
                FirstName = "Test",
                LastName = "Test",
                Telephone = "Test",
                Address = "Test",
                Email = "Test",
                Birthday = DateTime.Now
            };

            Client DestinationClient = new()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Telephone = "Test",
                Address = "Test",
                Email = "Test",
                Birthday = DateTime.Now
            };

            _mockMapper.Setup(mock => mock.Map<Client>(It.IsAny<CreateClientCommand>()))
                .Returns(DestinationClient);

            _mockClientRepository.Setup(mock => mock.AddAsync(It.IsAny<Client>(), default))
                .Returns(Task.FromResult(DestinationClient));

            // Act: Method execution to proof of our unit test

            var result = await _handler.Handle(commandSource, default);

            // Assert : verify return data  of our executed method

            Assert.NotNull(result);
            result.Errors.Should().BeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Message.Should().BeNullOrEmpty();
            result.Data.Should().BeGreaterThan(0);
        }
    }
}