# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a C# Selenium-based test automation framework supporting multi-browser web automation and API/database testing. The solution consists of:

- **Selenium.Framework.Core**: Core automation library with WebDriver abstractions, Page Object Model base classes, and test lifecycle management
- **Testing.Infrastructure**: Support library for API testing, database queries, test data generation, and file operations
- **Selenium.Sample**: Example test implementation demonstrating framework usage
- **\*.UnitTest projects**: Framework unit tests using MSTest and xUnit

Target framework: .NET Standard 2.0 (libraries), .NET Core 3.0+ (test projects)

## Building and Testing

### Build the entire solution
```bash
dotnet build Selenium.Framework.sln
```

### Build specific project
```bash
dotnet build Selenium.Framework.Core/Selenium.Framework.Core.csproj
dotnet build Testing.Infrastructure/Testing.Infrastructure.csproj
```

### Run all tests
```bash
dotnet test Selenium.Framework.sln
```

### Run tests for specific project
```bash
# MSTest-based tests (Framework.Core)
dotnet test Selenium.Framework.Core.UnitTest/Selenium.Framework.Core.UnitTest.csproj

# xUnit-based tests (Infrastructure)
dotnet test Testing.Infrastructure.UnitTest/Testing.Infrastructure.UnitTest.csproj
```

### Run a single test method
```bash
# MSTest example
dotnet test --filter "FullyQualifiedName~Selenium.Framework.Core.UnitTest.UnitTestBrowser.OpenChrome"

# xUnit example
dotnet test --filter "FullyQualifiedName~Testing.Infrastructure.UnitTest.UnitTest_CreditCardService.MaskedPAN"
```

## Architecture

### Page Object Model Implementation

All page objects should inherit from `WebPage` base class located in `Selenium.Framework.Core/WebPage.cs`. The framework uses SeleniumExtras PageFactory for element initialization.

**Key pattern:**
```csharp
public class YourPage : WebPage
{
    [FindsBy(How = How.Id, Using = "elementId")]
    private IWebElement element;

    public YourPage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

    public void PerformAction()
    {
        FillInputField(element, "value");  // Use base class helpers
        _log.Log("Action performed");       // Use inherited logger
        _assert.ExistText("Expected", "Fail message");  // Use inherited assertions
    }
}
```

**Important base class members:**
- `_driver`: WebDriver instance
- `_log`: ILogger instance for logging and screenshots
- `_assert`: IAssertion instance for validations
- Helper methods: `JSClick()`, `SubmitClick()`, `WaitForClick()`, `FillInputField()`, `SelectOptionByText()`, `SwitchToFrame()`, `RedirectTo()`

### Test Class Structure

The framework supports both MSTest and NUnit through base classes in `Selenium.Framework.Core/TestSet/`:
- `MSTestSetBase` - For MSTest tests (recommended for consistency with existing tests)
- `NUnitTestSetBase` - For NUnit tests

**Standard test pattern:**
```csharp
[TestClass]
public class YourTests : MSTestSetBase
{
    private Site site;
    private ILogger logger;

    [TestInitialize]
    public void Setup()
    {
        logger = new FileLogger("TestClassName", nameof(YourTests));
        Browser browser = new Browser("chrome");
        SiteEnvironment env = new SiteEnvironment("https://your-app-url.com");
        site = new Site(browser, env, logger);
    }

    [TestMethod]
    public void Test_Feature_Scenario()
    {
        // Test implementation
    }

    [TestCleanup]
    public void Teardown()
    {
        site.Close();
    }
}
```

### Browser Management

Browser instantiation uses factory pattern via `SeleniumWebDriverService` in `Selenium.Framework.Core/Services/SeleniumWebDriverService.cs`.

**Supported browsers:**
- `"chrome"` - Fully implemented with extension support
- `"firefox"` - Defined but incomplete implementation
- `"ie"` - Defined but incomplete implementation
- `"opera"` - Partially implemented

**Browser driver executables** are located in `Selenium.Framework.Core/Driver/`:
- chromedriver.exe
- geckodriver.exe
- IEDriverServer.exe
- operadriver.exe (referenced but not packaged)

**Creating browser instances:**
```csharp
Browser browser = new Browser("chrome");
IWebDriver driver = browser.Open();

// With Chrome extension:
ChromeExtension ext = new ChromeExtension("path/to/extension.crx");
Browser browser = new Browser("chrome", ext);
```

### Site Context and Navigation

The `Site` class (`Selenium.Framework.Core/Site.cs`) serves as the central context holder providing:
- Browser lifecycle management
- Environment configuration (via `SiteEnvironment`)
- Access to driver, logger, and assertion instances
- Navigation methods: `RedirectTo()`, `NavigateTo()`, `Close()`

