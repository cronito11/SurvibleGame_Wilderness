using UnityEngine;

namespace Surviblewilderness
{
    public enum QuestStatus { NotStarted, InProgress, Completed, Failed }
    public enum QuestType { MainQuest, SideQuest, DailyQuest }

    public abstract class QuestGeneric : ScriptableObject
    {
        
        [SerializeField] protected string questId;
        [SerializeField] protected string questName;
        [SerializeField] protected string questDescription;
        [SerializeField] protected bool visible;
        [SerializeField] protected QuestStatus status;
        [SerializeField] protected QuestType type;

        public string QuestId => questId;
        public string QuestName => questName;
        public string QuestDescription => questDescription;
        public bool Visible => visible;
        public QuestStatus Status => status;
        public QuestType Type => type;

        public abstract void StartQuest ();
        public abstract void CompleteQuest ();
        public abstract void FailQuest ();
        public abstract void UpdateQuestProgress (string data);
        public abstract void ResetQuest ();
        public abstract void LoadData (string jsonData);

    }
}
