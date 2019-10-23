using Xunit;

using Blue10SDK;
using Blue10SDKTests.Stubs;

namespace Blue10SDKTests
{
    public class ProjectTests
    {
        #region Fields

        private IWebApiAdapter mDummyWebApi = new WebApiAdapterStub();
        private Blue10Desk mApiClient;

        #endregion

        #region Constructors

        public ProjectTests()
        {
            mApiClient = new Blue10Desk(mDummyWebApi);
        }

        #endregion

        [Theory]
        [InlineData("TestProject")]
        [InlineData("TestProjéct")]
        public void AddProject_WhenProjectnameValid_AddsProject(string ProjectName)
        {
            //Create a test project
            var testProject = new Project { Name = ProjectName };
            
            //Post the poject using the Desk
            mApiClient.AddProject(testProject);

            //Get the stash from the dummy
            var stash = (mDummyWebApi as WebApiAdapterStub).Stash;
            
            Assert.Equal(testProject, stash["projects"]);
        }
    }
}