using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Floodlight_Open_Web.Helpers
{
    public abstract class BaseClass
    {
        #region Elements
        public static string baseURL = "https://floodlightopen.com/en-US/";
        public static IWebDriver getDriver { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates a new instance of webdriver for the provided browser type
        /// </summary>
        /// <param name="browserType">Browser to be sued in test</param>
        public static void Init(BrowserType browserType)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Documents/Projects/GarysStuff/Floodlight_Open_Web/Floodlight_Open_Web/bin/Debug/netcoreapp3.1";
            switch (browserType)
            {
                case BrowserType.Chrome:
                    getDriver = new ChromeDriver(filePath);
                    break;
                case BrowserType.ChormeHeadless:
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("headless");
                    getDriver = new ChromeDriver(".", chromeOptions);
                    break;
                case BrowserType.InternetExplorer:
                    getDriver = new InternetExplorerDriver(".");
                    break;
                case BrowserType.Firefox:
                    getDriver = new FirefoxDriver(".");
                    break;
            }
            getDriver.Manage().Window.Maximize();
            getDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Goto(baseURL);
        }

        /// <summary>
        /// Disposes of the instance of the driver
        /// </summary>
        public static void Close()
        {
            getDriver.Quit();
            getDriver.Dispose();
        }

        /// <summary>
        /// Navigates to the specified url
        /// </summary>
        /// <param name="url">webpage address to navigate to</param>
        public static void Goto(string url)
        {
            getDriver.Url = url;
        }

        /// <summary>
        /// Selects the specified drop down list item from the specified drop down list
        /// </summary>
        /// <param name="element">IWebElemnent that is the drop down list</param>
        /// <param name="value">value to be selected in the list</param>
        public static void SelectFromDropDownList(IWebElement element, string value)
        {
            SelectElement ddl = new SelectElement(element);
            ddl.SelectByText(value);
        }

        /// <summary>
        /// Scrolls the screen to the specified IWebElement
        /// </summary>
        /// <param name="element">IWebElement</param>
        public static void ScrollToItem(IWebElement element)
        {
            ((IJavaScriptExecutor)getDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        /// <summary>
        /// Gets the Textual value of the IWebElement provided
        /// </summary>
        /// <param name="element">IWebElement containing text</param>
        /// <returns></returns>
        public static string GetElementText(IWebElement element)
        {
            return element.Text;
        }

        /// <summary>
        /// Gets the selected value in the drop down listed provided
        /// </summary>
        /// <param name="element">Drop Down List IWebElement</param>
        /// <returns></returns>
        public static string GetSelectedDropDown(IWebElement element)
        {
            SelectElement ddl = new SelectElement(element);
            return ddl.AllSelectedOptions.First().ToString();
        }

        /// <summary>
        /// Returns a list of items contained in the IWebElement drop down provided
        /// </summary>
        /// <param name="element">IWebElement of type Drop Down</param>
        /// <returns>List of ites contained in the drop down</returns>
        public static IList<IWebElement> GetSelectedListOptions(IWebElement element)
        {
            SelectElement ddl = new SelectElement(element);
            return ddl.AllSelectedOptions;
        }

        /// <summary>
        /// Checks to see if element is present and if not throws an exception
        /// </summary>
        /// <param name="element"></param>
        public static void AssertElementPresent(IWebElement element)
        {
            if (!CheckFieldDisplayed(element))
                throw new Exception(string.Format("Element Not Present exception"));
        }

        /// <summary>
        /// Hovers the mouse over selected element
        /// </summary>
        /// <param name="element"></param>
        public static void Hover(IWebElement element)
        {
            Actions actions = new Actions(getDriver);
            actions.MoveToElement(element).Perform();
        }

        /// <summary>
        /// Sets the checkbox element to the desired state
        /// </summary>
        /// <param name="element">IWebElement of type checkbox</param>
        /// <param name="state">True = checked, Flase = unchecked</param>
        public static void SetCheckBoxState(IWebElement element, bool state)
        {
            if (element.Selected != state)
            {
                element.Click();
            }
        }

        /// <summary>
        /// Waits for document state to be ready
        /// </summary>
        /// <param name="seconds">Time to wait for page to load</param>
        public static void WaitForPageLoad(int seconds)
        {
            WebDriverWait wait = new WebDriverWait(getDriver, new TimeSpan(0, 0, seconds));
            wait.Until((x) =>
            {
                return ((IJavaScriptExecutor)getDriver).ExecuteScript("return document.readyState").Equals("complete");
            });
        }

        /// <summary>
        /// Waits for ajax state to be ready
        /// </summary>
        /// <param name="seconds">Time to wait for ajax to load</param>
        public static bool WaitForAjaxReady(int waitTime)
        {
            WebDriverWait wait = new WebDriverWait(getDriver, new TimeSpan(0, 0, waitTime));
            return wait.Until<bool>((d) =>
            {
                return getDriver.FindElements(By.CssSelector(".waiting, .tb-loading")).Count == 0;
            });
        }

        /// <summary>
        /// Find an element when waiting for it to load on screen
        /// </summary>
        /// <param name="by">Method used to find element</param>
        /// <param name="timeout">time needed for elemet to load</param>
        /// <returns></returns>
        public static IWebElement WaitForElement(By by, int timeout)
        {
            if (timeout > 0)
            {
                var wait = new WebDriverWait(getDriver, TimeSpan.FromSeconds(timeout));
                bool t = wait.Until(drv => drv.FindElement(by)).Enabled;
                return wait.Until(drv => drv.FindElement(by));
            }
            return getDriver.FindElement(by);
        }

        /// <summary>
        /// Checks to see if the provided item is displayed in the drop down list
        /// </summary>
        /// <param name="element">IWebElement of type Drop down list</param>
        /// <param name="item">Value expected in the drop down list</param>
        /// <returns>Bool value indicating if item is in the list</returns>
        public bool CheckDdlContainsItem(IWebElement element, string item)
        {
            IList<IWebElement> ddlValues = element.FindElements(By.TagName("option"));
            IList<string> options = new List<string>();
            foreach (var ddlValue in ddlValues)
            {
                options.Add(ddlValue.GetAttribute("text").ToString());
            }
            var query = options.Where(x => x.Contains(item)).Any();
            return query;
        }

        /// <summary>
        /// Checks to see if element provided has the attribute displayed as true or false
        /// </summary>
        /// <param name="element">Element to check</param>
        /// <returns>bool value</returns>
        public static bool CheckFieldDisplayed(IWebElement element)
        {
            return element.Displayed;
        }

        /// <summary>
        /// Checks to see if element provided has the attribute enabled as true or false
        /// </summary>
        /// <param name="element">Element to check</param>
        /// <returns>bool value</returns>
        public static bool CheckFieldEnabled(IWebElement element)
        {
            return element.Enabled;
        }

        /// <summary>
        /// This will drag a web element and place it above another element
        /// </summary>
        /// <param name="from">element to move</param>
        /// <param name="to">where element has to be placed</param>
        public static void DragAndDropItem(IWebElement from, IWebElement to)
        {
            ScrollToItem(from);
            Actions action = new Actions(getDriver);
            action.DragAndDrop(from, to).Build().Perform();
        }

        /// <summary>
        /// Checks to see if an element exists on the page
        /// </summary>
        /// <param name="by">method to find element</param>
        /// <returns>true if element exists or false if it does not</returns>
        public static bool CheckElementExists(By by)
        {
            if (getDriver.FindElements(by).Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }

    public enum BrowserType
    {
        InternetExplorer,
        Firefox,
        Chrome,
        ChormeHeadless
    }
}
