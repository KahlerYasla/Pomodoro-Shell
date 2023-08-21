using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SlidePanels : MonoBehaviour
{
    public RectTransform[] panelTransform;
    public GameObject[] panelOpenButton;

    private readonly float _slideDuration = 0.5f;

    // panelIndex: 0 and 1 = left; 2 and 3 = right
    private Vector3 _initialPositionLeft = new(-2000, 0, 0);
    private Vector3 _initialPositionRight = new(2000, 0, 0);

    public void Start()
    {
        SetThePanels();
    }

    public void SlideInPanel(int panelIndex)
    {
        SetThePanels();

        StartCoroutine(Slide(panelIndex, false));

        // slide out other panels
        for (int i = 0; i < panelTransform.Length; i++)
        {
            // print("sliding out panelIndex: " + i);
            if (i != panelIndex)
            {
                StartCoroutine(Slide(i, true));
            }
        }

        if (panelIndex == 2)
        {
            // load the ads
            WatchAndHarvestCredits.Instance.LoadRewardedAd();
        }

    }

    public void SlideOutPanel(int panelIndex)
    {
        StartCoroutine(Slide(panelIndex, true));

        // Disable the pressable property of pannel's open button in the navbar until the panel is fully slided out.
        panelOpenButton[panelIndex].GetComponent<UnityEngine.UI.Button>().interactable = false;
        StartCoroutine(EnableOpenButton(panelIndex));
    }

    // Enable the pressable property of pannel's open button in the navbar after the panel is fully slided out.
    private IEnumerator EnableOpenButton(int panelIndex)
    {
        yield return new WaitForSeconds(_slideDuration);
        panelOpenButton[panelIndex].GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    private IEnumerator Slide(int panelIndex, bool slideOut)
    {
        Vector3 _targetPosition = Vector3.zero;

        if (slideOut)
        {
            if (panelIndex == 0 || panelIndex == 1)
            {
                _targetPosition = _initialPositionLeft;
            }
            else if (panelIndex == 2 || panelIndex == 3)
            {
                _targetPosition = _initialPositionRight;
            }
        }
        else
        {
            _targetPosition = Vector3.zero;

            // set panel to active when sliding in is started.
            panelTransform[panelIndex].gameObject.SetActive(true);
        }

        float time = 0;
        while (time < _slideDuration)
        {
            time += Time.deltaTime;
            panelTransform[panelIndex].anchoredPosition = Vector3.Lerp(panelTransform[panelIndex].anchoredPosition, _targetPosition, time / _slideDuration);
            yield return null;
        }

        // set panel to inactive when sliding out is finished.
        if (slideOut)
        {
            panelTransform[panelIndex].gameObject.SetActive(false);
        }
    }

    #region Set the panels **********************************************************************************************************************
    private void SetThePanels()
    {
        UpdateProfilePanel();
        UpdateMarketPanel();
    }

    private async void UpdateProfilePanel()
    {
        // update the profile panel
        GameObject profilePanel = panelTransform[2].gameObject;
        if (profilePanel != null)
        {
            // get the profile data
            DatabaseManager databaseManager = DatabaseManager.Instance;

            ProfileModel profileData = await databaseManager.GetProfileModelAsync();

            // update the credits
            profilePanel.transform.Find("Credits").GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = profileData.Credits.ToString();

            // update the harvestable credits
            profilePanel.transform.Find("HarvestableCredits").GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = LocalSavedDataUtility.HarvestableCredits.ToString() + " / 25";
        }
    }

    public GameObject MarketContentGrid, InventoryContentGrid;
    public TMPro.TextMeshProUGUI CreditsText;
    private string[] UnlockedItems;
    private async void UpdateMarketPanel()
    {
        // clear the market content grid and inventory content grid
        foreach (Transform child in MarketContentGrid.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in InventoryContentGrid.transform)
        {
            Destroy(child.gameObject);
        }

        // get the market data
        DatabaseManager databaseManager = DatabaseManager.Instance;

        ProfileModel profileData = await databaseManager.GetProfileModelAsync();

        CreditsText.text = profileData.Credits.ToString();

        // get the unlocked items
        UnlockedItems = profileData.UnlockedItems.Split(',');

        int UnlockedItemSearchIndex = 0;


        for (int i = 0; i < NoteBook.CountOfAllItems(); i++)
        {
            int ThemeIndex = i / NoteBook.CountOfItemsPerTheme();
            int ItemIndex = i % NoteBook.CountOfItemsPerTheme();

            // create an button and set the image GameObject and load its sprite from the Resources folder and set its parent to relevant content grid
            string ItemCode = ThemeIndex.ToString() + "," + ItemIndex.ToString();
            GameObject itemImage = new GameObject(ItemCode);
            itemImage.AddComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { _ = MarketPanelController.Instance.PurchaseItemAsync(ItemCode); });
            itemImage.AddComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Sprites/CustomizableObjects/" + ThemeIndex + "/" + Dictionaries.IndexToItemName[ItemIndex]);

            // if the item is unlocked put it in the inventory content grid
            if (UnlockedItemSearchIndex < UnlockedItems.Length && UnlockedItems[UnlockedItemSearchIndex] == i.ToString())
            {
                itemImage.transform.SetParent(InventoryContentGrid.transform, false);

                UnlockedItemSearchIndex++;
            }
            else    // if the item is not unlocked put it in the market content grid
            {
                itemImage.transform.SetParent(MarketContentGrid.transform, false);
            }

        }

        // set the Content Grids' height
        MarketContentGrid.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 225 * (MarketContentGrid.transform.childCount / 5) + 1);
        InventoryContentGrid.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 225 * (InventoryContentGrid.transform.childCount / 5) + 1);

    }

    #endregion Set the panels **********************************************************************************************************************

}
