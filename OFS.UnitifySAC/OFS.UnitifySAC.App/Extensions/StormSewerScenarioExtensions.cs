using Haestad.Domain;
using OpenFlows.StormSewer.Domain;
using OpenFlows.StormSewer.Domain.ModelingElements;
using System.Collections.Generic;
using static OFS.UnitifySAC.App.Extentions.AlternativeExtensions;

namespace OFS.UnitifySAC.App.Extentions
{
    public static class StormSewerScenarioExtensions
    {
        public static string ToStr(this IStormSewerScenario scenario)
        {
            return $"Scenario: {scenario.Id}: {scenario.Label}";
        }

        public static IScenario WoScenario(this IStormSewerScenario scenario, IStormSewerModel stormSewerModel) =>
            stormSewerModel.DomainDataSet.ScenarioManager.Element(scenario.Id) as IScenario;

        public static IList<int> GetActiveScenarioIdsPath(this IStormSewerScenario waterScenario, IStormSewerModel stormSewerModel)
        {
            var ids = new List<int>();
            GetChildrenScenarioIdChain(waterScenario, ids);
            
            return ids;
        }
        private static void GetChildrenScenarioIdChain(IStormSewerScenario waterScenario,  IList<int> ids)
        {
            ids.Add(waterScenario.Id);
            waterScenario
                .Manager
                .ChildrenOfElement(waterScenario.Id)
                .ForEach(s => GetChildrenScenarioIdChain(s,  ids));

        }

        public static Dictionary<StormSewerAlternativeTypeEnum, IStormSewerAlternative> ActiveAlternativeMap(this IStormSewerScenario ssScenario, IStormSewerModel stormSewerModel)
        {
            // cache active scenario
            var activeScenario = stormSewerModel.ActiveScenario;

            // make given scenario active
            stormSewerModel.SetActiveScenario(ssScenario);

            var retVal= stormSewerModel.AlternativeTypes().ActiveAlternatives;

            // restore active scenario to original
            stormSewerModel.SetActiveScenario(activeScenario); ;

            return retVal;
        }

        public static void AssignAlternatives(this IStormSewerScenario waterScenario, 
            Dictionary<StormSewerAlternativeTypeEnum, 
            IStormSewerAlternative> map, 
            IStormSewerModel stormSewerModel)
        {
            foreach (var kvp in map)
            {
                waterScenario.WoScenario(stormSewerModel).AlternativeID((int)kvp.Key, kvp.Value.Id);                
            }
        }

    }
}
