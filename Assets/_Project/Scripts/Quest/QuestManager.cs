using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Player,
    NPC,
    Enemy,
    Item
}
namespace Surviblewilderness
{
    [CreateAssetMenu(fileName = "QuestManager", menuName = "Scriptable Objects/QuestManager")]
    public class QuestManager : ScriptableObject
    {
        [SerializeField] private QuestGeneric[] quests;

        public int QuestCount => quests.Length;

        public List<QuestGeneric> GetActiveQuests ()
        {
            List<QuestGeneric> activeQuests = new List<QuestGeneric>();
            foreach (var quest in quests)
            {
                if(quest.Status == QuestStatus.InProgress || quest.Status == QuestStatus.NotStarted &&
                    quest.Visible)    
                    activeQuests.Add(quest);
            }
            return activeQuests;
        }

        public QuestGeneric GetQuest(string questId)
        {
            foreach (var quest in quests)
            {
                if (quest.QuestId == questId)
                {
                    return quest;
                }
            }
            return null;
        }

        public void StartQuest (string questId)
        {
            QuestGeneric quest = GetQuest(questId);
            if (quest != null)
            {
                quest.StartQuest();
            } else
            {
                Debug.LogWarning($"Quest with ID {questId} not found.");
            }
        }

        public void CompleteQuest (string questId)
        {
            QuestGeneric quest = GetQuest(questId);
            if (quest != null)
            {
                quest.CompleteQuest();
            } else
            {
                Debug.LogWarning($"Quest with ID {questId} not found.");
            }
        }

    }
}
