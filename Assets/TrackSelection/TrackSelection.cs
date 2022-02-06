using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrackSelection : MonoBehaviour
{
    [SerializeField] private Button prvBtn;
    [SerializeField] private  Button nxtBtn;
    [SerializeField] private  Button confirmBtn;

    private int currentTrack;

    private void Awake()
    {
        SelectTrack(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && (currentTrack != 0))
        {
            prvBtn.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && (currentTrack != transform.childCount - 1))
        {
            nxtBtn.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            confirmBtn.onClick.Invoke();
        }
    }

    private void SelectTrack(int index)
    {
        prvBtn.interactable = (index != 0);
        nxtBtn.interactable = (index != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void ChangeTrack(int change)
    {

        currentTrack += change;
        SelectTrack(currentTrack);


    }


    public void Confirm()
    {
        if(currentTrack == 2)
        {
            PlayerPrefs.SetInt("CarSelected", 0);
            SceneManager.LoadScene("Darkland");
        }
        else
        {
            PlayerPrefs.SetInt("TrackSelected",currentTrack);
            SceneManager.LoadScene("CarSelection");
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
