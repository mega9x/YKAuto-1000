using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;

namespace Crawler.CustomBrowser;

public class Chrome : IBrowser
{
    private IWebDriver _driver;
    /// <summary>
    /// 设置 Firefox 浏览器. proxy 必须为正确 IP
    /// </summary>
    /// <param name="proxy"></param>
    public Chrome(string proxy, string port)
    {
        ChromeOptions opt2 = new();
        var p = new Proxy
        {
            Kind = ProxyKind.Manual,
            IsAutoDetect = false,
            SocksVersion = 5,
            SocksProxy = proxy + ":" + port,
        };
        opt2.AddArgument("user-agent=Mozilla/6.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/104.0.5112.102 Safari/537.36");
        opt2.AddArgument("--private");
        opt2.AddArgument("--proxy-server="+"socks5://" + proxy + ":" + port);
        opt2.PageLoadStrategy = PageLoadStrategy.Normal; // 等待 Dom 树加载完成
        _driver = new ChromeDriver(opt2);
        _driver.Manage().Cookies.DeleteAllCookies();
        _driver.ExecuteJavaScript("Object.defineProperties(navigator, {webdriver:{get:()=>undefined}});");
    }
    
    public IWebDriver GetDriver()
    {
        return _driver;
    }
}