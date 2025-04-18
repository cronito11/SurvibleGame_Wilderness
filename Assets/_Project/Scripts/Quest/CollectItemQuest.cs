using UnityEngine;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "Collect Item Quest", menuName = "Scriptable Objects/Quests/Collect Item Quest")]
    public class CollectItemQuest : QuestGeneric
    {
        [SerializeField] GameElement itemType;
        [SerializeField] private int targetCount;
        [SerializeField] private int currentCount;

        public GameElement ItemType => itemType;
        public override void StartQuest ()
        {
            status = QuestStatus.InProgress;
            Debug.Log($"Quest {questName} started.");
        }
        public override void CompleteQuest ()
        {
            status = QuestStatus.Completed;
            Debug.Log($"Quest {questName} completed.");
        }
        public override void FailQuest ()
        {
            status = QuestStatus.Failed;
            Debug.Log($"Quest {questName} failed.");
        }
        public override void UpdateQuestProgress (int progress)
        {
            currentCount += progress;
            Debug.Log($"Quest {questName} progress updated. Current count: {currentCount}");
            if (currentCount >= targetCount)
            {
                CompleteQuest();
            }
        }

        public override void ResetQuest ()
        {
            currentCount = 0;
            status = QuestStatus.NotStarted;
            visible = true;

            Debug.Log($"Quest {questName} reset.");
        }
        public override void LoadData (string jsonData)
        {
            // Implement JSON loading logic here
        }

        public override string SaveData()
        {
            //create a json object and return it
            return null;
        }
    }
}
