using System.Reflection;

namespace SimpleRetail.Common.Language;

public class Messages_EN : IMessages
{
    public string? Get(string methodCode)
    {
        MethodInfo method = GetType().GetMethod(methodCode);
        if (method != null && method.IsPublic && method.ReturnType == typeof(string))
        {
            return method.Invoke(this, null) as string;
        }
        else
        {
            return UnhandledException();
        }
    }

    public string UnhandledException() { return "Unhandled Exception."; }


    public string EntityUpdateFailedNotExists() { return "Failed to update entity. An Entity with that id not exists."; }
    public string EntityDeleteFailedNotExists(){ return "Failed to delete entity. An Entity with that id not exists."; }

    public string ChangeUserIdEmptyError() { return "ChangeUserId cannot be empty."; }

    public string EntityCreateFailedAlreadyExists() { return "Failed to create entity. An Entity with that id already exists."; }

    public string PropertyInsertDateNotExists(){ return "OrderBy Exception: Property 'InsertDate' not found in this entity."; }
}