### Testing.Infrastructure Services

These services are independent of Selenium and available for API/database/data testing:

**ApiService** (`Testing.Infrastructure/ApiService.cs`):
```csharp
ApiResponse<T> response = ApiService.Post<T>(url, jsonBody, authToken);
ApiResponse<T> response = ApiService.Get<T>(url, authToken);
ApiResponse<T> response = ApiService.Put<T>(url, jsonBody, authToken);
```

**DatabaseService** (`Testing.Infrastructure/DatabaseService.cs`):
```csharp
var db = new PostgreSQL(connectionString);
string result = db.Query("SELECT * FROM users WHERE id = 123");
```

**CreditCardService** (`Testing.Infrastructure/CreditCardService.cs`):
```csharp
var card = new CreditCard(CardType.Visa, validYears: 4);
// Access: card.PAN, card.ExpiryDate, card.MaskedPAN, card.CVV
```

**GeneratorService** (`Testing.Infrastructure/GeneratorService.cs`):
```csharp
string hash = GeneratorService.GetMD5Hash(length: 6, salt: "optional");
string refNum = GeneratorService.GetRefNumber("PREFIX");
```

**FileService** (`Testing.Infrastructure/FileService.cs`):
```csharp
List<T> data = FileService.LoadJson<T>("path/to/file.json");
FileService.SaveJson(dataList, "output.json");
```

### Logging and Screenshots

The framework provides two logger implementations via `ILogger` interface:
- `FileLogger` - Logs to `C:\Logs\TestResults\{timestamp}_{className}_{loggerName}/` with screenshots
- `ConsoleLogger` - Console-only output

**Usage:**
```csharp
logger.Log("Descriptive message about action");
logger.TakeSnapshot(driver, "SCREENSHOT_KEY");  // Screenshot saved with timestamp
```

Logs include `execute.log` file and `screenshot/` subdirectory organized by keys.

## Common Development Workflows

### Adding a New Page Object

1. Create class inheriting from `WebPage` in appropriate location
2. Define page elements using `[FindsBy]` attributes
3. Constructor must call base constructor: `base(driver, logger)`
4. Implement page-specific action methods using base class helpers
5. Use `_log`, `_assert`, and `_driver` inherited members

### Adding a New Test

1. Create test class inheriting from `MSTestSetBase` (or `NUnitTestSetBase`)
2. Initialize `Site` object in `[TestInitialize]` with browser and environment
3. Instantiate required page objects passing `site._driver` and `logger`
4. Implement test methods with `[TestMethod]` attribute
5. Clean up with `site.Close()` in `[TestCleanup]`

### Extending Browser Support

To add support for Firefox/IE/Opera browsers:
1. Implement browser-specific method in `SeleniumWebDriverService.cs`
2. Follow pattern of `OpenChrome()` method
3. Configure browser options (maximize window, arguments, etc.)
4. Ensure corresponding driver executable is in `Driver/` folder
5. Update `Browser.Open()` switch statement to route to new method

### Creating Custom Assertions

1. Implement `IAssertion` interface from `Selenium.Framework.Core/Assertion/IAssertion.cs`
2. Inject or instantiate in `WebPage` constructor
3. Common pattern: try assertion, log result, take screenshot, throw on failure
4. Methods to implement: `ExistElement()`, `ExistText()`, `NotExistText()`, `NotExistElement()`

## Important Implementation Notes

- **Element Interaction**: Prefer `WebPage` helper methods (`JSClick()`, `WaitForClick()`, `FillInputField()`) over raw Selenium calls for reliability and automatic logging
- **Waits**: Use built-in wait methods (`WaitFor()`, `WaitForClick()`, `WaitForText()`) instead of Thread.Sleep
- **Test Data**: Use `CreditCardService` for payment tests, `GeneratorService` for unique IDs, `FileService` for JSON test data
- **Assertions**: Always provide meaningful failure messages; screenshots are automatically captured
- **Frame Handling**: Use `SwitchToFrame()` helper method from `WebPage` base class
- **Select Elements**: Use `SelectOptionByText()` or `SelectOptionByIndex()` helpers
- **Test Framework**: MSTest is used for Framework.Core tests; xUnit for Infrastructure tests; both are supported for new test implementations

## Project Dependencies

Key NuGet packages:
- Selenium.WebDriver 3.141.0
- Selenium.Support 3.141.0
- DotNetSeleniumExtras.PageObjects.Core 3.12.0
- DotNetSeleniumExtras.WaitHelpers 3.11.0
- MSTest.TestFramework 2.0.0
- NUnit 3.12.0
- Npgsql 3.2.7 (PostgreSQL)
- Bogus 28.4.4 (fake data generation)
- Newtonsoft.Json 12.0.3
