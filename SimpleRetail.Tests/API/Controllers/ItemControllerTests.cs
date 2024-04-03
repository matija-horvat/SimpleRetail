using Microsoft.AspNetCore.Mvc;
using SimpleRetail.BL.Contracts;
using SimpleRetail.API.Controllers;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Tests.Common.Requests;
using SimpleRetail.Tests.Data.Dtos;

namespace SimpleRetail.Tests.API.Controllers;

public class ItemControllerTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IItemService> _serviceMock;
    private readonly ItemController _sut;

    private readonly ChangeItemRequest _request;
    private readonly ItemDto _serviceResponseMock;
    private readonly ItemDto? _serviceResponseNULLMock;
    private readonly IEnumerable<ItemDto> _serviceResponseLstMock;
    private readonly IEnumerable<ItemDto> _serviceResponseEmptyMock;

    public ItemControllerTests()
    {
        _fixture = new Fixture();
        _serviceMock = _fixture.Freeze<Mock<IItemService>>();
        _sut = new ItemController(_serviceMock.Object); //creates implementation in-memory

        _request = ChangeItemRequestMock.Get();
        _serviceResponseMock = ItemDtoMock.Get();
        _serviceResponseLstMock = ItemDtoMock.GetList();
        _serviceResponseEmptyMock = ItemDtoMock.GetEmptyList();
    }

    [Fact]
    public async Task GetAll_ShouldReturnOk_WhenDataFound()
    {
        // Arrange
        _serviceMock
            .Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(_serviceResponseLstMock);

        // Act
        var result = await _sut.GetAll(It.IsAny<int>(), It.IsAny<int>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Result.As<OkObjectResult>().Value.Should().BeOfType(_serviceResponseLstMock.GetType());

        _serviceMock.Verify(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task GetAll_ShouldReturnNotFound_WhenDataNotFound()
    {
        // Arrange
        _serviceMock
            .Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(_serviceResponseEmptyMock);

        // Act
        var result = await _sut.GetAll(It.IsAny<int>(), It.IsAny<int>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeAssignableTo<NotFoundObjectResult>();

        _serviceMock.Verify(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenItemFound()
    {
        // Arrange
        _serviceMock
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(_serviceResponseMock);

        // Act
        var result = await _sut.Get(It.IsAny<Guid>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Result.As<OkObjectResult>().Value.Should().BeOfType(_serviceResponseMock.GetType());

        _serviceMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenItemNotFound()
    {
        // Arrange
        _serviceMock
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(_serviceResponseNULLMock);

        // Act
        var result = await _sut.Get(It.IsAny<Guid>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Result.Should().BeAssignableTo<NotFoundObjectResult>();

        _serviceMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task CreateItem_ShouldReturnOkResponse_WhenValidRequest()
    {
        // Arrange
        _serviceMock
            .Setup(x => x.CreateAsync(_request))
            .ReturnsAsync(_serviceResponseMock);

        // Act
        var result = await _sut.Post(_request).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<OkObjectResult>();

        _serviceMock.Verify(x => x.CreateAsync(_request), Times.Once());
    }

    [Fact]
    public async Task UpdateItem_ShouldReturnOkResponse_WhenValidRequest()
    {
        // Arrange
        _serviceMock
            .Setup(x => x.UpdateAsync(_request))
            .ReturnsAsync(_serviceResponseMock);

        // Act
        var result = await _sut.Put(_request).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<OkObjectResult>();

        _serviceMock.Verify(x => x.UpdateAsync(_request), Times.Once());
    }

    [Fact]
    public async Task DeleteItem_ShouldReturnOkResponse_WhenValidRequest()
    {
        // Arrange
        var id = It.IsAny<Guid>();
        var ChangeUserId = It.IsAny<Guid>();
        _serviceMock.Setup(x => x.DeleteAsync(id, ChangeUserId)).Returns(Task.CompletedTask);

        // Act
        var result = await _sut.Delete(id, ChangeUserId).ConfigureAwait(false);


        // Assert
        result.Should().NotBeNull().And.BeAssignableTo<OkResult>();

        _serviceMock.Verify(x => x.DeleteAsync(id, ChangeUserId), Times.Once());
    }
}
