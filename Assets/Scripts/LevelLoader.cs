using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static Level levelToLoad;

    void Start()
    {
        GameObject[] loaders = GameObject.FindGameObjectsWithTag("LevelLoader");
        if(loaders.Length > 1)
        {
            Destroy(loaders[0].gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(LevelButton button)
    {
        levelToLoad = button.level;
    }
}
