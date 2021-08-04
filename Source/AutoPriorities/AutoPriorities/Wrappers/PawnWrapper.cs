using RimWorld;
using Verse;

namespace AutoPriorities.Wrappers
{
    internal record PawnWrapper : IPawnWrapper
    {
        private readonly Pawn _pawn;

        public PawnWrapper(Pawn pawn)
        {
            _pawn = pawn;
        }

        #region IPawnWrapper Members

        public string ThingID => _pawn.ThingID;

        public string NameFullColored => _pawn.NameFullColored;

        public string LabelNoCount => _pawn.LabelNoCount;

        public bool IsCapableOfWholeWorkType(IWorkTypeWrapper work)
        {
            return !_pawn.WorkTypeIsDisabled(((WorkTypeWrapper)work).workTypeDef);
        }

        public double AverageOfRelevantSkillsFor(IWorkTypeWrapper work)
        {
            // TODO: terrible hack, fix this
            return _pawn.skills.AverageOfRelevantSkillsFor(((WorkTypeWrapper)work).workTypeDef);
        }

        public Passion MaxPassionOfRelevantSkillsFor(IWorkTypeWrapper work)
        {
            // TODO: terrible hack, fix this
            return _pawn.skills.MaxPassionOfRelevantSkillsFor(((WorkTypeWrapper)work).workTypeDef);
        }

        public void WorkSettingsSetPriority(IWorkTypeWrapper work, int priorityV)
        {
            // TODO: terrible hack, fix this
            _pawn.workSettings.SetPriority(((WorkTypeWrapper)work).workTypeDef, priorityV);
        }

        #endregion
    }
}
