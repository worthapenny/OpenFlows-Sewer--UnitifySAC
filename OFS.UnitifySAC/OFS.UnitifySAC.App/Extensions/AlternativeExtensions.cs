/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-10-22 19:18:10
 * @ Modified by: Akshaya Niraula
 * @ Modified time: 2021-10-26 17:33:01
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula See LICENSE for details
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Haestad.Domain;
using Haestad.Support.Support;
using Haestad.Support.User;
using OpenFlows.Domain.ModelingElements;
using OpenFlows.StormSewer.Domain;
using OpenFlows.StormSewer.Domain.ModelingElements.NetworkElements;

namespace OFS.UnitifySAC.App.Extentions
{
    public static class AlternativeExtensions
    {
        #region Alternative Types


        public static IStormSewerAlternativeTypes AlternativeTypes(this IStormSewerModel ssModel)
        {
            return new StormSewerAlternativeTypeManager(ssModel);
        }
        public interface IStormSewerAlternativeTypes
        {
            IStormSewerAlternative ActiveAlternative(StormSewerAlternativeTypeEnum alternativeType);
            IStormSewerAlternative Create(string label, StormSewerAlternativeTypeEnum alternativeTypeEnum, int? parentID);


            Dictionary<StormSewerAlternativeTypeEnum, List<IStormSewerAlternative>> All { get; }
            Dictionary<StormSewerAlternativeTypeEnum, List<IStormSewerAlternative>> BaseAlternativesMap { get; }
            Dictionary<StormSewerAlternativeTypeEnum, IStormSewerAlternative> ActiveAlternatives { get; }

        }


        private class StormSewerAlternativeTypeManager : IStormSewerAlternativeTypes
        {
            #region Constructor
            public StormSewerAlternativeTypeManager(IStormSewerModel ssModel)
            {
                SSModel = ssModel;
            }

            #endregion

            #region Public Methods
            public IStormSewerAlternative Create(string label, StormSewerAlternativeTypeEnum alternativeTypeEnum, int? parentID)
            {
                var manager = SSModel.DomainDataSet.AlternativeManager((int)alternativeTypeEnum);
                var id = manager.Add();
                var alternative = manager.Element(id) as IAlternative;
                alternative.Label = label;

                if (parentID != null)
                    alternative.ParentID = parentID.Value;

                return new StormSewerAlternative(alternative, SSModel);
            }

            public IStormSewerAlternative ActiveAlternative(StormSewerAlternativeTypeEnum alternativeType)
            {
                IScenario scenario = SSModel
                    .DomainDataSet
                    .ScenarioManager
                    .Element(SSModel.ActiveScenario.Id) as IScenario;

                return new StormSewerAlternative(
                    SSModel
                    .DomainDataSet
                    .AlternativeManager((int)alternativeType)
                    .Element(scenario.AlternativeID((int)alternativeType)) as IAlternative,
                    SSModel
                    );
            }
            #endregion

            #region Public Properties
            public Dictionary<StormSewerAlternativeTypeEnum, List<IStormSewerAlternative>> BaseAlternativesMap
            {
                get
                {
                    var map = new Dictionary<StormSewerAlternativeTypeEnum, List<IStormSewerAlternative>>();
                    foreach (var item in Enum.GetValues(typeof(StormSewerAlternativeTypeEnum)).Cast<StormSewerAlternativeTypeEnum>())
                    {
                        var baseElements = SSModel
                            .DomainDataSet
                            .AlternativeManager((int)item)
                            .BaseElements()
                            .OfType<IAlternative>()
                            .Select(a => (new StormSewerAlternative(a, SSModel) as IStormSewerAlternative))
                            .ToList();

                        map.Add(item, baseElements);
                    }

                    return map;
                }
            }
            public Dictionary<StormSewerAlternativeTypeEnum, List<IStormSewerAlternative>> All
            {
                get
                {
                    var map = new Dictionary<StormSewerAlternativeTypeEnum, List<IStormSewerAlternative>>();
                    SSModel.Alternatives().ForEach(a =>
                    {
                        List<IStormSewerAlternative> alternatives = null;
                        if (!(map.TryGetValue(a.AlternativeType, out alternatives)))
                            map[a.AlternativeType] = new List<IStormSewerAlternative>();

                        map[a.AlternativeType].Add(a);
                    });

                    return map;
                }
            }
            public Dictionary<StormSewerAlternativeTypeEnum, IStormSewerAlternative> ActiveAlternatives
            {
                get
                {
                    var alternativeTypes = Enum.GetValues(typeof(StormSewerAlternativeTypeEnum));
                    var activeAlternatives = new Dictionary<StormSewerAlternativeTypeEnum, IStormSewerAlternative>();

                    foreach (var alternativeType in alternativeTypes)
                    {
                        var alternativeTypeEnum = (StormSewerAlternativeTypeEnum)alternativeType;
                        activeAlternatives.Add(alternativeTypeEnum, ActiveAlternative(alternativeTypeEnum));
                    }

                    return activeAlternatives;
                }
            }
            #endregion

