using Application.DTOs;
using Application.Features.Queries.GetClientById;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Moq;
using static Application.Features.Queries.GetClientById.GetClientByIdQuery;

namespace UnitTests.Clients.Queries
{
    public class GetClientByIdQueryHandlerTests
    {
        private readonly Mock<IRepositoryAsync<Client>> _mockClientRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetClientByIdQueryHandler _handler;
        public GetClientByIdQueryHandlerTests()
        {
            _mockClientRepository = new Mock<IRepositoryAsync<Client>>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetClientByIdQueryHandler(_mockClientRepository.Object, _mockMapper.Object);
        }

        //For set Method TestName

        //1->what do we test :GetClientByIdQueryHandler_
        //2->The scenario :WhenFoundId_
        //3-> The Return :ShouldReturnClient

        [Fact]
        public async void GetClientByIdQueryHandler_WhenFoundId_ShouldReturnClient()
        {
            //Arrange : Configure in parameters of our unit test

            GetClientByIdQuery command = new(){ Id=1};

            Client client = new()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Telephone = "Test",
                Address = "Test",
                Email = "Test",
                Birthday = DateTime.Now
            };

            ClientDto clientDto = new()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Telephone = "Test",
                Address = "Test",
                Email = "Test",
                Birthday = DateTime.Now
            };


            _mockClientRepository.Setup(mock => mock.GetByIdAsync(It.IsAny<int>(), default))
                .Returns(Task.FromResult(client));

            _mockMapper.Setup(mock => mock.Map<ClientDto>(It.IsAny<Client>()))
                .Returns(clientDto);

            // Act: Method execution to proof of our unit test

            var result = await _handler.Handle(command, default);

            // Assert : verify return data  of our executed method

            Assert.NotNull(result);
            result.Errors.Should().BeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Message.Should().BeNullOrEmpty();
            result.Data.Should().BeOfType(typeof(ClientDto));
            result.Data.Id.Should().Be(1);
        }
    }
}
