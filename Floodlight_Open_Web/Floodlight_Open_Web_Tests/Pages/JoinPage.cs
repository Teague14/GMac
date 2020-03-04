using Floodlight_Open_Web.Helpers;
using OpenQA.Selenium;

namespace Floodlight_Open_Web_Tests.Pages
{
    public class JoinPage:BaseClass
    {
        #region Elements
        IWebElement btnContinue = getDriver.FindElement(By.CssSelector("body > app-root > div > app-join > section.content-block.join-floodlight-open > article.join-trial-intro > p.button-con > a > span"));
        #endregion
        #region Methods
        /// <summary>
        /// Click Continue button
        /// </summary>
        public void ClickContinueButton()
        {
            btnContinue.Click();
        }
        #endregion
    }
}
