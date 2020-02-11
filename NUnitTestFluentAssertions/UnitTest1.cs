using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Tests
{
    public class Tests
    {
        private IWebDriver _driver;

        [OneTimeSetUp]
        public void Setup()
        {
            _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            _driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void AfterSuite()
        {
            _driver.Quit();
        }


        [Test]
        public void Test()
        {
            _driver.Url = "https://www.google.by/";
            var field = _driver.FindElement(By.XPath("//input[@name='q']"));
            var title1 = _driver.Title;
            field.SendKeys("Selenium");
            field.SendKeys(Keys.Enter);
            var title2 = _driver.Title;

            using (new AssertionScope())
            {
                title1.Should().Be("Google");
                title2.Should().Be("Selenium - Пошук Google");
            }                
        }
    }
}