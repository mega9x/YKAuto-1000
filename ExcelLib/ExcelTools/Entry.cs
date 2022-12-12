using MiniExcelLibs;
using ModelsLib;

namespace ExcelLib.ExcelTools;

public class Entry
{
    public string FilePath { get; private set; }
    public List<string> Sheets { get; private set; }
    public Dictionary<string, List<AccountGroup>> AllRows { get; private set; } = new();
    public Entry(string path)
    {
        this.FilePath = path;
        Sheets = MiniExcel.GetSheetNames(path);
        if (Sheets.Count <= 0)
        {
            return;
        }
        foreach (var s in Sheets)
        {
            var list = MiniExcel.Query<MissionModel>(path, sheetName: s).ToList();
            if (list.Count <= 0)
            {
                return;
            }
            var allAccounts = list.Select(p => p.Account);
            var accountGroups = 
                (from account in allAccounts let @group =
                    list.FindAll(f => f.Account == account) select new AccountGroup()
                {
                    Account = account, Group = @group
                }).ToList();
            AllRows[s] = accountGroups;
        }
    }

    public Entry AddData(string sheetName, MissionModel data)
    {
        if (!AllRows.ContainsKey(sheetName))
        {
            Create(sheetName, data);
            return this;
        }
        var ifFound = AllRows[sheetName]
            .Find(d => d.Account == data.Account)!
            .Group.Exists(d => d.Title.Contains(data.Title));
        if (!ifFound)
        {
            AllRows[sheetName]
                .Find(d => d.Account == data.Account)!
                .Group.Add(data);
        }
        return this;
    }

    private void Create(string sheetName, MissionModel data)
    {
        AllRows.Add(sheetName, new List<AccountGroup>()
        {
            new AccountGroup()
            {
                Account = data.Account,
                Group = new List<MissionModel>()
                {
                    data
                }
            }
        });
    }
    public Entry Save()
    {
        return this;
    }
}