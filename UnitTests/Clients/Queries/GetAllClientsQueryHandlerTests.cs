using Application.DTOs;
using Application.Features.Queries.GetAllClients;
using Application.Interfaces;
using Application.Specifications;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Newtonsoft.Json;
using System.Text;
using static Application.Features.Queries.GetAllClients.GetAllClientsQuery;


namespace UnitTests.Clients.Queries
{
    public class GetAllClientsQueryHandlerTests
    {
        private readonly Mock<IRepositoryAsync<Client>> _mockClientRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IDistributedCache> _mockDistributeCache;
        private readonly GetAllClientsQueryHandler _handler;
        public GetAllClientsQueryHandlerTests()
        {
            _mockClientRepository = new Mock<IRepositoryAsync<Client>>();
            _mockMapper = new Mock<IMapper>();
            _mockDistributeCache= new Mock<IDistributedCache>();

            _handler = new GetAllClientsQueryHandler(
                _mockClientRepository.Object,
                _mockMapper.Object,
                _mockDistributeCache.Object);
        }

        //For set Method TestName

        //1->what do we test :GetAllClientsQueryHandler
        //2->The scenario :WhenRedisClientIsNull_
        //3-> The Return :ShouldReturnClientListDB

        [Fact]
        public async void GetAllClientsQueryHandler_WhenRedisClientIsNull_ShouldReturnClientListDB()
        {
            //Arrange : Configure in parameters of our unit test

            GetAllClientsQuery command = new()
            { 
                FirstName="Test",
                LastName="Test",
                PageNumber=1,
                PageSize=5
            };

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

            _mockDistributeCache.Setup(mock => mock.GetAsync(It.IsAny<string>(),default))
                .Returns(Task.FromResult(new byte[] { }));

            _mockDistributeCache.Setup(mock => mock.SetAsync(
                It.IsAny<string>(),
                It.IsAny<byte[]>(),
                It.IsAny<DistributedCacheEntryOptions>(), default));

            _mockClientRepository.Setup(mock => mock.ListAsync(It.IsAny<PagedClientSpecification>(), default))
                .Returns(Task.FromResult(new List<Client> { client }));

            _mockMapper.Setup(mock => mock.Map<List<ClientDto>>(It.IsAny<List<Client>>()))
                .Returns(new List<ClientDto> { clientDto});

            // Act: Method execution to proof of our unit test

            var result = await _handler.Handle(command, default);

            // Assert : verify return data  of our executed method

            Assert.NotNull(result);
            result.Data.Count.Should().Be(1);
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(5);
            result.Errors.Should().BeNullOrEmpty();
            result.Message.Should().BeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
        }


        //For set Method TestName

        //1->what do we test :GetAllClientsQueryHandler
        //2->The scenario :WhenRedisClientIsNotNull_
        //3-> The Return :ShouldReturnClientListRedisCache


        [Fact]
        public async void GetAllClientsQueryHandler_WhenRedisClientIsNotNull_ShouldReturnClientListRedisCache()
        {
            //Arrange : Configure in parameters of our unit test

            GetAllClientsQuery command = new()
            {
                FirstName = "Test",
                LastName = "Test",
                PageNumber = 1,
                PageSize = 5
            };

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

            List<Client> clientsListTest = new List<Client> { client };

            byte[] byteArrayClient = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(clientsListTest));

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

            _mockDistributeCache.Setup(mock => mock.GetAsync(It.IsAny<string>(), default))
                .Returns(Task.FromResult(byteArrayClient));

            _mockMapper.Setup(mock => mock.Map<List<ClientDto>>(It.IsAny<List<Client>>()))
                .Returns(new List<ClientDto> { clientDto });

            // Act: Method execution to proof of our unit test

            var result = await _handler.Handle(command, default);

            // Assert : verify return data  of our executed method

            Assert.NotNull(result);
            result.Data.Count.Should().Be(1);
            result.Data.First().Id.Should().Be(1);
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(5);
            result.Errors.Should().BeNullOrEmpty();
            result.Message.Should().BeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
        }
    }
}
