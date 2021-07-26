using RimWorld;

namespace AutoPriorities.Wrappers
{
    public interface IPawnWrapper
    {
        string ThingID { get; }

        string NameFullColored { get; }

        string LabelNoCount { get; }

        string LabelNoCountColored { get; }

        bool AnimalOrWildMan();

        bool IsCapableOfWholeWorkType(IWorkTypeWrapper work);

        double AverageOfRelevantSkillsFor(IWorkTypeWrapper work);

        Passion MaxPassionOfRelevantSkillsFor(IWorkTypeWrapper work);

        void workSettingsSetPriority(IWorkTypeWrapper work, int priorityV);
    }
}