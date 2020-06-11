using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Game : MonoBehaviour
{
    public static bool paused = false;

    protected static GameObject pauseMenu;
    protected static GameObject endGameMenu;

    void Start()
    {
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
        if(pauseMenu)
            Destroy(pauseMenu);
        if (endGameMenu)
            Destroy(endGameMenu);
    }

    public static void pause()
    {
        paused = true;
        Time.timeScale = 0;
        var pauseMenuPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/PauseMenu.prefab");
        pauseMenu = Instantiate(pauseMenuPrefab, Vector3.zero, Quaternion.identity).gameObject;
    }

    public static void end()
    {
        paused = true;
        Time.timeScale = 0;
        var endGameMenuPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/GameEndMenu.prefab");
        endGameMenu = Instantiate(endGameMenuPrefab, Vector3.zero, Quaternion.identity).gameObject;
    }

    public static void newGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        play();
    }
}
