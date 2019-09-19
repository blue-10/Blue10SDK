using System;
using Blue10SDK;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private IWebApiAdapter mDummyWebApi =  new DummyWebApiClient();
        private Blue10Desk apiClient;
        
        [SetUp]
        public void Setup()
        {
            apiClient  = new Blue10Desk(mDummyWebApi);
        }

        [TestCase("TestProject")]
        [TestCase("TestProjéct")]
        public void ProjectTestHappy(string ProjectName)
        {
            //Create a test project
            var testProject = new Project {name = ProjectName};
            
            //Post the poject using the Desk
            apiClient.AddProject(testProject);

            //Get the stash from the dummy
            var stash = (mDummyWebApi as DummyWebApiClient).Stash;
            
            Assert.IsTrue(stash["projects"] == testProject);
        }
    }
}