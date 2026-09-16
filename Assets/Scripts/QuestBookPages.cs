
using UnityEngine;
using UnityEngine.VFX;
public class QuestBookPages : MonoBehaviour
{
    [SerializeField] private GameObject questBook;
    [SerializeField] private GameObject quest1;


    public void QuestBookPage1()
    {
        questBook.SetActive(false);
        quest1.SetActive(true);
        
    }


}
