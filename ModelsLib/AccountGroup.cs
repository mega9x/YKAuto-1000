using System.Net.Sockets;

namespace ModelsLib;

public class AccountGroup
{
    public string Account { get; set; }
    public List<MissionModel> Group { get; set; }
    public AccountGroup Combine(AccountGroup accountGroup)
    {
        var excepted = accountGroup.Group.Except(Group);
        Group.AddRange(excepted);
        return this;
    }
}