using Crawler.Option;
using ExcelLib.ExcelTools;
using ModelsLib;

namespace Crawler;

/// <summary>
/// 用于整理 Excel 对象
/// </summary>
public class MissionCrawler
{
    public IOption Option { get; set; }
    public Entry Entry { get; set; }
    public MissionCrawler(IOption option)
    {
        this.Option = option;
        Entry = new Entry(option.GetSavePath(), option.GetId());
    }
    public List<MissionModel> GetMission()
    {
        return Option.GetMission();
    }
    public List<MissionModel> SaveToExcel()
    {
        var missions = Option.GetMission();
        var group = new AccountGroup()
        {
            Account = Option.GetAccount(),
            Group = Option.GetMission(),
        };
        Entry.AddData(Option.GetPlatformName(), group).Save();
        return Option.GetMission();
    }
}