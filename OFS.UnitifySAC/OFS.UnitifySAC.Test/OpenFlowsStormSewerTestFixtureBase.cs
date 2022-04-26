using Haestad.LicensingFacade;
using NUnit.Framework;
using OpenFlows.StormSewer;
using OpenFlows.StormSewer.Domain;
using System.IO;

namespace OFS.UnitifySAC.Test
{
    public abstract class OpenFlowsStormSewerTestFixtureBase
    {
        #region Constructor
        public OpenFlowsStormSewerTestFixtureBase()
        {

        }
        #endregion

        #region Setup/Tear-down
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Assert.AreEqual(LicenseRunStatusEnum.OK, OpenFlowsStormSewer.StartSession(StormSewerProductLicenseType.SewerGEMS));
            Assert.AreEqual(true, OpenFlowsStormSewer.IsValid());

            OneTimeSetupImpl();
        }

        protected virtual void OneTimeSetupImpl()
        {
        }
        [TearDown]
        public void Teardown()
        {
            SSModel?.Dispose(); // Close the model
            SSModel = null;

            TeardownImpl();

            OpenFlowsStormSewer.EndSession();
        }
        protected virtual void TeardownImpl()
        {

        }
        #endregion

        #region Protected Methods
        protected void OpenModel(string filename)
        {
            FileAssert.Exists(filename);
            SSModel = OpenFlowsStormSewer.Open(filename);
        }
        protected virtual string BuildTestFilename(string filename)
        {
            // The defualt base path is the samples folder for WaterGEMS. You can change this to
            // whatever you want.  Remember this is the BASE path as it will be comined with the provided filename.
            return Path.Combine(@"C:\Program Files (x86)\Bentley\WaterGEMS\Samples", filename);
        }
        #endregion

        #region Protected Properties
        protected IStormSewerModel SSModel { get; set; }
        #endregion
    }
}
