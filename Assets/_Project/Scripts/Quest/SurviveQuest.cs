using UnityEngine;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "Survive Quest", menuName = "Scriptable Objects/Quests/Survive Quest")]
    public class SurviveQuest : QuestGeneric
    {
        [SerializeField] private int surviveTime; // Time in minutes to survive
        [SerializeField] private int timeSurvived; // Time in mihutes survived so far
        public override void StartQuest ()
        {
            status = QuestStatus.InProgress;
            visible = true;
            NotifyQuestStatusUpdate(); // Notify quest status update
            Debug.Log($"Quest: {questName} started.");
        }
        public override void CompleteQuest ()
        {
            status = QuestStatus.Completed;
            NotifyQuestStatusUpdate(); // Notify quest status update
            Debug.Log($"Quest: {questName} completed.");
        }
        public override void FailQuest ()
        {
            status = QuestStatus.Failed;
            NotifyQuestStatusUpdate(); // Notify quest status update
            Debug.Log($"Quest: {questName} failed.");
        }
        public override void UpdateQuestProgress (int progress)
        {
            timeSurvived += progress;
            if(timeSurvived >= surviveTime)
            {
                CompleteQuest();
            }
        }
        public override void ResetQuest ()
        {
            timeSurvived = 0;
            status = QuestStatus.NotStarted;
            visible = true;
            NotifyQuestStatusUpdate(); // Notify quest status update
            Debug.Log($"Quest: {questName} reset.");
        }
        public override void LoadData (string jsonData)
        {
            // Implement JSON loading logic here
        }

        public override string SaveData()
        {
            // Implement JSON saving logic here
            return null;
        }
    }
}
