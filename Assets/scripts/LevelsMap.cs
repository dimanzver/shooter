using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelsMap : MonoBehaviour
{

    void Start()
    {
        Profile profile = Profile.getProfile();
        if(profile == null)
        {
            toProfile();
            return;
        }
        updateMap();
    }

    void updateMap()
    {
        //обновляем имя игрока
        Profile profile = Profile.getProfile();
        GameObject profileNameObj = GameObject.Find("profileName");
        profileNameObj.GetComponent<Text>().text = profile.name;

        //обновляем карту
        GameObject[] mapItems = GameObject.FindGameObjectsWithTag("mapItem");
        int maxLevelFinished = 0;
        foreach(KeyValuePair<int, int> item in profile.finishedLevels)
        {
            if (item.Key > maxLevelFinished)
                maxLevelFinished = item.Key;
        }

        foreach (GameObject mapItem in mapItems)
        {
            MapItem mapItemObj = mapItem.GetComponent<MapItem>();
            mapItemObj.available = mapItemObj.level - 1 <= maxLevelFinished;

            int levelPoints = 0;
            if (profile.finishedLevels.TryGetValue(mapItemObj.level, out levelPoints))
            {
                mapItemObj.levelPoints = levelPoints;
            }

            mapItemObj.init();
        }
    }

    public void toProfile()
    {
        SceneManager.LoadScene("Scenes/Profile");
    }

    public void exit()
    {
        Application.Quit();
    }
}
