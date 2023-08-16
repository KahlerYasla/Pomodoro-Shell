using System.Collections;
using UnityEngine;

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

        // print("sliding in panelIndex: " + panelIndex);
        // slide out other panels
        for (int i = 0; i < panelTransform.Length; i++)
        {
            // print("sliding out panelIndex: " + i);
            if (i != panelIndex)
            {
                StartCoroutine(Slide(i, true));
            }
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
    }

    private async void UpdateProfilePanel()
    {
        // update the profile panel
        GameObject profilePanel = panelTransform[2].gameObject;
        if (profilePanel != null)
        {
            // get the profile data
            DatabaseManager databaseManager = new();

            ProfileModel profileData = await databaseManager.GetProfileModelAsync();

            Debug.Log("Credits: " + profileData.Credits);

            // update the credits
            profilePanel.transform.Find("Credits").GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = profileData.Credits.ToString();

            // update the harvestable credits
            profilePanel.transform.Find("HarvestableCredits").GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = LocalSavedDataUtility.HarvestableCredits.ToString();
        }
    }
    #endregion Set the panels **********************************************************************************************************************
}
