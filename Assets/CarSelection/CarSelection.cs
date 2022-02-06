using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private Button prvBtn;
    [SerializeField] private  Button nxtBtn;
    [SerializeField] private  Button confirmBtn;

    private int currentCar;

    private void Awake()
    {
        SelectCar(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && (currentCar != 0))
        {
            prvBtn.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && (currentCar != transform.childCount - 1))
        {
            nxtBtn.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            confirmBtn.onClick.Invoke();
        }
    }

    private void SelectCar(int index)
    {
        prvBtn.interactable = (index != 0);
        nxtBtn.interactable = (index != transform.childCount - 1);
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void ChangeCar(int change)
    {

        currentCar += change;
        SelectCar(currentCar);


    }


    public void Confirm()
    {
        PlayerPrefs.SetInt("CarSelected", currentCar);
        int selectedTrackIndex = PlayerPrefs.GetInt("TrackSelected");
        if(selectedTrackIndex ==0)
        {
            SceneManager.LoadScene("Gebirgeland");
        }
        if(selectedTrackIndex == 1)
        {
            SceneManager.LoadScene("Secondland");
        }
        
    }
}
