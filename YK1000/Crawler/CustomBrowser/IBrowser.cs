using OpenQA.Selenium;

namespace YK1000.Crawler.CustomBrowser;

public interface IBrowser
{
    IWebDriver GetDriver();
}