using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Surviblewilderness
{
    public class QuestUiManager : MonoBehaviour
    {
        public static event Action<QuestGeneric,Transform> OnQuestUpdate;

        [SerializeField] private GameObject questUiPrefab;
        [SerializeField] private GameObject questUiPanel;
        [SerializeField] private TMP_Text questDescriptionText;

        [Header("Layouts for different status of quest")]
        [SerializeField] private Transform completedQuestLayout;
        [SerializeField] private Transform inProgressQuestLayout;
        [SerializeField] private Transform notStartedQuestLayout;
        [SerializeField] private Transform failedQuestLayout;

        private List<QuestUi> questUiList = new List<QuestUi>();

        private void OnEnable()
        {
            QuestGeneric.OnQuestStatusUpdate += ListenToStatusUpdate;
            QuestUi.OnQuestUiClicked += ShowDescription;
        }
        private void OnDisable()
        {
            QuestGeneric.OnQuestStatusUpdate -= ListenToStatusUpdate;
            QuestUi.OnQuestUiClicked -= ShowDescription;
        }

        void Awake()
        {
            //get all the quests from the quest manager and add them to the ui
            if(SceneManager.GetActiveScene().name == "Combine_Level")
            {
                questUiPanel.SetActive(false);
            }
            else
            {
                questUiPanel.SetActive(true);
            }   
            QuestManager questManager = GameObject.FindAnyObjectByType<QuestGameManager>().QuestManager;

            if (questManager == null)
            {
                Debug.LogError("QuestManager not found in the scene.");
                return;
            }
            foreach (var quest in questManager.GetActiveQuests())
            {
                //check if the quest is not null and add it to the ui
                if (quest != null)
                {
                    UpdateQuestUI(quest, null);
                }
                else
                {
                    Debug.LogError("Quest is null");
                }
            }
        }

        void ShowDescription(QuestUi questUi)
        {
            //display the description on the description panel
            if(questUi == null)
            {
                Debug.LogError("QuestUi is null");
                return;
            }
            questDescriptionText.text = questUi.Quest.QuestDescription;
            Debug.Log($"Quest Description: {questUi.Quest.QuestDescription}");
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                questDescriptionText.text = "";
                questUiPanel.SetActive(!questUiPanel.activeSelf);
            }
        }
        
        public void RemoveQuestUi(QuestGeneric quest)
        {
            //remove quest ui from the dictionary

        }

        //add quest to the ui 
        public void AddQuestToUi(QuestGeneric quest, Transform layoutParent)
        {
            GameObject questUiGO = Instantiate(questUiPrefab, layoutParent);
            QuestUi questUi = questUiGO.GetComponent<QuestUi>();
            questUi.SetQuest(quest);
            questUiList.Add(questUi);   
        }

        private void ListenToStatusUpdate(QuestGeneric _updatedQuest)
        {
            foreach(var questUi in questUiList)
            {
                if (questUi.Quest.QuestId == _updatedQuest.QuestId)
                {
                    //fire event to update the quest from the quest ui manager 
                    UpdateQuestUI(questUi.Quest, questUi.transform);
                    break;
                }
            } 
        }

        public void UpdateQuestUI(QuestGeneric quest,Transform questTransform)
        {
            if (quest == null)
            {
                Debug.Log("Quest is null");
                return;
            }

            //update quest ui elements based on their status   
            switch (quest.Status)
            {
                case QuestStatus.Completed:
                    // Update UI to show quest completion
                    // if the ui element of quest does not exist in the layout then create one and add it to the layout
                    if (questTransform == null)
                        AddQuestToUi(quest, completedQuestLayout);
                    else
                        questTransform.SetParent(completedQuestLayout);
                    Debug.Log($"Quest '{quest.QuestName}' is completed!");
                    break;
                case QuestStatus.InProgress:
                    // Update UI to show quest in progress
                    // if the ui element of quest does not exist in the layout then create one and add it to the layout
                    if (questTransform == null)
                        AddQuestToUi(quest, inProgressQuestLayout);
                    else
                        questTransform.SetParent(inProgressQuestLayout);
                    Debug.Log($"Quest '{quest.QuestName}' is in progress.");
                    break;
                case QuestStatus.NotStarted:
                    // Update UI to show quest not started
                    // if the ui element of quest does not exist in the layout then create one and add it to the layout
                    if (questTransform == null)
                        AddQuestToUi(quest, notStartedQuestLayout);
                    else
                        questTransform.SetParent(notStartedQuestLayout);
                    Debug.Log($"Quest '{quest.QuestName}' is not started yet.");

                    break;
                default:
                    Debug.Log($"Quest '{quest.QuestName}' has an unknown status.");
                    break;
            }
        }

    }
}
