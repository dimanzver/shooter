using UnityEngine;

public class Game : MonoBehaviour
{
    public static bool paused = false;

    protected static GameObject pauseMenu;

    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        play();
    }

    // Update is called once per frame
    void Update()
    {
        checkPause();
    }

    void checkPause()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        if (!paused)
        {
            pause();
        }
        else
        {
            play();
        }
    }

    public static void play()
    {
        paused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public static void pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
}
