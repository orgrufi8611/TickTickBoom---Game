using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombTickingScript : MonoBehaviour
{
    [SerializeField] int timeToStop;
    [SerializeField] int timerSpeed;
    [SerializeField] int time;
    [SerializeField] TextMeshProUGUI timerIndicator,ShowTimeToStop,ShowTimerSpeed,msg;
    [SerializeField] GameObject bombUI;
    [SerializeField] GameObject bombButton;
    [SerializeField] Animator animator;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] AudioClip tick,ding,dingEnd,door;
    AudioSource aS;
    bool timerStart;
    bool deffused;
    bool start;
    public int score;
    bool interactable;
    float timespent;
    float timerDelta;
    // Start is called before the first frame update
    void Start()
    {
        aS = GetComponent<AudioSource>();
        bombUI.SetActive(false);
        timeToStop = Random.Range(1, 100);
        ShowTimeToStop.text = timeToStop.ToString();
        timerSpeed = Random.Range(3, 7);
        ShowTimerSpeed.text = timerSpeed.ToString();
        timerDelta = (float)timerSpeed / 100;
        msg.text = "Press and hold\nSpace to start defussing\nrelease space on the correct time";
        time = 0;
        deffused = false;
        timerStart = false;
        interactable = false;
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable)
        {
            timerIndicator.text = time.ToString();
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (!timerStart)
                {
                    timerStart = true;
                    StartCoroutine(StartTimer());
                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                timerStart = true;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && start || time == 100)
            {
                timerStart = false;
                deffused = true;
                StopCoroutine(StartTimer());
                CancelInvoke();
            }
            if(deffused )
            {
                score = Mathf.Clamp(100 - Mathf.Abs(time - timeToStop),0,100);
                msg.text = "Deffused with\n" + score.ToString() + "points";
                gameLogic.Deffused(score);
                interactable = false;
                animator.SetTrigger("Open");
                aS.PlayOneShot(door);
                Debug.Log("the time is" + time.ToString());
                Debug.Log("the actual time to finish was: " + timespent.ToString());
            }
        }
    }

    IEnumerator StartTimer()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        msg.text = "Start In";
        yield return delay;
        msg.text = "3...";
        aS.PlayOneShot(ding);
        yield return delay;
        msg.text = "2...";
        aS.PlayOneShot(ding);
        yield return delay;
        msg.text = "1...";
        aS.PlayOneShot(ding);
        yield return delay;
        msg.text = "Start";
        aS.PlayOneShot(dingEnd);
        yield return delay;
        start = true;
        msg.text = "";
        time = 0;
        timespent = 0;
        InvokeRepeating(nameof(Timer), 0.5f, timerDelta);
    }

    void Timer()
    {
        aS.PlayOneShot(tick);
        time++;
        timerIndicator.text = time.ToString();
    }

    public void Interact()
    {
        interactable = true;
        bombButton.SetActive(false);
        bombUI.SetActive(true);
        gameLogic.Deffusing();
    }
}
