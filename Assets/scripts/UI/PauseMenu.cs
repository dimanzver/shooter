using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bindContinue()
    {
        Game.play();
    }

    public void newGame()
    {
        Game.newGame();
    }

    public void exit()
    {
        Application.Quit();
    }
}
