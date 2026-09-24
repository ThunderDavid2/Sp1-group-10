using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroSceneManager : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    private bool skipped = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(waitFunction), 75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void waitFunction()
    {
        if (skipped == false)
        {
            LoadNextLevel();
        }
        
    }
    private void LoadNextLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void SkipIntro()
    {
        skipped = true;
        // Load the next level after
        SceneManager.LoadScene(sceneIndex);   
    }

}