            #region IElement Properties
            private IStormSewerModel SSModel { get; }
            #endregion
        }



        #region Enums
        public enum StormSewerAlternativeTypeEnum
        {
            HmiDataSetGeometryAlternative = 1,
            HMIDataSetTopologyAlternative = 2,
            HMIActiveTopologyAlternative = 3,
            HMIUserDefinedExtensionsAlternative = 100,
            PhysicalAlternative = 4,
            BoundaryConditionAlternative = 5,
            InitialConditionAlternative = 6,
            HydrologicAlternative = 7,
            OutputAlternative = 8,
            DryLoadAlternative = 9,
            RainfallRunoffAlternative = 10,
            WaterQualityAlternative = 11,
            SanitaryLoadingAlternative = 12,
            InfiltrationAndInflowAlternative = 13,
            ScadaAlternative = 14,

            HeadlossAlternative = 40,
            DesignAlternative = 41,
            SystemFlowsAlternative = 45,
        }
        #endregion

        #endregion

        #region Alternatives
        public static List<IStormSewerAlternative> Alternatives(this IStormSewerModel ssModel)
        {
            var alternatives = new List<IStormSewerAlternative>();
            
            foreach (var alternativeType in ssModel.DomainDataSet.DomainDataSetType().AlternativeTypes())
            {
                foreach (IAlternative alternative in ssModel.DomainDataSet.AlternativeManager(alternativeType.Id).Elements())
                    alternatives.Add(new StormSewerAlternative(alternative, ssModel));
            }

            return alternatives;
        }


        
        public interface IStormSewerAlternative : IElement
        {
            List<IStormSewerAlternative> Children();
            FieldCollection SupportedField();
            IField AlternativeField(string name, StormSewerNetworkElementType elementType);
            IAlternativeType WoAlternativeType { get; }
            StormSewerAlternativeTypeEnum AlternativeType { get; }
            IList<int> GetAlternativePathIds(IStormSewerModel ssModel, IStormSewerAlternative alternative);
            int ParentId { get; set; }
            IAlternative WoAlternative { get; }
            IAlternativeManager WoAlternativeManager { get; }

            IStormSewerAlternative ParentAlternative { get; }


            bool IsActive();
            void Delete();
            void MergeAllParents(IProgressIndicator pi);
            void AssignToActiveScenario(StormSewerAlternativeTypeEnum alternativeType, int alternativeId);
            //void AssignToNoScenario(StormSewerAlternativeTypeEnum alternativeType, IStormSewerAlternative alternative);
        }

        [DebuggerDisplay("{ToString()}")]
        private class StormSewerAlternative : IStormSewerAlternative, IElement
        {
            #region Constructor
            public StormSewerAlternative(IAlternative alternative, IStormSewerModel ssModel)
            {
                WoAlternative = alternative;
                SSModel = ssModel;
            }
            public StormSewerAlternative(int alternativeId, int alternativeTypeId, IStormSewerModel ssModel)
            {
                WoAlternative = (IAlternative)ssModel.DomainDataSet.AlternativeManager(alternativeTypeId).Element(alternativeId);
                SSModel = ssModel;
            }
            #endregion

