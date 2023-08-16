using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase;
using System.Threading.Tasks;

public class DatabaseManager : MonoBehaviour
{
    private string _ID;
    private DatabaseReference _dbReference;
    public TMPro.TMP_Text HarvestableCreditsText;

    // Start is called before the first frame update
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

            Debug.Log("Profile exists, retrieved profile: " + profile.ID);
            Debug.Log("Credits: " + profile.Credits);
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
        ProfileModel profile = new ProfileModel(_ID, 0, "", "");
        string json = JsonUtility.ToJson(profile);
        _dbReference.Child("profiles").Child(_ID).SetRawJsonValueAsync(json);
        Debug.Log("Profile does not exist, created new profile: " + profile.ID);

        return profile;
    }

    // Update the database with the new profile model
    public void UpdateDatabase(ProfileModel profile)
    {
        string json = JsonUtility.ToJson(profile);
        _dbReference.Child("profiles").Child(_ID).SetRawJsonValueAsync(json);
        Debug.Log("Updated profile: " + profile.ID);
    }

}
