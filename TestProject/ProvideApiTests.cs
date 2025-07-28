using MBAPI_TestApp.Controllers;

namespace TestProject
{
    public class ProvideApiTests
    {
        [Fact]
        public void CustomerQuery()
        {
            var controller = new BoostProviderApi();
            var query = new MeasureBoostApi.Interface.CustomerQuery() { searchString = "j" };

            var results = controller.CustomerSearch(query);
            Assert.NotNull(results);
            Assert.Equal(2, results.Count());
        }
    }
}