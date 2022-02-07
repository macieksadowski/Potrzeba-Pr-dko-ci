using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour
{

    [SerializeField] private Text lapsText;
    [SerializeField] private Text timeText;
    [SerializeField] private Text counterText;
    [SerializeField] private Text endTimeText;
    [SerializeField] private int lapsNumber;
    [SerializeField] private RectTransform endGamePanel;

    [SerializeField] private AudioSource counterSound;
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource endSound;
    [SerializeField] private AudioSource backgroundMusic;


    private Vector3 endGamePanelScale;

    private float startTime;
    private float elapsedTime;
    private int curLap = 0;


    private void Start()
    {
        endGamePanelScale = endGamePanel.localScale;
        curLap = 0;
        endGamePanel.localScale = new Vector3(0, 0, 0);
        lapsText.text = curLap + "/" + lapsNumber;
        StartCoroutine(Countdown(3));
        
    }




    private void OnTriggerEnter(Collider other)
    {
        if(curLap == 0)
        {
            startTime = Time.time;
        }
        curLap++;
        lapsText.text = curLap + "/" + lapsNumber;

        if(curLap > lapsNumber)
        {
            CarController.EndGame();
            endSound.Play();
            endTimeText.text += timeText.text;
            endGamePanel.localScale = endGamePanelScale;
        }
    }

    private void Update()
    {
        if(curLap > 0 && curLap <= lapsNumber)
        {
            elapsedTime = Time.time - startTime;
            int minutes = (int)elapsedTime / 60;
            int seconds = (int)elapsedTime - 60 * minutes;
            int milliseconds = (int)(1000* (elapsedTime - minutes * 60 - seconds) / 10);
            timeText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            
        }


        
    }

   

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;

        while(count > 0 )
        {
            counterText.text = count.ToString();
            counterSound.Play();
            yield return new WaitForSeconds(1);
            count--;
        }

        CarController.StartGame();
        counterText.fontSize = (int)(counterText.fontSize * 0.8);
        counterText.text = "START";
        startSound.Play();
        yield return new WaitForSeconds(1);
        counterText.text = "";
        backgroundMusic.Play();
    }
}
