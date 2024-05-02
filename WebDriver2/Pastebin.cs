using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V122.Browser;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDriver2
{
    public class Pastebin
    {
        private readonly IWebDriver driver;
        private readonly string url = @"https://pastebin.com/";

        public Pastebin(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.Id, Using = "postform-text")]
        public IWebElement CodeTextbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"w0\"]/div[5]/div[1]/div[3]/div/span")]
        public IWebElement SyntaxSelect { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id=\"w0\"]/div[5]/div[1]/div[4]/div/span")]
        public IWebElement ExpirationSelect { get; set; }

        [FindsBy(How = How.Id, Using = "postform-name")]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div[2]/div[1]/div[2]/div/form/div[5]/div[1]/div[11]/button")]
        public IWebElement SubmitButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "body > div.wrap > div.header > div > div > div.header__left > a.header__btn")]
        public IWebElement NewPaste { get; set; }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(url);
        }

        public void AddPasteText(string message)
        {   
            this.CodeTextbox.SendKeys(message);
            this.CodeTextbox.SendKeys(Keys.Enter);
        }

        public void ClearPasteText()
        {
            this.CodeTextbox.Clear();
        }

        public void SelectSyntax(string syntax)
        {
            this.SyntaxSelect.Click();
            var dropdownOptions = driver.FindElements(By.CssSelector("li[class *= 'select2-results__option'"));
            dropdownOptions.FirstOrDefault(x => x.Text.Equals(syntax))?.Click();
        }

        public void SelectExpiration(string expiration)
        {
            this.ExpirationSelect.Click();
            var dropdownOptions = driver.FindElements(By.CssSelector("li[class *= 'select2-results__option'"));
            dropdownOptions.FirstOrDefault(x => x.Text.Equals(expiration))?.Click();
        }

        public void AddTitle(string title)
        {
            this.Title.Clear();
            this.Title.SendKeys(title);
        }

        public void Submit()
        {
            this.SubmitButton.Click();
        }

        //public void Captcha()
        //{
        //    new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[@title='reCAPTCHA']")));
        //    var check = driver.FindElement(By.Id("recaptcha-anchor"));
        //    check.Click();
        //}
    }
}
