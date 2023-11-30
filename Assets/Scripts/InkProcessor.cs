using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class InkProcessor : MonoBehaviour
{
    public InkProcessor inkProcRef;
    public TextAsset rumourInkJSON;
    public Story rumourInkStory;
    public TextMeshProUGUI screenText;
    float displaySpeed = 0.05f;


    private void Awake()
    {
        inkProcRef = this;
        rumourInkStory = new Story(rumourInkJSON.text);
    }

    public void SetStoryKnot(string knotName) //adventurers will have rumourknot stored and will call this function when triggering rumour
    {
        rumourInkStory.ChoosePathString(knotName);
    }

    void RefreshText()
    {
        if (rumourInkStory.canContinue)
        {
            string text = rumourInkStory.Continue();
        }
    }
    
    public IEnumerator DisplayText(string text)
    {
        screenText.text = "";

        foreach  (char letter in text.ToCharArray())
        {
            screenText.text += letter;
            yield return new WaitForSeconds(displaySpeed);
        }

        if (rumourInkStory.canContinue)
        {
            RefreshText();
        }
    }

    public void InkTagHandler(string inkTag)
    {

    }
}
