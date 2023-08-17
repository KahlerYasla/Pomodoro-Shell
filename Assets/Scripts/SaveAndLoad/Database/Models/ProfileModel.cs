using System;

public class ProfileModel
{
    public string ID;

    // each active item is seperated by a comma and saved by its ID
    public string ActiveItems;

    // each unlocked item is seperated by a comma and saved by its ID
    public string UnlockedItems;
    public int Credits;

    public ProfileModel(string ID, int Credits, string ActiveItems, string UnlockedItems)
    {
        this.ID = ID;
        this.Credits = Credits;
        this.ActiveItems = ActiveItems;
        this.UnlockedItems = UnlockedItems;
    }
}
