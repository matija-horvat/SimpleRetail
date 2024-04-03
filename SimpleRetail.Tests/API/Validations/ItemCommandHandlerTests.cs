using SimpleRetail.API.Validations.Enums;
using SimpleRetail.API.Validations.Item;
using SimpleRetail.Common.Requests;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Tests.Common.Requests;
using SimpleRetail.Tests.Data.Dtos;

namespace SimpleRetail.Tests.API.Validations;

public class ItemCommandHandlerTests
{
    private readonly Mock<IItemRepository> _repositoryMock;
    private readonly ItemCommand _command;

    private readonly ItemCommandHandler _sut;

    public ItemCommandHandlerTests()
    {
        _repositoryMock = new Mock<IItemRepository>();
        _command = new ItemCommand(ChangeItemRequestMock.Get(), ChangeAction.INSERT);

        _sut = new ItemCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenInsertAction_ReturnsItemDto()
    {
        // Arrange
        var response = ItemDtoMock.Get();
        _repositoryMock.Setup(x => x.CreateAsync(It.IsAny<ChangeItemRequest>())).ReturnsAsync(response);

        // Act
        var result = await _sut.Handle(_command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(response.GetType());
        result.Should().BeEquivalentTo(response);

        _repositoryMock.Verify(x => x.CreateAsync(It.IsAny<ChangeItemRequest>()), Times.Once());
    }

    [Fact]
    public async Task Handle_WhenUpdateAction_ReturnsItemDto()
    {
        // Arrange
        _command.Action = ChangeAction.UPDATE;
        var response = ItemDtoMock.Get();
        _repositoryMock.Setup(x => x.UpdateAsync(It.IsAny<ChangeItemRequest>())).ReturnsAsync(response);

        // Act
        var result = await _sut.Handle(_command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(response.GetType());
        result.Should().BeEquivalentTo(response);

        _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<ChangeItemRequest>()), Times.Once());
    }
}
