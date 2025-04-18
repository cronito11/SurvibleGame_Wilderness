using System;
using UnityEngine;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "Kill Quest", menuName = "Scriptable Objects/Quests/Kill Quest")]
    public class KillQuest : QuestGeneric
    {
        [SerializeField] private EntityType targetEntity;
        [SerializeField] private int targetCount;
        [SerializeField] private int currentCount;

        public EntityType TargetEntity => targetEntity;

        public override void StartQuest()
        {
            status = QuestStatus.InProgress;
            NotifyQuestStatusUpdate(); // Notify quest status update
            Debug.Log($"Quest: {questName} started.");
        }

        public override void CompleteQuest()
        {
            status = QuestStatus.Completed;
            NotifyQuestStatusUpdate(); // Notify quest status update
            Debug.Log($"Quest: {questName} completed.");
        }

        public override void FailQuest()
        {
            status = QuestStatus.Failed;
            NotifyQuestStatusUpdate(); // Notify quest status update
            Debug.Log($"Quest: {questName} failed.");
        }

        public override void UpdateQuestProgress(int progress)
        {
            currentCount += progress;
            Debug.Log($"Quest: {questName} progress updated. Current count: {currentCount}");
            if (currentCount >= targetCount)
            {
                CompleteQuest();
            }
        }

        public override void ResetQuest()
        {
            currentCount = 0;
            status = QuestStatus.NotStarted;
            visible = true;

            NotifyQuestStatusUpdate(); // Notify quest status update
            
            Debug.Log($"Quest: {questName} reset.");
        }

        public override void LoadData(string jsonData)
        {
            // Implement JSON loading logic here
        }

        public override string SaveData()
        {
            // Create a JSON object and return it
            return null;
        }
    }
}