            #region Public Methods
            public List<IStormSewerAlternative> Children()
            {
                var alternatives = new List<IStormSewerAlternative>();
                foreach (var alternative in WoAlternative.Children())
                    alternatives.Add(new StormSewerAlternative(alternative as IAlternative, SSModel));

                return alternatives;
            }
            public void Delete() => WoAlternative.Manager.Delete(WoAlternative.Id);
            public FieldCollection SupportedField() => WoAlternative.SupportedFields();
            public IField AlternativeField(string name, StormSewerNetworkElementType elementType)
                => WoAlternative.AlternativeField(name, (int)elementType);
            public bool IsActive()
            {
                var activeAlternativeId = SSModel
                    .ActiveScenario
                    .WoScenario(SSModel)
                    .AlternativeID((int)AlternativeType);
                return activeAlternativeId == Id;
            }
            public IList<int> GetAlternativePathIds(IStormSewerModel ssModel, IStormSewerAlternative alternative)
            {
                var ids = new List<int>();
                GetParentAlternativeIdChain(alternative.WoAlternative, ids);
                return ids;
            }
            public void MergeAllParents(IProgressIndicator pi)
            {
                var parentAlternativeId = WoAlternative.ParentID;

                while (parentAlternativeId > 0)
                {
                    // if current alternative is active, make the parent alternative active
                    // as you can not start the merge from the active alternative
                    if (IsActive())
                        AssignToActiveScenario(AlternativeType, ParentId);

                    pi.AddTask($"Merging alternatives, {Label} and parent ({ParentAlternative.Label}) together...");
                    pi.IncrementTask();
                    pi.BeginTask(1);

                    (WoAlternative.Manager as IAlternativeManager).Merge(Id);
                    WoAlternative = SSModel.DomainDataSet.AlternativeManager(WoAlternativeType.Id).Element(parentAlternativeId) as IAlternative;
                    parentAlternativeId = WoAlternative.ParentID;

                    pi.EndTask();
                }
            }
            public void AssignToActiveScenario(StormSewerAlternativeTypeEnum alternativeType, int alternativeId)
            {
                SSModel
                    .ActiveScenario
                    .WoScenario(SSModel)
                    .AlternativeID((int)alternativeType, alternativeId);

            }
            #endregion

            #region Public Overridden Methods
            public override string ToString()
            {
                return $"Alternative: {Id}: {Label} [{AlternativeType}]";
            }
            #endregion

            #region Private Properties
            private void GetParentAlternativeIdChain(IAlternative alternative, IList<int> ids)
            {
                ids.Add(alternative.Id);
                if (alternative.ParentID > 0)
                    GetParentAlternativeIdChain(
                        alternative.Manager.Element(alternative.ParentID) as IAlternative,
                        ids);

            }


            private IStormSewerModel SSModel { get; }
            #endregion



            #region Public Properties
            public int Id => WoAlternative.Id;
            public string Label { get => WoAlternative.Label; set => WoAlternative.Label = value; }
            public string Notes { get => WoAlternative.Notes; set => WoAlternative.Notes = value; }
            public int ParentId { get => WoAlternative.ParentID; set => WoAlternative.ParentID = value; }
            public StormSewerAlternativeTypeEnum AlternativeType => (StormSewerAlternativeTypeEnum)WoAlternative.AlternativeTypeID;
            public ModelElementType ModelElementType => throw new NotImplementedException(); 
            public IAlternativeType WoAlternativeType => WoAlternative.AlternativeType();
            public IAlternative WoAlternative { get; private set; }
            public IAlternativeManager WoAlternativeManager => (IAlternativeManager)WoAlternative.Manager;  //SSModel.DomainDataSet.AlternativeManager(WoAlternative.AlternativeTypeID);
            public IStormSewerAlternative ParentAlternative => new StormSewerAlternative(ParentId, WoAlternative.AlternativeTypeID, SSModel);
            #endregion

        }
        #endregion

    }
}
