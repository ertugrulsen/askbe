using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace AskDefinexFunctionalTest.Steps
{
    [Binding]
    public class LoginSteps
    {

        IWebDriver driver = new ChromeDriver();

        [Given(@"I am at the login page")]
        public void GivenIAmAtTheLoginPage()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://104.154.140.175:3000/");
        }
        
        [When(@"I fill in the following form")]
        public void WhenIFillInTheFollowingForm(Table table)
        {
            IWebElement emailText = driver.FindElement(By.Id("login_username"));
            emailText.SendKeys("test");
            IWebElement passwordText = driver.FindElement(By.Id("login_password"));
            passwordText.SendKeys("123");
        }
        
        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            IWebElement loginButton = driver.FindElement(By.XPath("//*[@id='app']/div/div/div/div/div/form/div[4]/button/span"));
            loginButton.Click();
        }
        
        [Then(@"I should be at the home page")]
        public void ThenIShouldBeAtTheHomePage()
        {
            IWebElement element = driver.FindElement(By.TagName("input"));
            String title = element.GetAttribute("title");
            Assert.Equal("DefineX Template", title);
        }
        [AfterScenario]
        public void Dispose()
        {
            driver.Dispose();
        }
    }
}
