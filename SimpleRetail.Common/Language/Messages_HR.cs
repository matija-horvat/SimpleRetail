using System.Reflection;

namespace SimpleRetail.Common.Language;

public class Messages_HR : IMessages
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

    public string UnhandledException() { return "Neobrađena iznimka."; }

    public string EntityUpdateFailedNotExists() { return "Ažuriranje entiteta nije uspjelo. Entitet s tim ID-om ne postoji."; }
    public string EntityDeleteFailedNotExists() { return "Brisanje entiteta nije uspjelo. Entitet s tim ID-om ne postoji."; }

    public string ChangeUserIdEmptyError() { return "Nedostaje podatak ChangeUserId."; }

    public string EntityCreateFailedAlreadyExists() { return "Kreiranje entiteta nije uspjelo. Entitet s tim ID-om već postoji."; }
}
