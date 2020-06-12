using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectProfile : MonoBehaviour
{
    [SerializeField] protected Transform profileItemPrefab;

    protected Dictionary<string, Profile> profiles;
    protected GameObject profilesList;
    protected GameObject newProfileInput;
    protected GameObject newProfileAdd;
    protected string newProfileName = "";

    void Start()
    {
        profiles = Profile.getProfiles();
        profilesList = GameObject.Find("ProfileListContent");
        newProfileInput = GameObject.Find("NewProfileInput");
        newProfileAdd = GameObject.Find("AddProfileInput");
        updateList();
    }

    void updateList()
    {
        clearList();
        foreach (KeyValuePair<string, Profile> item in profiles)
        {
            GameObject listItem = Instantiate(profileItemPrefab).gameObject;
            listItem.transform.parent = profilesList.transform;
            Button button = listItem.GetComponent<Button>();
            GameObject textObj = button.transform.Find("Text").gameObject;
            Text text = textObj.GetComponent<Text>();
            text.text = item.Value.name;

            button.onClick.AddListener(delegate ()
            {
                selectProfile(text.text);
            });
        }
    }

    protected void clearList()
    {
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[profilesList.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in profilesList.transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    public void bindStartAdd()
    {
        selectProfile(newProfileName);
    }

    public void bindNameFieldChange()
    {
        newProfileName = newProfileInput.GetComponent<InputField>().text;
        validateProfileName();
    }

    protected void validateProfileName()
    {
        Button addButton = newProfileAdd.GetComponent<Button>();
        addButton.interactable = !string.IsNullOrEmpty(newProfileName);
    }

    protected void selectProfile(string name)
    {
        Profile.getProfile(newProfileName);
        //TODO: open levels scene
    }

    // Update is called once per frame
    void Update()
    {

    }
}
