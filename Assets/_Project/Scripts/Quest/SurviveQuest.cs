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
            Debug.Log($"Quest: {questName} started.");
        }
        public override void CompleteQuest ()
        {
            status = QuestStatus.Completed;
            Debug.Log($"Quest: {questName} completed.");
        }
        public override void FailQuest ()
        {
            status = QuestStatus.Failed;
            Debug.Log($"Quest: {questName} failed.");
        }
        public override void UpdateQuestProgress (int progress)
        {

        }
        public override void ResetQuest ()
        {
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
