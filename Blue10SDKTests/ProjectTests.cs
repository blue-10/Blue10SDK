using System;
using Blue10SDK;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private IWebApiAdapter mDummyWebApi =  new DummyWebApiClient();
        private Blue10WebApiClient apiClient;
        
        [SetUp]
        public void Setup()
        {
            apiClient  = new Blue10WebApiClient(mDummyWebApi);
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
        
        
        [TestCase(null)]
        [TestCase("-1")]
        public void ProjectThrows(string ProjectName)
        {
            //Todo Somekind of exception
            Assert.That(() => ProjectTestHappy(ProjectName), Throws.TypeOf<ArgumentException>());
        }
    }
}