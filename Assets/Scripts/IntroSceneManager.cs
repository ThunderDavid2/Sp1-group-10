using UnityEngine;

public class IntroSceneManager : MonoBehaviour
{
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
