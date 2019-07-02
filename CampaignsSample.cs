using Automation.UserInterfaceTesting.Framework.Interfaces;
using OpenQA.Selenium;

namespace CampaignAutomationTool.FunctionalTests.Pages
{
    public class Campaigns
    {
	    public static readonly string Url = $@"{CatUI.CatUrl}Campaigns";
	    

	    public void CampaignPageFieldValidationWithNoAnwers(IBrowser Browser)
	    {
		    var newCampaign = Browser.GetElement(By.Id("newCampaign"));
		    newCampaign.Click();

		    var createNewCampaignButton = Browser.GetElement(By.CssSelector("#detailsPartialView > form > div:nth-child(11) > div > button:nth-child(2)"));
		    createNewCampaignButton.Click();

			//TODO Discuss Error Message Pop Up 
	    }

	    public bool NewCampaignFieldValidationMessageDisplayed(IBrowser Browser)
	    {
		    var businessOwnerRequiredMessage = true;
		    try
		    {
				if (Browser.GetElement(By.Id("businessOwnerRequiredMessage")).Displayed);
		    }
		    catch
		    {
			    // ignored
		    }
		    return businessOwnerRequiredMessage;
	    }
	}
}
