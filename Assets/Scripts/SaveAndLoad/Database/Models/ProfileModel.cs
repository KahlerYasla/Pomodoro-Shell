using System;

public class ProfileModel
{
    public string ID;

    // active items are stored as a string of numbers separated by commas, rows are separated by semicolons and represent the theme of the item
    // 1 = active, 0 = inactive
    public string ActiveItems;

    // unlocked items are stored as a string of numbers separated by commas, rows are separated by semicolons and represent the theme of the item
    // 1 = unlocked, 0 = locked
    public string UnlockedItems;
    public int Credits;

    public ProfileModel(string ID, int Credits)
    {
        this.ID = ID;
        this.Credits = Credits;
    }
}
