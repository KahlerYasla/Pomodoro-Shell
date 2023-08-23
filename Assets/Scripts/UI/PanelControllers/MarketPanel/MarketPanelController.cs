using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

// singleton class

public class MarketPanelController : MonoBehaviour
{
    public GameObject InventoryGridContent;

    public static MarketPanelController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public async Task PurchaseItemAsync(string itemCodeSeperated)
    {
        int ThemeIndex = int.Parse(itemCodeSeperated.Split(',')[0]);
        int ItemIndex = int.Parse(itemCodeSeperated.Split(',')[1]);

        // if the item is already purchased, then set the item to the current item
        if (InventoryGridContent.transform.Find(itemCodeSeperated) != null)
        {
            // compose the item code
            int itemCode = (ThemeIndex * NoteBook.CountOfItemsPerTheme()) + ItemIndex;

            // set the current item
            DatabaseManager databaseManager = DatabaseManager.Instance;

            ProfileModel profileData = await databaseManager.GetProfileModelAsync();

            string[] currentItems = profileData.ActiveItems.Split(',');
            currentItems[ItemIndex] = itemCode.ToString();

            profileData.ActiveItems = string.Join(",", currentItems);

            databaseManager.UpdateDatabase(profileData);


            SceneReloader.ReloadScene();
        }
        else    // open the buy dialog
            BuyDialog.Instance.OpenBuyDialog(ThemeIndex, ItemIndex);
    }
}
