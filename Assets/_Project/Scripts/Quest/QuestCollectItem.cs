using UnityEngine;

namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "QuestCollectionItem", menuName = "Scriptable Objects/QuestCollectionItem")]
    public class QuestCollectItem : QuestGeneric
    {
        public override void StartQuest ()
        {
            Debug.Log($"Quest {questName} started.");
        }
        public override void CompleteQuest ()
        {
            Debug.Log($"Quest {questName} completed.");
        }
        public override void FailQuest ()
        {
            Debug.Log($"Quest {questName} failed.");
        }
        public override void UpdateQuestProgress (int progress)
        {
        }

        public override void ResetQuest ()
        {
            Debug.Log($"Quest {questName} reset.");
        }
        public override void LoadData (string jsonData)
        {
            // Implement JSON loading logic here
        }
    }
}
