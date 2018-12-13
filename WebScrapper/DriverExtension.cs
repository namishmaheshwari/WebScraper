using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebScrapper
{
    internal class DriverExtension
    {
        public DriverExtension()
        {
        }

        public void ClickElement(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                //Console.WriteLine(driver.FindElement(by).Displayed && driver.FindElement(by).Enabled);
                //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", );
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(timeoutInSeconds * 5);
                wait.IgnoreExceptionTypes(typeof(WebException), typeof(WebDriverException), typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(HttpRequestException));
                wait.Until(ExpectedConditions.ElementExists(by));
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                wait.Until(ExpectedConditions.ElementToBeClickable(by));

                //string id=driver.FindElement(by).GetAttribute("id");
                //((IJavaScriptExecutor)driver).ExecuteScript("$('#id').click();");
                //wait.Until(drv => drv.FindElement(by).Displayed && drv.FindElement(by).Enabled);
                //Console.WriteLine(driver.FindElement(by).Displayed && driver.FindElement(by).Enabled);
                //return driver.FindElement(by);
            }
            //return driver.FindElement(by);
        }

        public void FindOrThrow(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(timeoutInSeconds * 5);
                wait.IgnoreExceptionTypes(typeof(WebException), typeof(WebDriverException), typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(HttpRequestException));
                try{
                    wait.Until(ExpectedConditions.ElementExists(by));
                    wait.Until(ExpectedConditions.ElementIsVisible(by));
                }catch(Exception e ){
                    throw e;
                }

            }
        }

        public void FindElement(IWebDriver driver, By by, int timeoutInSeconds){
            if(timeoutInSeconds>0){
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(timeoutInSeconds*5);
                wait.IgnoreExceptionTypes(typeof(WebException), typeof(WebDriverException), typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(HttpRequestException));
                wait.Until(ExpectedConditions.ElementExists(by));
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                //return driver.FindElement(by);
            }
            //return driver.FindElement(by);
        }

        //ICollection<IWebElement>
        public void FindElements(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(timeoutInSeconds * 5);
                wait.IgnoreExceptionTypes(typeof(WebException), typeof(WebDriverException), typeof(NoSuchElementException), typeof(ElementNotVisibleException), typeof(HttpRequestException));
                wait.Until(ExpectedConditions.ElementExists(by));
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                //return driver.FindElements(by);
            }
            //return driver.FindElements(by);
        }

        public void FindElement(IWebDriver driver,IWebElement element, By by, int timeoutInSeconds)
        {
            if(timeoutInSeconds>0){
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.PollingInterval = TimeSpan.FromMilliseconds(timeoutInSeconds * 5);
                wait.IgnoreExceptionTypes(typeof(WebException),typeof(WebDriverException), typeof(NoSuchElementException), typeof(ElementNotVisibleException),typeof(HttpRequestException),typeof(HttpRequestException));

                Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver Web) =>
                {
                    Console.WriteLine("Waiting for sttus to change");
                    if (element.FindElement(by).Displayed && element.FindElement(by).Enabled)
                    {
                        return true;
                    }

                    return false;
                });
                wait.Until(waitForElement);

                //string str=element.FindElement(by).Text;
                //Console.WriteLine(str);
                //return str;
            }
            //return null;
        }

        public void ImplicitWait(IWebDriver driver, int timeoutInSeconds){
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }
    }
}