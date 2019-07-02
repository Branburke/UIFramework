using Automation.UserInterfaceTesting.Framework.Interfaces;
using Automation.UserInterfaceTesting.Helpers.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Automation.UserInterfaceTesting.Helpers
{
    /// <summary>
    /// Class for web element helpers.
    /// </summary>
    public class WebElementHelper : IWebElementHelper
    {
        /// <summary>
        /// Performs a double click action on a web element.
        /// </summary>
        /// <param name="element">The web element.</param>
        /// <param name="browser">The browser object.</param>
        public void DoubleClick(IWebElement element, IBrowser browser)
        {
            var sequence = new Actions(browser.WebDriver);
            sequence.DoubleClick(element);
            sequence.Perform();
        }

        /// <summary>
        /// Perform the Hover Over action on a web element.
        /// </summary>
        /// <param name="element">The web element.</param>
        /// <param name="browser">The browser object.</param>
        public void HoverOver(IWebElement element, IBrowser browser)
        {
            var sequence = new Actions(browser.WebDriver);
            sequence.MoveToElement(element).Build().Perform();
        }

        /// <summary>
        /// Scrolls to a specified element in the browser.
        /// </summary>
        /// <param name="element">The web element.</param>
        /// <param name="browser">The browser object.</param>
        public void ScrollToElement(IWebElement element, IBrowser browser)
        {
            var sequence = new Actions(browser.WebDriver);
            sequence.MoveToElement(element);
            sequence.Perform();
        }

        /// <summary>
        /// Clicks on an element and attempts to move to another location in the browser.
        /// </summary>
        /// <param name="element">The web element.</param>
        /// <param name="browser">The browser object.</param>
        /// <param name="xOffset">The horizontal position.</param>
        /// <param name="yOffset">The vertical position.</param>
        public void ClickAndMove(IWebElement element, IBrowser browser, int xOffset, int yOffset)
        {
            var sequence = new Actions(browser.WebDriver);
            sequence.ClickAndHold(element);
            sequence.MoveByOffset(xOffset, yOffset);
            sequence.Perform();
        }

        /// <summary>
        /// Gets the parent element.
        /// </summary>
        /// <param name="element">The web element.</param>
        /// <returns>The parent element of the specified web element.</returns>
        public IWebElement GetParent(IWebElement element) => element.FindElement(By.XPath(".."));
    }
}
