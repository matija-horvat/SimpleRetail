using MediatR;
using SimpleRetail.API.Services;
using SimpleRetail.API.Validations.Item;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Tests.Common.Requests;
using SimpleRetail.Tests.Data.Dtos;

namespace SimpleRetail.Tests.API.Services;

public class ItemServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IItemRepository> _repositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ItemService _sut;

    private readonly ChangeItemRequest _request;
    private readonly ItemDto _serviceResponseMock;
    private readonly ItemDto? _serviceResponseNULLMock;
    private readonly IEnumerable<ItemDto> _serviceResponseLstMock;

    public ItemServiceTests()
    {
        _fixture = new Fixture();
        _repositoryMock = _fixture.Freeze<Mock<IItemRepository>>();
        _mediatorMock = _fixture.Freeze<Mock<IMediator>>();

        _sut = new ItemService(_repositoryMock.Object, _mediatorMock.Object); //creates implementation in-memory

        _request = ChangeItemRequestMock.Get();
        _serviceResponseMock = ItemDtoMock.Get();
        _serviceResponseLstMock = ItemDtoMock.GetList();
    }

    [Fact]
    public async Task GetAll_ShouldReturnObject_WhenDataFound()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(_serviceResponseLstMock);

        // Act
        var result = await _sut.GetAll(It.IsAny<int>(), It.IsAny<int>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(_serviceResponseLstMock.GetType());
        result.Should().BeEquivalentTo(_serviceResponseLstMock);

        _repositoryMock.Verify(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task GetAll_ShouldReturnNull_WhenDataNotFound()
    {
        // Arrange
        IEnumerable<ItemDto>? responseMock = null;
        _repositoryMock
            .Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(responseMock);

        // Act
        var result = await _sut.GetAll(It.IsAny<int>(), It.IsAny<int>()).ConfigureAwait(false);

        // Assert
        result.Should().BeNull();
        result.Should().BeEquivalentTo(responseMock);

        _repositoryMock.Verify(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task GetById_ShouldReturnObject_WhenDataFound()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(_serviceResponseMock);

        // Act
        var result = await _sut.GetById(It.IsAny<Guid>()).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(_serviceResponseMock.GetType());
        result.Should().BeEquivalentTo(_serviceResponseMock);

        _repositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenDataNotFound()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync(_serviceResponseNULLMock);

        // Act
        var result = await _sut.GetById(It.IsAny<Guid>()).ConfigureAwait(false);

        // Assert
        result.Should().BeNull();
        result.Should().BeEquivalentTo(_serviceResponseNULLMock);

        _repositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task Create_ShouldReturnObject_OnSuccess()
    {
        // Arrange
        _mediatorMock
            .Setup(x => x.Send(It.IsAny<ItemCommand>(), default))
            .ReturnsAsync(_serviceResponseMock);

        // Act
        var result = await _sut.CreateAsync(_request).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull().And.BeOfType(_serviceResponseMock.GetType());
        result.Should().BeEquivalentTo(_serviceResponseMock);

        _mediatorMock.Verify(x => x.Send(It.IsAny<ItemCommand>(), default), Times.Once());
    }

    [Fact]
    public async Task Update_ShouldReturnObject_OnSuccess()
    {
        // Arrange
        _mediatorMock
            .Setup(x => x.Send(It.IsAny<ItemCommand>(), default))
            .ReturnsAsync(_serviceResponseMock);

        // Act
        var result = await _sut.UpdateAsync(_request).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull().And.BeOfType(_serviceResponseMock.GetType());
        result.Should().BeEquivalentTo(_serviceResponseMock);

        _mediatorMock.Verify(x => x.Send(It.IsAny<ItemCommand>(), default), Times.Once());
    }

    [Fact]
    public async Task Delete_ShouldSucceffulReturn_IfOk()
    {
        // Arrange
        var id = It.IsAny<Guid>();
        var ChangeUserId = It.IsAny<Guid>();
        _repositoryMock.Setup(x => x.DeleteAsync(id, ChangeUserId)).Returns(Task.CompletedTask);

        // Act
        await _sut.DeleteAsync(id, ChangeUserId).ConfigureAwait(false);

        // Assert
        _repositoryMock.Verify(x => x.DeleteAsync(id, ChangeUserId), Times.Once());
    }
}
