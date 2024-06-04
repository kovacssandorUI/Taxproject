

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

        await page.GotoAsync(_testSettings.ApplicationUrl ?? throw new InvalidOperationException());

        // await page.GotoAsync("https://login.taxually.com/taxuallyb2c.onmicrosoft.com/b2c_1_taxually_signup_signin/oauth2/v2.0/authorize?client_id=f8c607a5-b43a-40b5-bf80-70ecd4285a9c&scope=openid%20https%3A%2F%2Ftaxuallyb2c.onmicrosoft.com%2Fprod-core-api%2Fread-write%20profile%20offline_access&redirect_uri=https%3A%2F%2Fapp.taxually.com%2F&client-request-id=8e6dbb83-e9cc-4d63-bee0-36f443f35979&response_mode=fragment&response_type=code&x-client-SKU=msal.js.browser&x-client-VER=3.7.0&client_info=1&code_challenge=Imzf_pQvc0MsJecW1HWYU3tBmJhedNB0A2-l27NLWJ8&code_challenge_method=S256&nonce=3e884354-4b7a-44e9-8199-d97b5ab6add4&state=eyJpZCI6ImVlNzkzNTIxLWIzZTEtNDJlYS05YzE5LWY0NzU5ZDdjYzM4MCIsIm1ldGEiOnsiaW50ZXJhY3Rpb25UeXBlIjoicmVkaXJlY3QifX0%3D");

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
}