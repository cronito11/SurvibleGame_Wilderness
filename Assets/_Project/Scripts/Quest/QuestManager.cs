using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

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

    class QuestGameManager : MonoBehaviour // Should Be in the gamescene // or in Dont Destroy
    {
        [SerializeField] private QuestManager questManager;
        private PlayerInventory inventoryManager;


        private void Awake ()
        {
            inventoryManager = PlayerInventory.Instance;
        }
        private void Start ()
        {
            PlayerInventory.OnInventoryChanged += OnInventoryChanged;
            //L
            // link to Event of player
        }

        private void OnDestroy ()
        {
            PlayerInventory.OnInventoryChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged (InventoryItem item)
        {
            throw new NotImplementedException();
        }
    }
}
