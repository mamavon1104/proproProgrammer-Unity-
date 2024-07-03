enum GetNameType
{
    FirstNameOnly,
    LastNameOnly,
    FullName,
}
public class CustomerCharacter
{
    internal string firstName, lastName;

    internal CustomerCharacter(string firstName, string lastName)
    {
        firstName = this.firstName;
        lastName = this.lastName;
    }

    internal string GetName(GetNameType getType) //勝手にフルネーム以外も追加しちゃう。
    {
        return getType switch
        {
            GetNameType.FirstNameOnly => firstName,
            GetNameType.LastNameOnly => lastName,
            GetNameType.FullName => firstName + lastName,
            _ => "Error",
        };
    }
}

/*
public class CustomerCharacter
{
    internal string characterName;

    internal CustomerCharacter(string name)
    {
        characterName = name;
    }

    internal string GetName()
    {
        return characterName;
    }
}
*/