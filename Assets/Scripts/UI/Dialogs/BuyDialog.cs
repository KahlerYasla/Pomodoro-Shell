using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// singleton class
public class BuyDialog : MonoBehaviour
{
    public static BuyDialog Instance { get; private set; }

    public GameObject buyDialog;
    public Image itemImage;
    public TMPro.TextMeshProUGUI priceText;

    private int _themeIndex;
    private int _itemIndex;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenBuyDialog(int themeIndex, int itemIndex)
    {
        _themeIndex = themeIndex;
        _itemIndex = itemIndex;

        // set the item image
        itemImage.sprite = Resources.Load<Sprite>("Sprites/CustomizableObjects/" + _themeIndex + "/" + Dictionaries.IndexToItemName[_itemIndex]);

        // set the price text
        priceText.text = "30";

        // open the buy dialog
        buyDialog.SetActive(true);
    }

    public void CloseBuyDialog()
    {
        // close the buy dialog
        buyDialog.SetActive(false);
    }

    public async void BuyButton()
    {
        // update the database
        DatabaseManager databaseManager = DatabaseManager.Instance;

        ProfileModel profileData = await databaseManager.GetProfileModelAsync();

        // if the player has enough credits
        if (profileData.Credits >= 30)
        {
            // deduct the credits
            profileData.Credits -= 30;

            // add the item to the inventory
            string[] unlockedItems = profileData.UnlockedItems.Split(',');
            unlockedItems = unlockedItems.Append((_themeIndex * NoteBook.CountOfItemsPerTheme() + _itemIndex).ToString()).ToArray();
            Debug.Log(string.Join(",", unlockedItems));

            // sort ascending the unlocked items
            unlockedItems = unlockedItems.OrderBy(x => int.Parse(x)).ToArray();

            profileData.UnlockedItems = string.Join(",", unlockedItems);

            databaseManager.UpdateDatabase(profileData);

            // close the buy dialog
            CloseBuyDialog();

            // reload the scene
            SceneReloader.ReloadScene();
        }
        else
        {
            // show a message that the player does not have enough credits
            Debug.Log("Not enough credits");
        }
    }

}
