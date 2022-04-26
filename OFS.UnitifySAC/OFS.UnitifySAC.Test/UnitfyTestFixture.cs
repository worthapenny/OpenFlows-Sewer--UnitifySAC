using Haestad.Framework.Application;
using NUnit.Framework;
using System.Linq;
using OFS.UnitifySAC.App.Domain;
using Haestad.Support.User;
using OFS.UnitifySAC.App.Extentions;

namespace OFS.UnitifySAC.Test
{
    [TestFixture]
    public class UnitfyTestFixture : OpenFlowsStormSewerTestFixtureBase
    {
        #region Constructor
        public UnitfyTestFixture():base()
        {
        }
        #endregion

        #region Setup/Teardown
        protected override void OneTimeSetupImpl()
        {
            var modelFilePath = "";
            modelFilePath = @"D:\Office\WaterSight\Users\Carollo\Manatee County\Model\ManateeSESA SewerGEMS Model 20220419\SE Model UPD 20220419 A.stsw";

            OpenModel(modelFilePath);
        }
        protected override void TeardownImpl()
        {            
        }
        #endregion

        #region Tests
        [Test]
        public void SimplifySACTest()
        {
            var unitify = new UnitifyScenariosAlternativesCalcOptions(SSModel);
            unitify.DeleteScenariosExceptActive();
            unitify.DeleteCalcOptionsExceptActive();
            unitify.DeleteAlternativesExceptActive();
            unitify.MergeAlternativesToTheRoot(new NullProgressIndicator());
            unitify.DeleteScenariosExceptBase();

            Assert.AreEqual(1, SSModel.Scenarios.Count);
            Assert.AreEqual(1, SSModel.CalculationOptions(SSModel.DomainDataSet.NumericalEngineTypeName(SSModel.ActiveScenario.Options.Id)));
            foreach (var alternativeTypeKVP in SSModel.AlternativeTypes().All)
                Assert.AreEqual(1, alternativeTypeKVP.Value.Count);



            SSModel.SaveAs(SSModel.ModelInfo.Filename.Replace(".stsw", "_UnitifiedSAC.stsw"));
        }
        #endregion
    }
}
