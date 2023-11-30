using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager tMInst;

    int tutorialCount = 0;
    bool activeTut;
    bool shownFirstTime;
    public List<string> tutorialTexts = new List<string>();
    public List<int> dayTrigger = new List<int>();
    public List<int> hourTrigger = new List<int>();
    public List<int> minuteTrigger = new List<int>();
    public TMP_Text tutorialText;
    public GameObject tutPanel;
    Animator anim;

    void Start()
    {
        tMInst = this;
        anim = tutPanel.GetComponent<Animator>();
    }

    void Update()
    {
        if (GameTimer.inst.day == dayTrigger[tutorialCount] && GameTimer.inst.hours == hourTrigger[tutorialCount] && GameTimer.inst.minutes == minuteTrigger[tutorialCount])
        {
            if (!activeTut)
            {
                StartCoroutine(ShowTutorial(false));
            }
        }

        if (Input.GetKeyDown(KeyCode.T) && shownFirstTime == true && activeTut == false)
        {
            ShowTutorial(true);
        }
    }


    IEnumerator ShowTutorial(bool repeatTut)
    {
        activeTut = true;

        if (repeatTut)
        {
            for (int i = 0; i < tutorialTexts.Count; i++)
            {
                tutorialText.text = tutorialTexts[i];
                anim.SetTrigger("Slide In");
                yield return new WaitForSeconds(5f);
                anim.SetTrigger("Slide Out");
            }
        }
        else
        {
            tutorialText.text = tutorialTexts[tutorialCount];
            tutorialCount++;
            anim.SetTrigger("Slide In");
            yield return new WaitForSeconds(5f);
            anim.SetTrigger("Slide Out");
            
            if (tutorialCount == tutorialTexts.Count)
            {
                shownFirstTime = true;
            }
        }

        activeTut = false;
    }

}
