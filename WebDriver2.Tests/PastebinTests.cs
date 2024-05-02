using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V123.IndexedDB;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Selenium.Extensions;
using Selenium.WebDriver.UndetectedChromeDriver;
using SeleniumExtras.WaitHelpers;

namespace WebDriver2.Tests
{
    public class Tests
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        [SetUp]
        public void Setup()
        {
            this.Driver = new FirefoxDriver();
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(30));
            this.Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TeardownTest()
        {
            this.Driver.Dispose();
        }

        [Test]
        public void Pastebin_CreateNewPost_CheckTitleAndSyntaxAndCode()
        {
            string username = "PasteBinTest124";
            string password = "password123456";
            string[] lines = new string[] { "git config--global user.name \"New Sheriff in Town\"", 
                "git reset $(git commit - tree HEAD ^{ tree } -m \"Legacy code\"",
                "git push origin master --force\";"
            };
            string title = "how to gain dominance among developers";
            string syntax = "Bash";
            string expiration = "10 Minutes";

            string linesConfirm = "git config--global user.name \"New Sheriff in Town\"\r\ngit reset $(git commit - tree HEAD ^{ tree } -m \"Legacy code\"\r\ngit push origin master --force\";\r\n";

            Pastebin pastebin = new Pastebin(this.Driver);

            pastebin.Navigate();

            this.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div/div/div[2]/div/button[2]"))).Click();
            this.Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("body > div.wrap > div.header > div > div > div.header__right > div > a.btn-sign.sign-in"))).Click();

            this.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("loginform-username"))).SendKeys(username);
            this.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("loginform-password"))).SendKeys(password);
            this.Wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id=\"w0\"]/div[4]/div[3]/button"))).Click();

            pastebin.NewPaste.Click();

            this.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div/div/div[2]/div/button[2]"))).Click();

            foreach (var line in lines)
            {
                pastebin.AddPasteText(line);
            }

            pastebin.SelectSyntax(syntax);
            pastebin.SelectExpiration(expiration);

            pastebin.AddTitle(title);

            var js = (IJavaScriptExecutor)this.Driver;
            js.ExecuteScript("window.scrollBy(0,500)", "");

            pastebin.Submit();

            this.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div/div/div/div[2]/div/button[2]"))).Click();

            //var codeTextboxElements = this.Driver.FindElements(By.CssSelector("div[class *= 'de1'"));
            //string code = this.Driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/textarea")).Text;
            string syntaxOnPage = this.Wait.Until(ExpectedConditions.ElementIsVisible((By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/div[1]/a[1]")))).Text;
            string code = this.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div[1]/div[1]/textarea"))).Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(title + " - Pastebin.com", Driver.Title);
                Assert.AreEqual(syntax, syntaxOnPage);
                Assert.AreEqual(linesConfirm, code);
                //for (int i = 0; i < lines.Length; i++)
                //{
                //    Assert.AreEqual(lines[i], codeTextboxElements[i].Text);
                //}
            });
        }
    }
}