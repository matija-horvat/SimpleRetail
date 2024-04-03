using SimpleRetail.API.Validations.Item;
using SimpleRetail.Tests.Common.Requests;

namespace SimpleRetail.Tests.API.Validations;

public class ItemCommandValidatorTests
{
    [Fact]
    public void Validate_WithValidCommand_ShouldPassValidation()
    {
        // Arrange
        var validator = new ItemCommandValidator();
        var command = new ItemCommand(ChangeItemRequestMock.Get());

        // Act
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithInvalidCommand_ShouldFailValidation()
    {
        // Arrange
        var validator = new ItemCommandValidator();
        var command = new ItemCommand(ChangeItemRequestMock.GetInValid());

        // Act
        var validationResult = validator.Validate(command);

        // Assert
        validationResult.IsValid.Should().BeFalse();
    }
}
