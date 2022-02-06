using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Text speedText;
    [SerializeField] private Transform cars;
    [SerializeField] private float distance = 6.4f;
    [SerializeField] private float height = 1.4f;
    [SerializeField] private float rotationDamping = 3.0f;
    [SerializeField] private float heightDamping = 2.0f;
    [SerializeField] private float zoomRatio = 0.5f;
    [SerializeField] private float defaultFOV = 60f;

    private Vector3 rotationVector;
    private Transform car;


    private void Start()
    {
        int selectedCarIndex = PlayerPrefs.GetInt("CarSelected");
        car = cars.GetChild(selectedCarIndex).transform;
        
        for (int i = 0; i < cars.childCount; i++)
        {
            if (i == selectedCarIndex)
                continue;
            GameObject.Destroy(cars.GetChild(i).gameObject);
        }
        car.position = cars.position;
    }


    private void LateUpdate()
    {
        float destAngle = rotationVector.y;
        float destHeight = car.position.y + height;
        float curAngle = transform.eulerAngles.y;
        float curHeight = transform.position.y;

        curAngle = Mathf.LerpAngle(curAngle, destAngle, rotationDamping * Time.deltaTime);
        curHeight = Mathf.Lerp(curHeight, destHeight, heightDamping * Time.deltaTime);

        Quaternion curRotation = Quaternion.Euler(0, curAngle, 0);
        transform.position = car.position;
        transform.position -= curRotation * Vector3.forward * distance;
        Vector3 tmp = transform.position;
        tmp.y = curHeight;
        transform.position = tmp;
        transform.LookAt(car);

    }

    void FixedUpdate()
    {
        Vector3 localVelocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
        if (localVelocity.z < -0.1f)
        {
            Vector3 tmp = rotationVector;
            tmp.y = car.eulerAngles.y + 180;
            rotationVector = tmp;
        }
        else
        {
            Vector3 tmp = rotationVector;
            tmp.y = car.eulerAngles.y;
            rotationVector = tmp;
        }
        float acc = car.GetComponent<Rigidbody>().velocity.magnitude;
        GetComponent<Camera>().fieldOfView = defaultFOV + acc * zoomRatio * Time.deltaTime;

        speedText.text = ((int)(acc * 3.6)).ToString()  + " km/h";
    }
}
