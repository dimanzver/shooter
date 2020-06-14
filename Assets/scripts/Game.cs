using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class Game : MonoBehaviour
{
    public static bool paused = false;
    public const int levels = 10;

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

    public static void endLevel()
    {
        paused = true;
        GameObject player = GameObject.Find("Player");
        PlayerController playerScript = player.GetComponent<PlayerController>();
        float healthRel = playerScript.health / playerScript.maxHealth;
        int stars = healthRel >= 0.7 ? 
                    3 :
                    (healthRel >= 0.4 ? 2 : 1);
        Profile profile = Profile.getProfile();
        int level = Convert.ToInt32(SceneManager.GetActiveScene().name.Replace("Level ", ""));
        profile.setLevel(level, stars);
        openLevelResults(level, stars);
    }

    public static void startLevel(int level)
    {
        SceneManager.LoadScene("Scenes/Level " + level);
    }

    static void openLevelResults(int level, int stars)
    {
        var levelResultsPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/prefabs/LevelResults.prefab");
        GameObject levelResults = Instantiate(levelResultsPrefab).gameObject;
        LevelResults levelResultsScript = levelResults.GetComponent<LevelResults>();
        levelResultsScript.level = level;
        levelResultsScript.stars = stars;
        levelResultsScript.init();
    }
}
