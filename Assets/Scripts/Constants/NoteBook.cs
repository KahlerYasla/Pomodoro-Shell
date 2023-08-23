using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.IO;

public static class NoteBook
{
    // there are 2 folders in the CostumazibleObjects folder each representing a theme
    // give the count of the themes which is count of the folders
    public static int CountOfThemes() => 4;
    public static int CountOfItemsPerTheme() => Dictionaries.IndexToItemName.Count;
    public static int CountOfAllItems() => CountOfItemsPerTheme() * CountOfThemes();

}
