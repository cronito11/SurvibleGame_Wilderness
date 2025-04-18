using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Surviblewilderness
{
    public class QuestUi : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<QuestUi> OnQuestUiClicked;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private QuestGeneric quest;
        [SerializeField] private TMP_Text questTitle;
        [SerializeField] private TMP_Text levRequired;
       
        public QuestGeneric Quest => quest;
        public TMP_Text QuestTitle => questTitle;
        public TMP_Text LevRequired => levRequired;
       
        public void SetQuest(QuestGeneric quest)
        {
            this.quest = quest;
            this.questTitle.text = quest.QuestName;
        }

        private void StartQuest()
        {
            if(quest == null)
            {
                Debug.LogError("Quest is null");
                return;
            }
            if (quest.Status != QuestStatus.NotStarted)
            {
                Debug.Log($"Quest {quest.QuestName} is already started or completed.");
                return;
            }
            quest.StartQuest();
        }

        #region Double Click Functionality

        private float lastClickTime = 0f;
        private const float doubleClickThreshold = 0.3f; // seconds
        public void OnPointerClick(PointerEventData eventData)
        {
            //display the description on the description panel
            OnQuestUiClicked?.Invoke(this);
            // Check if the click is a double click
            float timeSinceLastClick = Time.time - lastClickTime;

            if (timeSinceLastClick <= doubleClickThreshold)
            {
                // Double click detected
                //trigger double click event that will update the status of thhis quest to in progress from quest ui manager
                StartQuest();   
                Debug.Log($"Double Click! on quest {quest.QuestName}");
            }

            lastClickTime = Time.time;
        }
        #endregion
    }
}
