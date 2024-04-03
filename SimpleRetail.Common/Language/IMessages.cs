namespace SimpleRetail.Common.Language;

public interface IMessages
{
    string? Get(string methodCode);
    string UnhandledException();

    string EntityUpdateFailedNotExists();
    string EntityDeleteFailedNotExists();

    string ChangeUserIdEmptyError();

    string EntityCreateFailedAlreadyExists();
}
