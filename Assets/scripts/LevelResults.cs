using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelResults : MonoBehaviour
{
    public int level;
    public int stars;

    // Start is called before the first frame update
    public void init()
    {
        //update stars
        GameObject levelEndTitle = GameObject.Find("LevelEndTitle");
        levelEndTitle.GetComponent<Text>().text = "Уровень " + level + " пройден (" + stars + " из 3)";
    }

    public void exit()
    {
        Application.Quit();
    }

    public void toLevelsMap()
    {
        SceneManager.LoadScene("Scenes/Levels");
    }

    public void next()
    {
        if(level >= Game.levels)
        {
            SceneManager.LoadScene("Scenes/Levels");
        }
        else
        {
            SceneManager.LoadScene("Scenes/Level " + (level + 1));
        }
    }
}
