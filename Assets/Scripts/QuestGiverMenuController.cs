using UnityEngine;

public class QuestGiverMenuController : MonoBehaviour
{
    [SerializeField] private GameObject questGiverMenu, page1;

    public void HideQuestGiverMenu()
    {
        questGiverMenu.SetActive(false);
    }

    public void HideQuestPage1()
    {
        page1.SetActive(false);
    }
}
