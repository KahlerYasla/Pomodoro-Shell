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
    public TMPro.TMP_Text TotalMinutesDoneText, CreditsText, HarvestableCreditsText;

    // Start is called before the first frame update
    void Start()
    {
        _ID = SystemInfo.deviceUniqueIdentifier;
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async Task<ProfileModel> GetProfileModelAsync()
    {
        _ID = SystemInfo.deviceUniqueIdentifier;
        _dbReference = FirebaseDatabase.DefaultInstance.RootReference;

        // if profile exists, retrieve profile else create new profile
        ProfileModel profile = null;

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



    private ProfileModel CreateProfileModel()
    {
        ProfileModel profile = new ProfileModel(_ID, 0);
        string json = JsonUtility.ToJson(profile);
        _dbReference.Child("profiles").Child(_ID).SetRawJsonValueAsync(json);
        Debug.Log("Profile does not exist, created new profile: " + profile.ID);

        return profile;
    }

    // Updates the total minutes done with adding the total minutes done to the current total minutes done
    public void UpdateProfileTotalMinutesDone()
    {
        // check if profile exists
        _dbReference.Child("profiles").Child(_ID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error getting data from database");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    // profile exists
                    ProfileModel profile = JsonUtility.FromJson<ProfileModel>(snapshot.GetRawJsonValue());
                    string json = JsonUtility.ToJson(profile);
                    _dbReference.Child("profiles").Child(_ID).SetRawJsonValueAsync(json);
                    Debug.Log("Profile exists, updated profile: " + profile.ID);
                }
                else
                {
                    // profile does not exist
                    CreateProfileModel();
                }
            }
        });
    }

    // Updates the credits with adding the harvestable credits to the current credits and then sets the harvestable credits to 0
    public void UpdateProfileCredits()
    {
        // check if profile exists
        _dbReference.Child("profiles").Child(_ID).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Error getting data from database");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    // profile exists
                    ProfileModel profile = JsonUtility.FromJson<ProfileModel>(snapshot.GetRawJsonValue());

                    profile.Credits += int.Parse(HarvestableCreditsText.text);

                    HarvestableCreditsText.text = "0";
                    LocalSavedDataUtility.HarvestableCredits = 0;

                    string json = JsonUtility.ToJson(profile);
                    _dbReference.Child("profiles").Child(_ID).SetRawJsonValueAsync(json);
                    Debug.Log("Profile exists, updated profile: " + profile.ID);
                }
                else
                {
                    // profile does not exist
                    CreateProfileModel();
                }
            }
        });
    }

}
