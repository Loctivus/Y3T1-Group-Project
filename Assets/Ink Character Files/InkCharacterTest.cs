using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class InkCharacterTest : MonoBehaviour
{
    public TextAsset inkCharJSON;
    public Story inkCharStory;
    public string charName;
    public GameObject charPanel;
    public GameObject charImage;
    public TextMeshProUGUI charText;

    [SerializeField]
    private Canvas canvas;

    void Awake()
    {
        inkCharStory = new Story(inkCharJSON.text);   
    }

    private void OnEnable()
    {
        StartDialogue();
    }

    public void CharDialogue()
    {
        if (!charPanel.activeInHierarchy)
        {
            charPanel.SetActive(true);
            inkCharStory.ChoosePathString(charName);

        }

        if (inkCharStory.canContinue)
        {
            string inkText = inkCharStory.Continue();
            inkText = inkText.Trim();
            charText.text = inkText;

        }
    }

    public void StartDialogue()
    {
        StartCoroutine(TextTimer());
    }

    IEnumerator TextTimer()
    {
        CharDialogue();
        yield return new WaitForSeconds(5f);
        Debug.Log("Next line call");
        if (inkCharStory.canContinue)
        {
            StartCoroutine(TextTimer());
        }
        else
        {
            charPanel.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
