using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriver2
{
    public class PastebinCreated
    {
        private readonly IWebDriver driver;
        private readonly string url = @"https://pastebin.com/34dhf5mH";

        public PastebinCreated(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "postform-text")]
        public IWebElement CodeTextbox { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[1]/div[2]/div[1]/div[2]/div[4]/div[1]/div[1]/a[1]")]
        public IWebElement Syntax { get; set; }

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(url);
        }

        public void ValidatePastebinCreated(string title, string syntax, string code)
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(title, driver.Title);
                Assert.AreEqual(syntax, this.Syntax.Text);
                //Assert.AreEqual(this.Syntax.Text, syntax);
            });
        }

    }
}
