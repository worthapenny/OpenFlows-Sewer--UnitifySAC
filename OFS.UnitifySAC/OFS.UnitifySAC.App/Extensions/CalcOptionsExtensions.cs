using Haestad.Domain;
using Haestad.Support.Support;
using OpenFlows.Domain.ModelingElements;
using OpenFlows.StormSewer.Domain;
using System.Collections.Generic;
using System.Diagnostics;

namespace OFS.UnitifySAC.App.Extentions
{
    public static class CalcOptionsExtensions
    {
        public static List<IStormSewerCalculationOptions> CalculationOptions(this IStormSewerModel ssModel, string ssSolverType)
        {
            var options = new List<IStormSewerCalculationOptions>();

            var engineTypes = ssModel.DomainDataSet.DomainDataSetType().NumericalEngineTypes();
            
            foreach (var engineType in engineTypes)
            {
                if (!(engineType.Name == ssSolverType))
                    continue;

                foreach (var option in ssModel.DomainDataSet.ScenarioManager.CalculationOptionsManager(engineType.Name).Elements())
                    options.Add(new StormSewerCalculationOptions(option as ICalculationOptions));
            }

            return options;
        }

        public struct StormSewerEngineType
        {
            public const string CivilStormDynamicEngine = "CivilStormDynamicEngine";
            public const string PondPackNumericalEngine = "PondPackNumericalEngine";
            public const string StormDerivedEngine = "StormDerivedEngine";
            public const string GVFPressureEngine = "GVFPressureEngine";
            public const string GravityDerivedEngine = "GravityDerivedEngine";
            public const string GVFEngine = "GVFEngine";
            public const string SwmmEngine = "SwmmEngine";
            public const string EnergyCostEngine = "EnergyCostEngine";
            public const string UrbanFloodEngine = "UrbanFloodEngine";
            public const string DelawareProductEngi = "DelawareProductEngi";
        }

        public interface IStormSewerCalculationOptions : IElement
        {
            #region Public Methods
            FieldCollection SupportedFields();
            IField Field(string name);
            void Delete();
            #endregion

            #region Public Properties
            string TypeName { get; }
            #endregion

        }

        [DebuggerDisplay("{ToString()}")]
        private class StormSewerCalculationOptions : IStormSewerCalculationOptions
        {
            #region Constructor
            public StormSewerCalculationOptions(ICalculationOptions options)
            {
                WoCalcOption = options;
            }
            #endregion

            #region Public Methods
            public FieldCollection SupportedFields() => WoCalcOption.SupportedFields();
            public IField Field(string name) => WoCalcOption.CalculationOptionsField(name);
            public void Delete()
            {
                WoCalcOption.Manager.Delete(Id);
            }
            #endregion

            #region Public Overriden Methods
            public override string ToString()
            {
                return $"Calc. Option: {Id}: {Label}";
            }
            #endregion

            #region Public Properties
            public int Id => WoCalcOption.Id;
            public string Notes { get => WoCalcOption.Notes; set => WoCalcOption.Notes = value; }
            public ModelElementType ModelElementType { get => ModelElementType.Options; }
            public string Label { get => WoCalcOption.Label; set => WoCalcOption.Label = value; }
            public string TypeName { get => WoCalcOption.NumericalEngineTypeName; }
            #endregion

            #region Private Properties
            private ICalculationOptions WoCalcOption { get; set; }
            #endregion

        }
    }
}
