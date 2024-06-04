

using EaFramework.Config;
using EaFramework.Driver;
using Microsoft.Playwright;
using Xunit;
using System;


namespace Taxproject;

public class UnitTest1 : IClassFixture<PlaywrightDriverInitializer>
{
    private readonly PlaywrightDriver _playwrightDriver;
    private readonly TestSettings _testSettings;
    private static Random random = new Random();


    public UnitTest1(PlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _testSettings = ConfigReader.ReadConfig();
        _playwrightDriver = new PlaywrightDriver(_testSettings, playwrightDriverInitializer);
    }

    [Fact]
    public async Task Test1()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
        });

        var page = await _playwrightDriver.Page;

        var context = await browser.NewContextAsync();
        try
        {

            await page.GotoAsync(_testSettings.ApplicationUrl ?? throw new InvalidOperationException());

            await page.GetByPlaceholder("Email Address").ClickAsync();
            
            await page.GetByPlaceholder("Email Address").FillAsync("sandorkovacs122@gmail.com");
            
            await page.GetByPlaceholder("Password").ClickAsync();

            await page.GetByPlaceholder("Password").FillAsync("Sa123456!");

            await page.GetByRole(AriaRole.Button, new() { Name = "Sign in" }).ClickAsync();

            await page.Locator("div").Filter(new() { HasText = "Subscription summary The price you see here will change according to your settin" }).Nth(3).ClickAsync();

            await page.Locator(".col-md-8").ClickAsync();

            await page.GetByText("Czech Republic").First.ClickAsync();

            await page.GetByText("Czech Republic").ClickAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Help me get a VAT number" }).ClickAsync();

            await page.GetByRole(AriaRole.Button, new() { Name = "Next step" }).ClickAsync();

            await page.Locator("ng-select").Filter(new() { HasText = "×Company" }).Locator("span").Nth(2).ClickAsync();

            await page.GetByRole(AriaRole.Option, new() { Name = "Company" }).ClickAsync();

            await page.Locator("#companyLegalNameOfBusiness").ClickAsync();

            await page.GetByLabel("Incorporation number").ClickAsync();

            await page.GetByLabel("Incorporation number").FillAsync("2");

            await page.GetByLabel("State").ClickAsync();

            await page.GetByLabel("State").FillAsync("Hajdu-Bihar");

            await page.Locator("app-datepicker").GetByRole(AriaRole.Button).ClickAsync();

            await page.GetByRole(AriaRole.Gridcell, new() { Name = "Monday, June 3, 2024" }).GetByText("3").ClickAsync();

            await page.GetByLabel("State").ClickAsync();

            await page.GetByLabel("State").FillAsync("Hajdu-Bihar");

            await page.GetByLabel("ZIP/Post code").ClickAsync();

            await page.GetByLabel("ZIP/Post code").FillAsync("4031");

            await page.GetByLabel("City").ClickAsync();

            await page.GetByLabel("City").FillAsync("CityTest");

            await page.GetByLabel("Street").ClickAsync();

            await page.GetByLabel("Street").FillAsync("Test2");

            await page.GetByLabel("House number").ClickAsync();

            await page.GetByLabel("House number").FillAsync(random.Next(1, 101).ToString());

            await page.GetByRole(AriaRole.Button, new() { Name = "Next step" }).ClickAsync();

        }
        finally
        {
            await browser.CloseAsync();
        }
    }

}
