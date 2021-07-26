using AutoPriorities.APLogger;
using AutoPriorities.PawnDataSerializer;
using AutoPriorities.WorldInfoRetriever;

namespace AutoPriorities
{
    public class PawnsDataBuilder
    {
        private readonly IPawnsDataSerializer _serializer;
        private readonly IWorldInfoRetriever _worldInfoRetriever;
        private readonly ILogger _logger;

        public PawnsDataBuilder(IPawnsDataSerializer serializer, IWorldInfoRetriever worldInfoRetriever, ILogger logger)
        {
            _serializer = serializer;
            _worldInfoRetriever = worldInfoRetriever;
            _logger = logger;
        }

        public PawnsData Build()
        {
            var data = new PawnsData(_serializer, _worldInfoRetriever, _logger);
            var save = _serializer.LoadSavedData();
            data.SetData(save);
            return data;
        }
    }
}
