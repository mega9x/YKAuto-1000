using ModelsLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using YK1000.Crawler.CustomBrowser;

namespace YK1000.Crawler.Option;

public class CjOption : IOption
{
    private const string URI = "https://members.cj.com/member/login/#/";
    private List<Mission> _mission = new();
    private string AccountName { get; set; }
    private string AccountPassword { get;  set; }

    private IWebDriver _driver;
    /// <summary>
    /// name 是账号名, password 是账号密码, proxy 是代理 ip
    /// </summary>
    /// <param name="name"></param>
    /// <param name="password"></param>
    /// <param name="proxy"></param>
    public CjOption(string name, string password, string proxy, string port)
    {
        var firefox = new Chrome(proxy, port);
        _driver = firefox.GetDriver();
        _driver.Navigate().GoToUrl(URI);
        this.SetAccount(name, password);
    }

    public IOption SetAccount(string name, string password)
    {
        this.AccountName = name;
        this.AccountPassword = name;
        Login();
        return this;
    }

    public IOption Login()
    {
        _driver.FindElement(By.Id("username")).SendKeys(AccountName);
        _driver.FindElement(By.Id("AccountPassword")).SendKeys(AccountPassword);
        _driver.FindElement(By.Id("btn-login")).Click();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        Init();
        return this;
    }

    public IOption Init()
    {
        _driver.FindElement(By.CssSelector("#whole-page > header > div > nav > div > div > div > ul:nth-child(3) > li:nth-child(2) > a")).Click();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        _driver.FindElement(By.Id("status_active")).Click();
        _driver.FindElement(By.Id("searchButton")).Click();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        var selected = _driver.FindElements(By.ClassName("adv-row"));
        foreach (var s in selected)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            var name= s.FindElement(By.ClassName("adv-detail")).FindElement(By.ClassName("category-name")).Text;
            if (name is null) continue;
            s.FindElement(By.ClassName("adv-row-icon-anchor")).Click();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            var code = _driver.FindElement(By.Id("codeTabs")).FindElement(By.TagName("textarea")).GetAttribute("value");
            this._mission.Add(new Mission
            {
                Code = code,
                Name = name,
            });
            _driver.Navigate().Back();
            
        }
        return this;
    }

    public List<Mission> GetMission()
    {
        return _mission;
    }
}