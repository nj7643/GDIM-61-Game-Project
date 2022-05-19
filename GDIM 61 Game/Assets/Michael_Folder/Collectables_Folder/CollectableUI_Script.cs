using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableUI_Script : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Image image;

    private string collectableName;

    public void SetInfo(string incomingName, string incomingHint, string incomingText)
    {
        nameText.text = incomingText;
        collectableName = incomingName;
        descriptionText.text = incomingHint;
    }

    public void DisplayInfo(string incomingName, string incomingDescription)
    {
        nameText.text = incomingName;
        descriptionText.text = incomingDescription;
    }

    public string GetCollectableName()
    {
        return collectableName;
    }

}
