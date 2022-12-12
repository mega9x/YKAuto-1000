using ModelsLib;

namespace YK1000.Crawler.Option;

/// <summary>
/// 爬虫接口
/// </summary>
public interface IOption
{
    /// <summary>
    /// 设置登录账号密码
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    IOption SetAccount(string name, string password);
    /// <summary>
    /// 登录
    /// </summary>
    /// <returns></returns>
    IOption Login();
    /// <summary>
    /// 获取任务代码, 任务名称
    /// </summary>
    /// <returns></returns>
    IOption Init();
    /// <summary>
    /// 获取已获取到的任务
    /// </summary>
    /// <returns></returns>
    List<Mission> GetMission();

}