using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(LoadNextLevel), 60f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }

}
