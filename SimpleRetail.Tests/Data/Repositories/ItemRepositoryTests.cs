using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common;
using SimpleRetail.Common.Errors;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.EF.Model;
using SimpleRetail.Data.Repositories;
using SimpleRetail.Tests.Common.Requests;
using SimpleRetail.Tests.Data.Dtos;
using SimpleRetail.Tests.Data.Stub;

namespace SimpleRetail.Tests.Data.Repositories;

public class ItemRepositoryTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapperMock;

    private readonly ChangeItemRequest _request;

    public ItemRepositoryTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _mapperMock = new Mock<IMapper>();

        _request = ChangeItemRequestMock.Get();
    }


    [Fact]
    public async Task GetAll_ShouldReturnResults()
    {
        // Arrange
        IDbContextFactory<DataContext> contextFactoryMock = new DataDbContextFactoryMock("GetAll_ShouldReturnResults");
        TestItemRepository sut = new TestItemRepository(_mapperMock.Object, contextFactoryMock);

        var itemDtos = _fixture.CreateMany<ItemDto>().ToList();

        _mapperMock.Setup(x => x.Map<List<ItemDto>>(It.IsAny<List<Item>>())).Returns(itemDtos);

        // Act
        var result = await sut.GetAll(It.IsAny<int>(), It.IsAny<int>());

        // Assert
        result.Should().NotBeNull();
        result?.Count().Should().Be(itemDtos.Count);

        _mapperMock.Verify(x => x.Map<List<ItemDto>>(It.IsAny<List<Item>>()), Times.Once());
    }

    [Fact]
    public async Task GetById_ShouldReturnItem()
    {
        // Arrange
        IDbContextFactory<DataContext> contextFactoryMock = new DataDbContextFactoryMock("GetById_ShouldReturnItem");
        TestItemRepository sut = new TestItemRepository(_mapperMock.Object, contextFactoryMock );

        var dto = ItemDtoMock.Get();

        //we dont need mapper mock because we go to override method
        //_mapperMock.Setup(x => x.Map<ClientDto>(dbObject)).Returns(dto);

        // Act
        var result = await sut.GetById(It.IsAny<Guid>());

        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<ItemDto>();
        result.Should().BeEquivalentTo(dto);
    }

    [Fact]
    public async Task GetByIdInternal_ShouldReturnItem()
    {
        // Arrange
        IDbContextFactory<DataContext> contextFactoryMock = new DataDbContextFactoryMock("GetByIdInternal_ShouldReturnItem");
        ItemRepository sut = new ItemRepository(_mapperMock.Object, contextFactoryMock);

        var dto = ItemDtoMock.Get();

        _mapperMock.Setup(x => x.Map<ItemDto>(It.IsAny<Item>())).Returns(dto);

        // Act
        var result = await sut.GetById(dto.Id);

        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<ItemDto>();
        result.Should().BeEquivalentTo(dto);

        _mapperMock.Verify(x => x.Map<ItemDto>(It.IsAny<Item>()), Times.Once());
    }

    [Fact]
    public async Task CreateItem_ShouldReturnObject_OnSuccess()
    {
        // Arrange
        IDbContextFactory<DataContext> contextFactoryMock = new DataDbContextFactoryMock("CreateItem_ShouldReturnObject_OnSuccess");
        TestItemRepository sut = new TestItemRepository(_mapperMock.Object, contextFactoryMock);

        var responseDto = ItemDtoMock.Get();
        var dbObject = _fixture.Create<Item>();

        _mapperMock.Setup(x => x.Map<Item>(_request)).Returns(dbObject);


        // Act
        var result = await sut.CreateAsync(_request);


        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<ItemDto>();
        result.Should().BeEquivalentTo(responseDto);

        _mapperMock.Verify(x => x.Map<Item>(It.Is<ChangeItemRequest>(arg => arg == _request)), Times.Once());
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnObject_OnSuccess()
    {
        // Arrange
        IDbContextFactory<DataContext> contextFactoryMock = new DataDbContextFactoryMock("UpdateItem_ShouldReturnObject_OnSuccess");
        TestItemRepository sut = new TestItemRepository(_mapperMock.Object, contextFactoryMock);

        var responseDto = ItemDtoMock.Get();
        var dbObject = ItemMock.Get();
        _request.Id = responseDto.Id;
        dbObject.Id = responseDto.Id;

        _mapperMock
            .Setup(x => x.Map<ChangeItemRequest, Item>(It.IsAny<ChangeItemRequest>(), It.IsAny<Item>()))
            .Returns((ChangeItemRequest request, Item itemResponse) =>
            {
                itemResponse = ItemMock.Get();
                return itemResponse;
            });

        // Act
        var result = await sut.UpdateAsync(_request);


        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<ItemDto>();
        result.Should().BeEquivalentTo(responseDto);

        _mapperMock.Verify(x => x.Map<ChangeItemRequest, Item>(It.IsAny<ChangeItemRequest>(), It.IsAny<Item>()), Times.Once());
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnError_IfNotFound()
    {
        // Arrange
        IDbContextFactory<DataContext> contextFactoryMock = new DataDbContextFactoryMock("UpdateItem_ShouldReturnError_IfNotFound");
        TestItemRepository sut = new TestItemRepository(_mapperMock.Object, contextFactoryMock);

        var nonExistingClientId = Guid.NewGuid();
        var dbObject = ItemMock.Get();
        _request.Id = nonExistingClientId;

        _mapperMock
            .Setup(x => x.Map<ChangeItemRequest, Item>(It.IsAny<ChangeItemRequest>(), It.IsAny<Item>()))
            .Returns((ChangeItemRequest request, Item itemResponse) =>
            {
                itemResponse = ItemMock.Get();
                return itemResponse;
            });

        // Act
        var result = await Record.ExceptionAsync(() => sut.UpdateAsync(_request));

        // Assert
        result.Should()
                .NotBeNull().And
                .BeAssignableTo<SimpleRetailException>().And
                .Match<SimpleRetailException>(ex => ex.Code.Contains(nameof(Configuration.Messages.EntityUpdateFailedNotExists)));
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnObject_OnSuccess()
    {
        // Arrange
        IDbContextFactory<DataContext> contextFactoryMock = new DataDbContextFactoryMock("DeleteItem_ShouldReturnObject_OnSuccess");
        TestItemRepository sut = new TestItemRepository(_mapperMock.Object, contextFactoryMock);

        var dto = ItemDtoMock.Get();

        // Act
        var result = await Record.ExceptionAsync(() => sut.DeleteAsync(dto.Id, It.IsAny<Guid>()));

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnError_OnFailure()
    {
        // Arrange
        IDbContextFactory<DataContext> contextFactoryMock = new DataDbContextFactoryMock("DeleteItem_ShouldReturnError_OnFailure");
        TestItemRepository sut = new TestItemRepository(_mapperMock.Object, contextFactoryMock);

        var id = Guid.NewGuid();

        // Act
        var result = await Record.ExceptionAsync(() => sut.DeleteAsync(id, It.IsAny<Guid>()));

        // Assert
        result.Should()
                .NotBeNull().And
                .BeAssignableTo<SimpleRetailException>().And
                .Match<SimpleRetailException>(ex => ex.Code.Contains(nameof(Configuration.Messages.EntityDeleteFailedNotExists)));
    }

}
