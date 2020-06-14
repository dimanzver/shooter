using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void exit()
    {
        Application.Quit();
    }

    public void newGame()
    {
        Game.newGame();
    }

    public void toLevelsMap()
    {
        SceneManager.LoadScene("Scenes/Levels");
    }
}
