using Crawler.CustomBrowser;
using ModelsLib;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Regex = ModelsLib.Const.Regex;

namespace Crawler.Option;

public class CjOption : IOption
{
    private const string URI = "https://members.cj.com/member/login/#/";
    private List<MissionModel> _mission = new();

    private IWebDriver _driver;
    public InputModel Input { get; private set; }
    /// <summary>
    /// 设置代理和登录信息
    /// </summary>
    /// <param name="model"></param>
    /// <param name="proxy"></param>
    /// <param name="port"></param>
    public CjOption(InputModel model, string proxy, string port)
    {
        var chrome = new Chrome(proxy, port);
        _driver = chrome.GetDriver();
        _driver.Navigate().GoToUrl(URI);
        Input = model;
        _ = Login();
    }
    
    public async Task<IOption> Login()
    {
        _driver.FindElement(By.Id("username")).SendKeys(Input.Account);
        _driver.FindElement(By.Id("password")).SendKeys(Input.Password);
        _driver.FindElement(By.Id("btn-login")).Click();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        await Init();
        return this;
    }

    public async Task<IOption> Init()
    {
        var delay = Task.Delay(1500);
        _driver.FindElement(By.CssSelector("#whole-page > header > div > nav > div > div > div > ul:nth-child(3) > li:nth-child(2) > a")).Click();
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        _driver.FindElement(By.Id("status_active")).Click();
        _driver.FindElement(By.Id("searchButton")).Click();
        var selected = _driver.FindElements(By.ClassName("adv-row"));
        foreach (var s in selected)
        {
            var name= s.FindElement(By.ClassName("adv-detail")).FindElement(By.ClassName("adv-name")).Text;
            if (name is null) throw new Exception("name is null");
            var link = s.FindElement(By.ClassName("adv-row-icon-anchor")).GetAttribute("href");
            _driver.ExecuteJavaScript("window.open('"+link+"')");
            var handler = _driver.WindowHandles;
            if (handler.Count <= 0)
            {
                throw new Exception("The new window is not exist");
            }
            _driver.SwitchTo().Window(handler[1]);
            await delay;
            while (true)
            {
                var id = _driver.FindElement(By.ClassName("switch-get-code")).GetAttribute("for-link-id");
                if (id is null || id.Length <= 0)
                {
                    await delay;
                    continue;
                }
                _driver.FindElement(By.ClassName("switch-get-code")).Click();
                break;
            }
            var c = "";
            while (true)
            {
                var code = _driver.FindElement(By.Id("codeTabs")).FindElement(By.TagName("textarea")).GetAttribute("value");
                if (code is null || code.Length <= 0)
                {
                    await delay;
                    continue;
                }
                c = code.Replace(Regex.NOLINE_PATTERN, "");
                break;
            }
            this._mission.Add(new MissionModel(Input, name, c));
            _driver.Close();
            _driver.SwitchTo().Window(handler[0]);
        }
        return this;
    }

    public List<MissionModel> GetMission()
    {
        return _mission;
    }

    public string GetPlatformName()
    {
        return "CJ";
    }

    public string GetId()
    {
        return Input.Id;
    }

    public string GetAccount()
    {
        return Input.Account;
    }

    public string GetSavePath()
    {
        return Input.SavePath;
    }
    private WebDriverWait Await()
    {
        Wait.Instance.WaitSeconds = 60;
        return Wait.Instance.ToWait(_driver);
    }
}