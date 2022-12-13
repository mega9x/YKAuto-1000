using MiniExcelLibs;
using ModelsLib;

namespace ExcelLib.ExcelTools;

public class Entry
{
    public string filename { get; set; }
    public string FilePath { get; private set; }
    public List<string> Sheets { get; private set; }
    public Dictionary<string, List<AccountGroup>> AllRows { get; private set; } = new();
    public Entry(string path, string id)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        this.FilePath = Path.Combine(path, id + ".xlsx");
        this.filename = id + ".xlsx";
        if (!File.Exists(FilePath))
        {
            return;
        }
        Sheets = MiniExcel.GetSheetNames(FilePath);
        if (Sheets.Count <= 0)
        {
            return;
        }
        foreach (var s in Sheets)
        {
            var list = MiniExcel.Query<MissionModel>(FilePath, sheetName: s).ToList();
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

    public Entry AddData(string sheetName, AccountGroup data)
    {
        if (!AllRows.ContainsKey(sheetName))
        {
            Create(sheetName, data);
            return this;
        }

        var ifFound = AllRows[sheetName].Find(d => d.Account == data.Account);
        if (ifFound is null)
        {
            AllRows[sheetName].Add(data);
            return this;
        }
        AllRows[sheetName].Find(d => d.Account == data.Account)!.Combine(data);
        return this;
    }

    private void Create(string sheetName, AccountGroup data)
    {
        AllRows.Add(sheetName, new List<AccountGroup>()
        {
            data,
        });
    }
    public Entry Save()
    {
        File.Delete(FilePath);
        var sheetDataPair = new Dictionary<string, object>();
        foreach(var data in AllRows)
        {
            var list = new List<MissionModel>();
            foreach (var vAccountGroup in data.Value)
            {
                list.AddRange(vAccountGroup.Group);
            }
            sheetDataPair.Add(data.Key, list.ToArray());
        }
        MiniExcel.SaveAs(FilePath, sheetDataPair);
        return this;
    }
}