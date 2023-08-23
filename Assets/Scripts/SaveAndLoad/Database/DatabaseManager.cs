using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;
using System.Linq;

// singleton class
public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    private string _ID;
    private DatabaseReference _dbReference;
    public TMPro.TMP_Text HarvestableCreditsText;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _ID = SystemInfo.deviceUniqueIdentifier;
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // get profile model as a ProfileModel object
    public async Task<ProfileModel> GetProfileModelAsync()
    {
        _ID = SystemInfo.deviceUniqueIdentifier;
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        // if profile exists, retrieve profile else create new profile
        ProfileModel profile;

        // check if profile exists
        DataSnapshot snapshot = await _dbReference.Child("profiles").Child(_ID).GetValueAsync();
        if (snapshot.Exists)
        {
            // profile exists
            profile = JsonUtility.FromJson<ProfileModel>(snapshot.GetRawJsonValue());
        }
        else
        {
            // profile does not exist
            profile = CreateProfileModel();
        }

        return profile;
    }

    // create new profile
    private ProfileModel CreateProfileModel()
    {
        // creating a string of all default items separated by commas
        string items = string.Join(",", Enumerable.Range(0, NoteBook.CountOfItemsPerTheme()).ToArray());

        // creating a new profile model and uploading it to the database
        ProfileModel profile = new(_ID, 30, items, items);

        UpdateDatabase(profile);

        Debug.Log("Profile does not exist, created new profile: " + profile.ID);

        return profile;
    }

    // Update the database with the new profile model
    public void UpdateDatabase(ProfileModel profile)
    {
        string json = JsonUtility.ToJson(profile);
        Debug.Log("Updating profile: " + json);


        _dbReference.Child("profiles").Child(_ID).SetRawJsonValueAsync(json);
        Debug.Log("Updated profile: " + profile.ID);
    }

}
