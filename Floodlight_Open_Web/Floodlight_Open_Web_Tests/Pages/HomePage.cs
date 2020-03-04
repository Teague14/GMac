using System;
using Floodlight_Open_Web.Helpers;
using OpenQA.Selenium;

namespace Floodlight_Open_Web_Tests.Pages
{
    public class HomePage:BaseClass
    {
        #region Elements
        IWebElement btnHowItWorks = getDriver.FindElement(By.XPath("/html/body/app-root/app-main-nav/mat-toolbar/nav/ul/li/a[contains(.,'How it Works')]"));
        IWebElement btnWhyJoin = getDriver.FindElement(By.XPath("/html/body/app-root/app-main-nav/mat-toolbar/nav/ul/li/a[contains(.,'Why Join')]"));
        IWebElement btnForScientists = getDriver.FindElement(By.XPath("/html/body/app-root/app-main-nav/mat-toolbar/nav/ul/li/a[contains(.,'For Scientists')]"));
        IWebElement btnSeeData = getDriver.FindElement(By.XPath("//*[@id='view-data-link']/span[contains(.,'See Data')]"));
        IWebElement btnJoin = getDriver.FindElement(By.Id("join-trial-link"));
        #endregion
        #region Methods
        /// <summary>
        /// Clicks on the How it Works button
        /// </summary>
        public void ClickHowItWorksButton()
        {
            ScrollToItem(btnHowItWorks);
            btnHowItWorks.Click();
        }

        /// <summary>
        /// Clicks on the Why Join button
        /// </summary>
        public void ClickWhyJoinButton()
        {
            ScrollToItem(btnWhyJoin);
            btnWhyJoin.Click();
        }

        /// <summary>
        /// Clicks the For Scientists button
        /// </summary>
        public void ClickForScientistsButton()
        {
            ScrollToItem(btnForScientists);
            btnForScientists.Click();
        }

        /// <summary>
        /// Clicks the See Data button
        /// </summary>
        public void ClickSeeDataButton()
        {
            ScrollToItem(btnSeeData);
            btnSeeData.Click();
        }

        /// <summary>
        /// Clicks the Join button
        /// </summary>
        public JoinPage ClickJoinButton()
        {
            ScrollToItem(btnJoin);
            btnJoin.Click();
            return new JoinPage();
        }
        #endregion
    }
}
