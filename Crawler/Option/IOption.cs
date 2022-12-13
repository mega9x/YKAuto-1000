using ModelsLib;

namespace Crawler.Option;

/// <summary>
/// 爬虫接口
/// </summary>
public interface IOption
{
    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    Task<IOption> Login();
    /// <summary>
    /// 获取任务代码, 任务名称
    /// </summary>
    /// <returns></returns>
    Task<IOption> Init();
    /// <summary>
    /// 获取已获取到的任务
    /// </summary>
    /// <returns></returns>
    List<MissionModel> GetMission();

    string GetPlatformName();
    string GetId();
    string GetAccount();
    string GetSavePath();

}