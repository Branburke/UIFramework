using Automation.Core.Framework;
using Automation.Core.Framework.Interfaces;
using UserInterfaceTesting.Framework.Interfaces;
using Automation.UserInterfaceTesting.Helpers.Interfaces;
using Automation.UserInterfaceTesting.Services.Interfaces;
using System;
using Xunit;

namespace Automation.UserInterfaceTesting.Framework
{
    /// <summary>
    /// The base class for a UI test.
    /// </summary>
    /// <typeparam name="T">The type of the test class.</typeparam>
    public abstract class UI_TestBase<T> : TestBase, IDisposable, IClassFixture<TestResultsFixture<T>> where T : class, new()
    {
        /// <summary>
        /// Gets or Sets he class representing the web browser.
        /// </summary>
        public IBrowser Browser { get; set; }

        /// <summary>
        /// Gets or Sets the settings manager.
        /// </summary>
        public ISettingsManager SettingsManager { get; set; } 

        /// <summary>
        /// Gets or Sets the stored procedure runner.
        /// </summary>
        public IStoredProcedureRunner StoredProcedureRunner { get; set; }

        /// <summary>
        /// Gets or Sets the web element helper.
        /// </summary>
        public IWebElementHelper WebElementHelper { get; set; }

        /// <summary>
        /// Gets or Sets the thread helper.
        /// </summary>
        public IThreadHelper ThreadHelper { get; set; }

        /// <summary>
        /// Gets or Sets the ThycoticService
        /// </summary>
        public IThycoticService ThycoticService { get; set; }

        /// <summary>
        /// Initializes the UI test class.
        /// </summary>
        public UI_TestBase()
        {
            SettingsManager = GetService<ISettingsManager>();
            StoredProcedureRunner = GetService<IStoredProcedureRunner>();
            WebElementHelper = GetService<IWebElementHelper>();
            Browser = GetService<IBrowser>();
            ThreadHelper = GetService<IThreadHelper>();
            ThycoticService = GetService<IThycoticService>();

            var directoryHelper = GetService<IDirectoryHelper>();
            var environmentHelper = GetService<IEnvironmentHelper>();

            if (!environmentHelper.GetEnvironmentVariable("PATH").Contains(directoryHelper.GetCurrentDirectory()))
            {
                environmentHelper.SetEnvironmentVariable("PATH", environmentHelper.GetEnvironmentVariable("PATH") + ";" + directoryHelper.GetCurrentDirectory());
            }

            Setup();
        }

        /// <summary>
        /// Ensures the browser is closed.
        /// </summary>
        public void Dispose()
        {
            Browser.Close();
        }

        /// <summary>
        /// Sets up required items for a test class.
        /// </summary>
        protected abstract void Setup();
    }
}
