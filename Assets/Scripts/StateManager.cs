using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateManager : MonoBehaviour
{
    public static StateManager inst;
    public bool gamePlaying;

    [SerializeField] AnimationCurve customerTarget;
    public int happyCustomers;
    public TMP_Text customerText;
    public enum eventCalls
    {
        InitGame,
        StartGame,
        EndGame,
        PauseGame,
        UnPauseGame,
        StartDay
    }
    public GameObject startPanel;
    public GameObject endPanel;
    public GameObject pausePanel;
    public GameObject deathPanel;
    public GameObject reportPanel;
    //public GameObject questPanel;
    public Animator fadeAnim;

    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        GameStateEvents.inst.OnGameInit += InitGame;
        GameStateEvents.inst.OnGameStart += GameStarted;
        GameStateEvents.inst.OnGameEnd += GameEnded;
        GameStateEvents.inst.OnStartDay += DayStart;
        GameStateEvents.inst.OnEndDay += DayEnd;
        StartCoroutine("StartDelay");
    }
   
    void Update()
    {
        
        if (gamePlaying)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
                GameStateEvents.inst.Pause();
        }
        
    }

    void InitGame()
    {
        //Debug.Log("Hell Ya");
        endPanel.SetActive(false);
        deathPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    void GameStarted()
    {
        Debug.Log("Yeah");
        GameStateEvents.inst.OnPlayerDied += PlayerDied;
        GameStateEvents.inst.OnPause += Pause;
        GameStateEvents.inst.OnUnPause += UnPause;
        //fadeAnim.SetTrigger("Fade");
        startPanel.SetActive(false);
        gamePlaying = true;
        GameStateEvents.inst.StartDay();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void GameEnded()
    {
        GameStateEvents.inst.OnPlayerDied -= PlayerDied;
        GameStateEvents.inst.OnPause -= Pause;
        GameStateEvents.inst.OnUnPause -= UnPause;
        Cursor.lockState = CursorLockMode.Confined;
        endPanel.SetActive(true);
        gamePlaying = false;
    }

    void PlayerDied()
    {
        GameStateEvents.inst.OnPlayerDied -= PlayerDied;
        GameStateEvents.inst.OnPause -= Pause;
        GameStateEvents.inst.OnUnPause -= UnPause;
        Cursor.lockState = CursorLockMode.Confined;
        deathPanel.SetActive(true);
        gamePlaying = false;
    }


    void DayStart()
    {
        //fadeAnim.SetTrigger("Fade");
        reportPanel.SetActive(false);
        happyCustomers = 0;
        UpdateUI();
        //questPanel.SetActive(true);
    }


    public void UpdateUI()
    {
        customerText.text = happyCustomers + "/" + Mathf.FloorToInt(customerTarget.Evaluate(GameTimer.inst.day));
    }
    void DayEnd()
    {
        //fadeAnim.SetTrigger("Fade");
        Cursor.lockState = CursorLockMode.Confined;
        //questPanel.SetActive(false);
        if (happyCustomers < Mathf.FloorToInt(customerTarget.Evaluate(GameTimer.inst.day)))
        {
            GameStateEvents.inst.PlayerDied();
        }
        else if(GameTimer.inst.day >= 31)
        {
            GameStateEvents.inst.GameEnd();
        }
        else
        {
            reportPanel.SetActive(true);
        }
    }

    void Pause()
    {
        GameStateEvents.inst.OnPause -= Pause;
        Cursor.lockState = CursorLockMode.Confined;
        gamePlaying = false;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void UnPause()
    {
        GameStateEvents.inst.OnPause += Pause;
        Cursor.lockState = CursorLockMode.Locked;
        gamePlaying = true;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void BuyQuest()
    {

    }

    public void EventCall(int i)
    {
        eventCalls c = (eventCalls)i;
        if (c == eventCalls.InitGame)
            GameStateEvents.inst.GameInit();
        else if (c == eventCalls.StartGame)
            GameStateEvents.inst.GameStart();
        else if (c == eventCalls.EndGame)
            GameStateEvents.inst.GameEnd();
        else if (c == eventCalls.PauseGame)
            GameStateEvents.inst.Pause();
        else if (c == eventCalls.UnPauseGame)
            GameStateEvents.inst.UnPause();
        else if (c == eventCalls.StartDay)
            GameStateEvents.inst.StartDay();
    }

    IEnumerator StartDelay()
    {
        yield return null;
        GameStateEvents.inst.GameInit();
    }
}
