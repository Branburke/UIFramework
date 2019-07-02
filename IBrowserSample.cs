using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Automation.UserInterfaceTesting.Framework.Interfaces
{
    /// <summary>
    /// Interface that represents a web browser
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        /// Gets or sets the web driver.
        /// </summary>
        IWebDriver WebDriver { get; set; }
        /// <summary>
        /// Initializes a browser with the specified option
        /// </summary>
        /// <param name="browser">The browser option</param>
        /// <param name="url">The starting URL</param>
        void InitializeBrowser(Browsers browser, string url);
        /// <summary>
        /// Navigates to a specified URL
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        void NavigateTo(string url);
        /// <summary>
        /// Refreshes the current page.
        /// </summary>
        void RefreshPage();
        /// <summary>
        /// Gets a web element
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds to retry the search.</param>
        /// <returns>The web element found.</returns>
        IWebElement GetElement(By by, int timeoutInSeconds = 0);
        /// <summary>
        /// Gets a web element from a set of elements by a text value.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="textValue">The text value identify the element.</param>
        /// <returns>The first element that is found based on the search criteria.</returns>
        IWebElement GetElementFromElementsByTextValue(By by, string textValue);
        /// <summary>
        /// Gets a web element from a set of elements by a specified attribute value.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="attribute">The name of the attribute.</param>
        /// <param name="value">The text value of the attribute.</param>
        /// <returns>The first element that is found based on the search criteria.</returns>
        IWebElement GetElementFromElementsByAttribute(By by, string attribute, string value);
        /// <summary>
        /// Gets a list of web elements by a specified condition.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <returns>Gets a list of web elements based on specified search criteria.</returns>
        List<IWebElement> GetElements(By by, int timeoutInSeconds = 0);
        /// <summary>
        /// Maximizes a browser window.
        /// </summary>
        void MaximizeWindow();
        /// <summary>
        /// Sets the screen resolution
        /// </summary>
        /// <param name="width">The screen width</param>
        /// <param name="height">The screen height</param>
        void SetScreenResolution(int width, int height);
        /// <summary>
        /// Sets the browser position on the screen.
        /// </summary>
        /// <param name="x">The horizontal position.</param>
        /// <param name="y">The vertical position.</param>
        void SetScreenPosition(int x, int y);
        /// <summary>
        /// Scrolls the web page down.
        /// </summary>
        void ScrollDown();
        /// <summary>
        /// Scrolls the web page up.
        /// </summary>
        void ScrollUp();
        /// <summary>
        /// Zooms in a webpage.
        /// </summary>
        /// <param name="times">The number of times page zooms in.</param>
        void ZoomIn(int times);
        /// <summary>
        /// Resets the zoom to default.
        /// </summary>
        void ResetZoom();
        /// <summary>
        /// Checks if the page is at the specified URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>The result of the URL check.</returns>
        bool IsAt(string url);
        /// <summary>
        /// Closes a dialog box.
        /// </summary>
        /// <param name="accept">Value to determine if accept button should be pressed.  Defaults to false.</param>
        void CloseDialogBox(bool accept = false);
        /// <summary>
        /// Switches to an embedded frame.
        /// </summary>
        /// <param name="by">The condition used to search for the frame.</param>
        void SwitchToFrame(By by);
        /// <summary>
        /// Gets the current Window ID.
        /// </summary>
        /// <returns>The Window ID.</returns>
        string GetCurrentWindowId();
        /// <summary>
        /// Switches to the default content.
        /// </summary>
        void SwitchToDefaultContent();
        /// <summary>
        /// Switches to the parent frame.
        /// </summary>
        void SwitchToParentFrame();
        /// <summary>
        /// Gets the page source.
        /// </summary>
        /// <returns>Gets the html source of the page.</returns>
        string GetPageSource();
        /// <summary>
        /// Closes the browser.
        /// </summary>
        void Close();
        /// <summary>
        /// Waits for an element to disappear.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="retries">The number of retries.  Retries defaults to 100.</param>
        void WaitForElementToDisappear(By by, int retries = 100);
        /// <summary>
        /// Waits for an element to appear.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="retries">The number of retries.  Retries defaults to 50.</param>
        void WaitForElementToAppear(By by, int retries = 50);
        /// <summary>
        /// Executes an action and waits for it to complete.
        /// </summary>
        /// <param name="action">The specified action.</param>
        /// <param name="retries">The number of retries.</param>
        void WaitForAction(Action action, int retries);
        /// <summary>
        /// Checks if an element is displayed.
        /// </summary>
        /// <param name="by">The condition used to search for the frame.</param>
        /// <returns>The result of the check.</returns>
        bool IsElementDisplayed(By by);
    }
}
