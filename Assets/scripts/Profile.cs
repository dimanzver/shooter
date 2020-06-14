using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class Profile
{
    [JsonProperty]
    public Dictionary<int, int> finishedLevels = new Dictionary<int, int> { };
    [JsonProperty]
    public string name;

    protected static Dictionary<string, Profile> profilesDict = null;

    const string PROFILES_KEY = "profiles";
    const string PROFILE_NAME_KEY = "profileName";

    public Profile(string name)
    {
        this.name = name;
    }

    public void setLevel(int level, int result)
    {
        int oldResult = 0;
        if(finishedLevels.TryGetValue(level, out oldResult))
        {
            if (oldResult > result)
                result = oldResult;
        }
        finishedLevels[level] = result;
        setProfiles(profilesDict);
    }

    public static string getCurrentProfileName()
    {
        return PlayerPrefs.GetString(PROFILE_NAME_KEY);
    }

    public static void setProfile(string profileName)
    {
        PlayerPrefs.SetString(PROFILE_NAME_KEY, profileName);
    }

    public static Profile getProfile(string profileName = null)
    {
        if (profileName == null)
            profileName = getCurrentProfileName();
        if (string.IsNullOrEmpty(profileName))
            return null;
        
        Dictionary<string, Profile> profiles = getProfiles();
        if (!profiles.ContainsKey(profileName))
            return registerProfile(profileName);

        return profiles[profileName];
    }

    public static void removeProfile(string name)
    {
        Dictionary<string, Profile> profiles = getProfiles();
        profiles.Remove(name);
    }

    public static Profile registerProfile(string name)
    {
        Profile profile = new Profile(name);
        Dictionary<string, Profile> profiles = getProfiles();
        profiles.Add(name, profile);
        setProfiles(profiles);
        return profile;
    }

    public static Dictionary<string, Profile> getProfiles()
    {
        if (profilesDict == null)
        {
            profilesDict = new Dictionary<string, Profile> { };
            Dictionary<string, object> profilesParsed = LocalStorage.getDictionary(PROFILES_KEY);
            if (profilesParsed == null)
            {
                profilesParsed = new Dictionary<string, object> { };
            }

            foreach (KeyValuePair<string, object> item in profilesParsed)
            {
                string serializedProfile = JsonConvert.SerializeObject(item.Value);
                profilesDict.Add(item.Key, JsonConvert.DeserializeObject<Profile>(serializedProfile));
            }
        }


        return profilesDict;
    }

    public static void removeAll()
    {
        profilesDict = new Dictionary<string, Profile> { };
        setProfiles(profilesDict);
    }

    protected static void setProfiles(Dictionary<string, Profile> profiles)
    {
        Dictionary<string, object> profilesToSave = new Dictionary<string, object> { };

        foreach (KeyValuePair<string, Profile> item in profiles)
        {
            profilesToSave.Add(item.Key, (object)item.Value);
        }

        LocalStorage.setDictionary(PROFILES_KEY, profilesToSave);
    }

}
