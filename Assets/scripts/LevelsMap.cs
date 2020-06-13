using UnityEngine;
using UnityEngine.SceneManagement;

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
        foreach(GameObject mapItem in mapItems)
        {
            MapItem mapItemObj = mapItem.GetComponent<MapItem>();

            if(profile != null)
            {
                mapItemObj.available = mapItemObj.level == 1 || profile.finishedLevels.ContainsKey(mapItemObj.level);
                int levelPoints = 0;
                if (profile.finishedLevels.TryGetValue(mapItemObj.level, out levelPoints))
                {
                    mapItemObj.levelPoints = levelPoints;
                }
            }
            else
            {
                mapItemObj.available = mapItemObj.level == 1;
            }

            mapItemObj.init();
        }
    }
}
