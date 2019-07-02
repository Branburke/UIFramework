using Automation.UserInterfaceTesting.Framework;
using Xunit;
using Automation.Core.Framework;
using CampaignAutomationTool.FunctionalTests.Pages;
using CampaignAutomation.UserInterfaceTesting.Enumerations;
using CampaignAutomationTool.FunctionalTests.Enumerations;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CampaignAutomationTool.FunctionalTests.Tests
{
    public class Campaigns_Test : CatUI_TestBase
    {
		[Fact]
        [TestReport(reportTestId: "", team: "", reportTestName: "", category:"")]
        public void LoginToCATWelcomePage()
        {
            CatUI.LoginPage.LoginAs(Browser, User.AutoQA_DEV);
            Assert.True(CatUI.WasAbleToLogin(Browser));
        }

	    [Fact]
	    [TestReport(reportTestId: "", team: "", reportTestName: "", category: "")]
	    public void NavigateToCampaignPageFromCATHomePage()
	    {
		    CatUI.LoginPage.LoginAs(Browser, User.AutoQA_INT);
		    Assert.True(CatUI.WasAbleToLogin(Browser));
			CatUI.WelcomePage.NavigateToCampaignPageUsingCard(Browser);
			Assert.True(Browser.IsAt(Campaigns.Url));
			CatUI.NavigateToWelcomePage();
			Assert.True(Browser.IsAt(WelcomePage.Url));
			CatUI.WelcomePage.NavigateToCampaignPageUsingNavigationBar(Browser);
			Assert.True(Browser.IsAt(Campaigns.Url));
	    }

	    [Fact]
	    [TestReport(reportTestId: "", team: "", reportTestName: "", category: "")]
	    public void NewCampaignFieldValidationDisplayedWhenNoAnswersGiven()
	    {
		    CatUI.LoginPage.LoginAs(Browser, User.AutoQA_CRT);
		    Assert.True(CatUI.WasAbleToLogin(Browser));
		    CatUI.WelcomePage.NavigateToCampaignPageUsingCard(Browser);
		    Assert.True(Browser.IsAt(Campaigns.Url));
			CatUI.Campaigns.CampaignPageFieldValidationWithNoAnwers(Browser);
			Assert.True(CatUI.Campaigns.NewCampaignFieldValidationMessageDisplayed(Browser));
	    }

	}
}
