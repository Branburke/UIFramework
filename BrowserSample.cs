using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Automation.UserInterfaceTesting.Framework
{
    /// <summary>
    /// Class that represents a web browser
    /// </summary>
    public class Browser : IBrowser
    {
        private IThreadHelper _threadHelper;
        private IWebDriverFactory _webDriverFactory;
        private IWebDriverHelpers _webDriverHelpers;

        /// <summary>
        /// Gets or sets the web driver.
        /// </summary>
        public IWebDriver WebDriver { get; set; }

        /// <summary>
        /// Initializes the browser class.
        /// </summary>
        /// <param name="threadHelper">The thread helper.</param>
        /// <param name="webDriverFactory">The web driver factory.</param>
        /// <param name="webDriverHelpers">The web driver helpers.</param>
        public Browser(IThreadHelper threadHelper,
            IWebDriverFactory webDriverFactory,
            IWebDriverHelpers webDriverHelpers)
        {
            _threadHelper = threadHelper;
            _webDriverFactory = webDriverFactory;
            _webDriverHelpers = webDriverHelpers;
        }

        /// <summary>
        /// Initializes a browser with the specified option
        /// </summary>
        /// <param name="browser">The browser option</param>
        /// <param name="url">The starting URL</param>
        public void InitializeBrowser(Browsers browser, string url)
        {
            switch (browser)
            {
                case Browsers.InternetExplorerLocal:
                    {
                        WebDriver = _webDriverFactory.CreateInternetExplorerWebDriver(
                            new InternetExplorerOptions
                            {
                                InitialBrowserUrl = url,
                                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                                BrowserAttachTimeout = new TimeSpan(0, 0, 30),
                                PageLoadStrategy = PageLoadStrategy.Normal
                            });
                    }
                    break;
                case Browsers.InternetExplorerLocalWithClearedBrowserData:
                    {
                        WebDriver = _webDriverFactory.CreateInternetExplorerWebDriver(
                            new InternetExplorerOptions
                            {
                                InitialBrowserUrl = url,
                                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                                BrowserAttachTimeout = new TimeSpan(0, 0, 30),
                                PageLoadStrategy = PageLoadStrategy.Normal,
                                EnsureCleanSession = true
                            });
                    }
                    break;
                case Browsers.InternetExplorerRemoteWithClearedBrowserData:
                    {
                        WebDriver = _webDriverFactory.CreateRemoteWebDriver(new InternetExplorerOptions
                        {
                            InitialBrowserUrl = url,
                            IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                            BrowserAttachTimeout = new TimeSpan(0, 0, 30),
                            PageLoadStrategy = PageLoadStrategy.Normal,
                            EnsureCleanSession = true
                        });
                        WebDriver.Manage().Timeouts().AsynchronousJavaScript = new TimeSpan(0, 0, 30);
                    }
                    break;
                case Browsers.InternetExplorerRemote:
                    {
                        WebDriver = _webDriverFactory.CreateRemoteWebDriver(
                            new InternetExplorerOptions
                            {
                                InitialBrowserUrl = url,
                                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                                BrowserAttachTimeout = new TimeSpan(0, 0, 30),
                                PageLoadStrategy = PageLoadStrategy.Normal
                            });
                        WebDriver.Manage().Timeouts().AsynchronousJavaScript = new TimeSpan(0, 0, 30);
                    }
                    break;
                case Browsers.ChromeLocal:
                    {
                        var chromeOptions = new ChromeOptions();
						chromeOptions.AddAdditionalCapability("useAutomationExtension",false);
					    WebDriver = _webDriverFactory.CreateChromeWebDriver(chromeOptions);
                        WebDriver.Navigate().GoToUrl(url);
                    }
                    break;
                case Browsers.HeadlessChromeLinuxRemote:
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.PlatformName = "LINUX";
                        chromeOptions.AddArgument("headless");
                        chromeOptions.AcceptInsecureCertificates = true;
                        WebDriver = _webDriverFactory.CreateRemoteWebDriver(chromeOptions);
                        WebDriver.Navigate().GoToUrl(url);
                    }
                    break;
                case Browsers.HeadlessChromeWindowsRemote:
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.PlatformName = "WINDOWS";
                        chromeOptions.AddArgument("headless");
                        chromeOptions.AddArgument("disable-gpu");
                        chromeOptions.AddArgument("disable-extensions");
                        chromeOptions.AddArgument("start-maximized");
                        WebDriver = _webDriverFactory.CreateRemoteWebDriver(chromeOptions);
                        WebDriver.Navigate().GoToUrl(url);
                    }
                    break;
                case Browsers.ChromeLocalDeleteCookies:
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                        WebDriver = _webDriverFactory.CreateChromeWebDriver(chromeOptions);
                        WebDriver.Manage().Cookies.DeleteAllCookies();
                        WebDriver.Navigate().GoToUrl(url);
                    }
                    break;
                case Browsers.HeadlessChromeLinuxRemoteDeleteCookies:
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.PlatformName = "LINUX";
                        chromeOptions.AddArgument("headless");
                        chromeOptions.AcceptInsecureCertificates = true;
                        WebDriver = _webDriverFactory.CreateRemoteWebDriver(chromeOptions);
                        WebDriver.Manage().Cookies.DeleteAllCookies();
                        WebDriver.Navigate().GoToUrl(url);
                    }
                    break;
                case Browsers.HeadlessChromeWindowsRemoteDeleteCookies:
                    {
                        var chromeOptions = new ChromeOptions();
                        chromeOptions.PlatformName = "WINDOWS";
                        chromeOptions.AddArgument("headless");
                        chromeOptions.AddArgument("disable-gpu");
                        chromeOptions.AddArgument("disable-extensions");
                        chromeOptions.AddArgument("start-maximized");
                        WebDriver = _webDriverFactory.CreateRemoteWebDriver(chromeOptions);
                        WebDriver.Manage().Cookies.DeleteAllCookies();
                        WebDriver.Navigate().GoToUrl(url);
                    }
                    break;
                case Browsers.EdgeLocal:
                    {
                        var edgeOptions = new EdgeOptions();
                        WebDriver = _webDriverFactory.CreateEdgeWebDriver(edgeOptions);
                        WebDriver.Navigate().GoToUrl(url);
                    }
                    break;
            }
        }

        /// <summary>
        /// Gets a web element
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds to retry the search.</param>
        /// <returns>The web element found.</returns>
        public IWebElement GetElement(By by, int timeoutInSeconds = 0)
        {
            try
            {
                return _webDriverHelpers.FindElementWithWait(WebDriver, by, timeoutInSeconds);
            }
            catch (WebDriverTimeoutException)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a web element from a set of elements by a text value.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="textValue">The text value identify the element.</param>
        /// <returns>The first element that is found based on the search criteria.</returns>
        public IWebElement GetElementFromElementsByTextValue(By by, string textValue)
        {
            var elements = GetElements(by, 20);
            return elements.FirstOrDefault(element => element.Text.Equals(textValue));
        }

        /// <summary>
        /// Gets a web element from a set of elements by a specified attribute value.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="attribute">The name of the attribute.</param>
        /// <param name="value">The text value of the attribute.</param>
        /// <returns>The first element that is found based on the search criteria.</returns>
        public IWebElement GetElementFromElementsByAttribute(By by, string attribute, string value)
        {
            var elements = GetElements(by, 20);
            return elements.FirstOrDefault(element => element.GetAttribute(attribute) == value);
        }

        /// <summary>
        /// Gets a list of web elements by a specified condition.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <returns>Gets a list of web elements based on specified search criteria.</returns>
        public List<IWebElement> GetElements(By by, int timeoutInSeconds = 0)
        {
            try
            {
                return _webDriverHelpers.FindElementsWithWait(WebDriver, by, timeoutInSeconds);
            }
            catch (WebDriverTimeoutException)
            {
                return new List<IWebElement>();
            }
        }

        /// <summary>
        /// Zooms in a webpage.
        /// </summary>
        /// <param name="times">The number of times page zooms in.</param>
        public void ZoomIn(int times)
        {
            while (times > 0)
            {
                _webDriverHelpers.PerformZoomInAction(WebDriver);
                times--;
            }
        }

        /// <summary>
        /// Checks if the page is at the specified URL.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>The result of the URL check.</returns>
        public bool IsAt(string url)
        {
            var retries = 25;
            while (retries > 0)
            {
                _threadHelper.Sleep(200);
                --retries;
                if (WebDriver.Url.Contains(url)) return true;
            }
            return false;
        }

        /// <summary>
        /// Closes a dialog box.
        /// </summary>
        /// <param name="accept">Value to determine if accept button should be pressed.  Defaults to false.</param>
        /// <exception cref="NoAlertPresentException">Throws NoAlertPresentException when no alert dialog is open.</exception>
        public void CloseDialogBox(bool accept = false)
        {
            var retries = 25;
            while (retries >= 0)
            {
                try
                {
                    var alert = WebDriver.SwitchTo().Alert();
                    if (accept)
                    {
                        alert.Accept();
                    }
                    else
                    {
                        alert.Dismiss();
                    }
                    return;
                }
                catch (NoAlertPresentException)
                {
                    if (retries > 1)
                    {
                        _threadHelper.Sleep(200);
                        retries--;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Switches to an embedded frame.
        /// </summary>
        /// <param name="by">The condition used to search for the frame.</param>
        /// <exception cref="NoSuchElementException">Throws NoSuchElementException when unable to find the frame.</exception>
        public void SwitchToFrame(By by)
        {
            var retries = 25;
            while (retries > 0)
            {
                try
                {
                    WebDriver.SwitchTo().Frame(GetElement(by));
                    return;
                }
                catch (NoSuchElementException)
                {
                    _threadHelper.Sleep(500);
                    --retries;
                    if (retries == 0) throw;
                }
            }
        }

        /// <summary>
        /// Waits for an element to disappear.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="retries">The number of retries.  Retries defaults to 100.</param>
        public void WaitForElementToDisappear(By by, int retries = 100)
        {
            bool isElementDisplayed = true;
            while (isElementDisplayed && retries > 0)
            {
                try
                {
                    isElementDisplayed = _webDriverHelpers.IsElementDisplayed(WebDriver, by);
                    if (!isElementDisplayed) break;
                }
                catch
                {
                    // ignored but would like to look into handling this scenario.
                }
                _threadHelper.Sleep(200);
                --retries;
            }
        }

        /// <summary>
        /// Waits for an element to appear.
        /// </summary>
        /// <param name="by">The condition used to search for the element.</param>
        /// <param name="retries">The number of retries.  Retries defaults to 50.</param>
        /// <exception cref="TimeoutException">Throws TimeoutException when element fails to appear after allotted retries.</exception>
        public void WaitForElementToAppear(By by, int retries = 50)
        {
            var isElementDisplayed = false;

            while (!isElementDisplayed && retries > 0)
            {
                isElementDisplayed = _webDriverHelpers.IsElementDisplayed(WebDriver, by);

                --retries;
            }

            if (!isElementDisplayed && retries == 0)
                throw new TimeoutException("Element is not available.");
        }

        /// <summary>
        /// Executes an action and waits for it to complete.
        /// </summary>
        /// <param name="action">The specified action.</param>
        /// <param name="retries">The number of retries.</param>
        public void WaitForAction(Action action, int retries)
        {
            for (var i = retries; i > 0; i--)
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception)
                {
                    _threadHelper.Sleep(200);
                }
            }
        }

        /// <summary>
        /// Checks if an element is displayed.
        /// </summary>
        /// <param name="by">The condition used to search for the frame.</param>
        /// <returns>The result of the check.</returns>
        public bool IsElementDisplayed(By by) => _webDriverHelpers.IsElementDisplayed(WebDriver, by);
        /// <summary>
        /// Maximizes a browser window.
        /// </summary>
        public void MaximizeWindow() => WebDriver.Manage().Window.Maximize();
        /// <summary>
        /// Sets the screen resolution
        /// </summary>
        /// <param name="width">The screen width</param>
        /// <param name="height">The screen height</param>
        public void SetScreenResolution(int width, int height) => WebDriver.Manage().Window.Size = new Size(width, height);
        /// <summary>
        /// Sets the browser position on the screen.
        /// </summary>
        /// <param name="x">The horizontal position.</param>
        /// <param name="y">The vertical position.</param>
        public void SetScreenPosition(int x, int y) => WebDriver.Manage().Window.Position = new Point(x, y);
        /// <summary>
        /// Scrolls the web page down.
        /// </summary>
        public void ScrollDown() =>_webDriverHelpers.PerformScrollDownAction(WebDriver);
        /// <summary>
        /// Scrolls the web page up.
        /// </summary>
        public void ScrollUp() => _webDriverHelpers.PerformScrollUpAction(WebDriver);
        /// <summary>
        /// Navigates to a specified URL
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        public void NavigateTo(string url) => WebDriver.Navigate().GoToUrl(url);
        /// <summary>
        /// Refreshes the current page.
        /// </summary>
        public void RefreshPage() => WebDriver.Navigate().Refresh();
        /// <summary>
        /// Resets the zoom to default.
        /// </summary>
        public void ResetZoom() => _webDriverHelpers.PerformResetZoomAction(WebDriver);
        /// <summary>
        /// Gets the current Window ID.
        /// </summary>
        /// <returns>The Window ID.</returns>
        public string GetCurrentWindowId() => WebDriver.CurrentWindowHandle;
        /// <summary>
        /// Switches to the default content.
        /// </summary>
        public void SwitchToDefaultContent() => WebDriver.SwitchTo().DefaultContent();
        /// <summary>
        /// Switches to the parent frame.
        /// </summary>
        public void SwitchToParentFrame() => WebDriver.SwitchTo().ParentFrame();
        /// <summary>
        /// Gets the page source.
        /// </summary>
        /// <returns>Gets the html source of the page.</returns>
        public string GetPageSource() => WebDriver.PageSource;
        /// <summary>
        /// Closes the browser.
        /// </summary>
        public void Close() => WebDriver?.Quit();
    }
}