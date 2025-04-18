using UnityEngine;
namespace Surviblewilderness
{
    class QuestGameManager : MonoBehaviour // Should Be in the gamescene // or in Dont Destroy
    {
        [SerializeField] private QuestManager questManager;
        
        private void Start ()
        {
            PlayerInventory.OnInventoryChanged += OnInventoryChanged;
            DestroyCharacterDeath.OnCharacterDeath += OnCharacterDeath;
            TimeController.OnAnHourPassed += SurvialQuestTracking;

        }

        private void OnDestroy ()
        {
            PlayerInventory.OnInventoryChanged -= OnInventoryChanged;
            DestroyCharacterDeath.OnCharacterDeath -= OnCharacterDeath;
            TimeController.OnAnHourPassed -= SurvialQuestTracking;
        }

        private void OnInventoryChanged (InventoryItem item)
        {
            foreach (var quest in questManager.GetActiveQuests())
            {
                CollectItemQuest collectItemQuest = quest as CollectItemQuest;
                if (collectItemQuest)
                {
                    if (collectItemQuest.Status == QuestStatus.Completed)
                        return;
                    if (item.gameItem.gameElement == collectItemQuest.ItemType)
                    {
                        //implement update logic 
                        collectItemQuest.UpdateQuestProgress(item.quantity);
                    }                   
                }
            }
        }

        private void OnCharacterDeath(EntityType entityType)
        {
            foreach (var quest in questManager.GetActiveQuests())
            {
                KillQuest killQuest = quest as KillQuest;

                if (killQuest)
                {
                    if(killQuest.Status == QuestStatus.Completed)
                        return;
                    //implement update logic 
                    //killQuest.
                    if (entityType == killQuest.TargetEntity)
                    {
                        killQuest.UpdateQuestProgress(1);
                    }
                }
            }

            
        }

        private void SurvialQuestTracking()
        {
            //implement survival quest tracking

            foreach (var quest in questManager.GetActiveQuests())
            {
                SurviveQuest survivalQuest = quest as SurviveQuest;
                if (survivalQuest)
                {
                    if (survivalQuest.Status == QuestStatus.Completed)
                        return;
                    survivalQuest.UpdateQuestProgress(1);
                }
            }
        }
    }
}
