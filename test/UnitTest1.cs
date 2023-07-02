namespace TestProject
{
    [Collection(PlaywrightFixture.PlaywrightCollection)]
    public class UnitTest1
    {
        private readonly PlaywrightFixture fixture;

        public UnitTest1(PlaywrightFixture playwrightFixture)
        {
            this.fixture = playwrightFixture;
        }

        [Fact]
        public async Task Test1()
        {
            var url = "https://localhost:5001/";
            await this.fixture.GotoPageAsync(url,
              async (page) =>
              {
                  var text = await page.Locator("h1").TextContentAsync();
                  Assert.True(text == "Login");
              });
        }
    }
}