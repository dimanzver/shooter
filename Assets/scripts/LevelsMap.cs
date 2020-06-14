using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelsMap : MonoBehaviour
{

    void Start()
    {
        Profile profile = Profile.getProfile();
        if(profile == null)
        {
            SceneManager.LoadScene("Scenes/Profile");
            return;
        }
        updateMap();
    }

    void updateMap()
    {
        Profile profile = Profile.getProfile();

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
}
