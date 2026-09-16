
using UnityEngine;
using UnityEngine.InputSystem;
public class QuestLogBook : MonoBehaviour
{
    [SerializeField] private InputActionReference openOrCloseQuestBook;
    [SerializeField] private GameObject questBookPanel, questBookText;

    private bool questBookButton;
    bool questBookInput = false;




    private void Update()
      {  
        QuestBookInputUpdate();
      }


    private void QuestBookInputUpdate()
    {
        questBookButton = openOrCloseQuestBook.action.triggered;

        if (questBookButton)
       {
            if(questBookInput == false)
            {
                questBookInput = true;
            }
           else
            {
                questBookInput = false;
            }
        }
        QuestBookUI();

    }
    
    private void QuestBookUI()
    {
        if (questBookInput == true)
        {
            questBookPanel.SetActive(true);
            questBookText.SetActive(true);
            GetComponent<QuestLogBookText>().QuestProgressText();
        }
        else
        {
            questBookPanel.SetActive(false);
            questBookText.SetActive(false);
        }
    }
    
}
