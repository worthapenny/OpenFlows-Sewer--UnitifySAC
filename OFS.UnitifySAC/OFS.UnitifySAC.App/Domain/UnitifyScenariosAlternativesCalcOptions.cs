using Haestad.Support.User;
using OFS.UnitifySAC.App.Extentions;
using OpenFlows.StormSewer.Domain;
using OpenFlows.StormSewer.Domain.ModelingElements;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using static OFS.UnitifySAC.App.Extentions.AlternativeExtensions;
using static OFS.UnitifySAC.App.Extentions.CalcOptionsExtensions;

namespace OFS.UnitifySAC.App.Domain
{
    public class UnitifyScenariosAlternativesCalcOptions
    {
        #region Constructor
        public UnitifyScenariosAlternativesCalcOptions(IStormSewerModel stormSewerModel)
        {
            SSModel = stormSewerModel;
        }
        #endregion

        #region Public Methods
        public bool Unitify(IProgressIndicator pi)
        {
            bool success = true;

            pi.AddTask("Delete Scenario that are not active...");
            pi.AddTask("Delete Alternative that are not active...");
            pi.AddTask("Delete Calc. Options that are not active...");
            pi.AddTask("Merge Alternatives to the root...");
            pi.AddTask("Clean up scenario tree...");


            //
            // Delete Scenarios
            pi.IncrementTask(); // Select Task
            pi.BeginTask(1); // Show Count
            DeleteScenariosExceptActive();
            pi.IncrementStep(); 
            pi.EndTask(); // Done check mark

            // Delete Alternatives
            pi.IncrementTask(); // Select Task
            pi.BeginTask(1); // Show Count
            DeleteAlternativesExceptActive();
            pi.IncrementStep();
            pi.EndTask(); // Done check mark

            // Delete Cacl. Options
            pi.IncrementTask(); // Select Task
            pi.BeginTask(1); // Show Count
            DeleteCalcOptionsExceptActive();
            pi.IncrementStep();
            pi.EndTask(); // Done check mark

            // Clean up scenario tree
            pi.IncrementTask();
            pi.BeginTask(1);
            DeleteScenariosExceptBase();
            pi.IncrementStep();
            pi.EndTask();

            // Merge Alternatives
            MergeAlternativesToTheRoot(pi);


            return success;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Merge All Alternatives to the root Alternative
        /// </summary>
        public void MergeAlternativesToTheRoot(IProgressIndicator pi)
        {
            var allAlternatviesMap = SSModel.AlternativeTypes().All;
            var baseAlternativesMap = SSModel.AlternativeTypes().BaseAlternativesMap;


            foreach (var item in allAlternatviesMap)
            {
                var alternativeType = item.Key;
                var alternatives = item.Value;
                List<IStormSewerAlternative> baseAlternatives; 
                baseAlternativesMap.TryGetValue(alternativeType, out baseAlternatives);

                if (baseAlternatives == null)
                {
                    continue;
                }

                if (alternatives.Count > 1)
                {
                    Log.Debug($"Working with {alternatives.Count} alternatives of {alternativeType} alternative type");

                    // assuming the larger id are older child
                    // which is not always true
                    var orderedAlternatives = alternatives.OrderBy(a => a.Id).Reverse().ToList();

                    // set the base alternative to the active scenario
                    // and remove the base alternative from collection so that rest can be merged
                    foreach (var baseAlternative in baseAlternatives)
                    {
                        baseAlternative.AssignToActiveScenario(alternativeType, baseAlternative.Id);
                        orderedAlternatives = orderedAlternatives.Where(a => a.Id != baseAlternative.Id).ToList();

                        // Now merge alternative to the parent alternative
                        foreach (var alternative in orderedAlternatives)
                        {
                            Log.Debug($"About to merge alternative {alternative}");
                            alternative.MergeAllParents(pi);
                            Log.Debug($"    Merged alternative {alternative}"); 
                        }
                    }
                    Log.Debug(new string('.', 30));
                }
            }

            Log.Debug(new string('.', 100));
        }

        /// <summary>
        /// Delete scenario except for the active one.
        /// </summary>
        public void DeleteScenariosExceptActive()
        {
            Log.Debug("About to delete non-active scenarios...");

            var activeScenarioId = SSModel.Scenarios.ActiveScenario.Id;
            var deleteScenarios = new List<IStormSewerScenario>();
            var originalScenariosCount = SSModel.Scenarios.Count;

            SSModel.Scenarios.Elements().ForEach(s =>
            {
                if (!(s.GetActiveScenarioIdsPath(SSModel).Contains(activeScenarioId)))
                    deleteScenarios.Add(s);
            });

            deleteScenarios.ForEach(s =>
            {
                Log.Debug($"Deleted: {s.ToStr()}, as it is not active or part of active tree");
                s.Delete();
            });
            var remainingScenariosCount = originalScenariosCount - deleteScenarios.Count;
            Log.Debug($"Scenarios deleted, except the active one: Before count: {originalScenariosCount}, Deleted count: {deleteScenarios.Count}, Remaining count: {remainingScenariosCount}");

            Log.Debug(new string('.', 100));
        }

        public void DeleteScenariosExceptBase()
        {
            Log.Debug("About to delete non-base scenarios...");

            var activeScenarioId = SSModel.ActiveScenario.Id;
            var activeScenarioLabel = SSModel.ActiveScenario.Label;
            var baseScenarios = SSModel.Scenarios.BaseElements();
            var allScenarios = SSModel.Scenarios.Elements();
            var originalScenariosCount = allScenarios.Count;

            var deleteScenarios = new List<IStormSewerScenario>();
            foreach (var baseScenario in baseScenarios)
            {
                // Make the base scenario active so that others can be deleted
                baseScenario.MakeCurrent();

                foreach (var scenario in allScenarios)
                {
                    if (scenario.Id == activeScenarioId)
                        baseScenario.Label = activeScenarioLabel;

                    // don't delete the base scenario
                    if (scenario.Id != baseScenario.Id)
                        deleteScenarios.Add(scenario);
                }
            }

            deleteScenarios.ForEach(s => {
                Log.Debug($"Deleted: {s.ToStr()} as it is not base");
                s.Delete();
            });

            var remainingScenariosCount = originalScenariosCount - deleteScenarios.Count;
            Log.Debug($"Scenarios deleted, except the base scenarios: Before count: {originalScenariosCount}, Deleted count: {deleteScenarios.Count}, Remaining count: {remainingScenariosCount}");

            Log.Debug(new string('.', 100));
        }

        /// <summary>
        /// Delete Alternatives except for the active one.
        /// Maintain the tree structure
        /// </summary>
        public void DeleteAlternativesExceptActive()
        {
            Log.Debug("About to find non-active alternatives...");

            var deleteAlternatives = new List<IStormSewerAlternative>();

            var alternativeTypesToAlternativeMap = SSModel.AlternativeTypes().All;
            foreach (var item in alternativeTypesToAlternativeMap)
            {
                var alternativeType = item.Key;
                var alternatives = item.Value;

                Log.Debug($"Working with {alternatives.Count} alternatives under {Enum.GetName(typeof(StormSewerAlternativeTypeEnum), alternativeType)} alternative type");

                // get the active alternative for the given alternative type
                var activeAlternative = SSModel.AlternativeTypes().ActiveAlternative(alternativeType);
                Log.Debug($"Active alternative is: {activeAlternative}");

                // get the path (based on id) to the active alternative
                var ids = activeAlternative.GetAlternativePathIds(SSModel, activeAlternative);
                var idsPath = string.Join("-", ids);
                Log.Debug($"Path: {idsPath}");

                // any alternative that doesn't belong to the 'ids' are delete candidates
                alternatives.ForEach(a =>
                {
                    if (!(ids.Contains(a.Id)))
                        deleteAlternatives.Add(a);
                });

                Log.Debug(new string ('.', 30));
            }

            Log.Debug(new string('.', 100));
            Log.Debug("About to delete non-active alternatives...");
            deleteAlternatives.ForEach(a =>
            {
                Log.Debug($"Deleted: {a}, as it is not part of the active alternative tree");
                a.Delete();
            });

            Log.Debug(new string('.', 100));
        }

        /// <summary>
        /// Delete all the Calc Options except for the active one
        /// </summary>
        public void DeleteCalcOptionsExceptActive()
        {
            Log.Debug("About to delete non-active calc. options...");

            var activeCalcOptionId = SSModel.ActiveScenario.Options.Id;
            var deleteCalcOptions = new List<IStormSewerCalculationOptions>();
            var enegineName = SSModel.DomainDataSet.NumericalEngineTypeName(SSModel.ActiveScenario.Options.Id);
            SSModel.CalculationOptions(enegineName).ForEach(c =>
            {
                if (c.Id != activeCalcOptionId)
                    deleteCalcOptions.Add(c);
            });

            deleteCalcOptions.ForEach(c => {
                Log.Debug($"Deleted: {c} as it's not active");
                c.Delete();
            });

            var solverType = SSModel.DomainDataSet.NumericalEngineTypeName(SSModel.ActiveScenario.Options.Id);
            var remainingCalcOptionsCount = SSModel.CalculationOptions(solverType).Count - deleteCalcOptions.Count;
            Log.Debug($"Calc. Options deleted, except the base scenarios: Before count: {SSModel.CalculationOptions(solverType).Count}, Deleted count: {deleteCalcOptions.Count}, Remaining count: {remainingCalcOptionsCount}");

            Log.Debug(new string('.', 100));
        }

        #endregion


        #region Private Properties
        private IStormSewerModel SSModel { get; }
        #endregion
    }
}
