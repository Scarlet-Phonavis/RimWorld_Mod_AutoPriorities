using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using AutoPriorities.APLogger;
using AutoPriorities.Core;
using AutoPriorities.Percents;
using AutoPriorities.WorldInfoRetriever;
using AutoPriorities.Wrappers;

namespace AutoPriorities.PawnDataSerializer
{
    public class PawnsDataSerializer : IPawnsDataSerializer
    {
        private readonly string _fullPath;
        private readonly ILogger _logger;
        private readonly IWorldInfoFacade _worldInfo;

        public PawnsDataSerializer(ILogger logger, string fullPath, IWorldInfoFacade worldInfo)
        {
            _logger = logger;
            _fullPath = fullPath;
            _worldInfo = worldInfo;
        }

        #region IPawnsDataSerializer Members

        public SaveData LoadSavedData()
        {
            var (percents, excluded) = GetStateLoaders();

            return new SaveData
            {
                ExcludedPawns = excluded,
                WorkTablesData = percents
            };
        }

        public void SaveData(SaveDataRequest request)
        {
            SaveState((request.WorkTablesData, request.ExcludedPawns));
        }

        #endregion

        private void SaveState(
            (List<(Priority priority, JobCount maxJobs, Dictionary<IWorkTypeWrapper, IPercent> workTypes)>,
                HashSet<(IWorkTypeWrapper, IPawnWrapper)>) state)
        {
            using var stream = new FileStream(_fullPath, FileMode.Create);
            new XmlSerializer(typeof(PercentTableSaver.Ser)).Serialize(stream, PercentTableSaver.Ser.Serialized(state));
        }

        private (List<(Priority priority, JobCount? maxJobs, Dictionary<IWorkTypeWrapper, IPercent> workTypes)>
            percents,
            HashSet<(IWorkTypeWrapper, IPawnWrapper)> excluded)
            GetStateLoaders()
        {
            try
            {
                if (File.Exists(_fullPath))
                {
                    using var stream = new FileStream(_fullPath, FileMode.OpenOrCreate);
                    var ser =
                        (PercentTableSaver.Ser)new XmlSerializer(typeof(PercentTableSaver.Ser)).Deserialize(stream);
                    return (ser.ParsedData(_worldInfo), ser.ParsedExcluded(_worldInfo));
                }
            }
            catch (Exception e)
            {
                _logger.Err("Error while deserializing state");
                _logger.Err(e);
            }

            return (
                new List<(Priority, JobCount?, Dictionary<IWorkTypeWrapper, IPercent>)>(),
                new HashSet<(IWorkTypeWrapper, IPawnWrapper)>());
        }
    }
}
