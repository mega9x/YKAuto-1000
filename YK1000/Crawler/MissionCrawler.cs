using ModelsLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using YK1000.Crawler.Option;

namespace YK1000.Crawler;

/// <summary>
/// 用于整理 Excel 对象
/// </summary>
public class MissionCrawler
{
    public IOption Option { get; set; }
    public MissionCrawler(IOption option)
    {
        this.Option = option;
    }

    public List<Mission> GetMission()
    {
        return Option.GetMission();
    }
}