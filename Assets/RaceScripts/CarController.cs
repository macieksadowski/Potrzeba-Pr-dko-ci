using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentBreakForce;
    private float currentSteerAngle;
    private float currentAccForce;
    private bool isBreaking;
    private static bool gameStarted = false;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float accelerationForce;
    [SerializeField] private Text accText;
    [SerializeField] private Text coinCnt;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;


    public int Coins = 0;


    public void Start()
    {
        accText.text = "";
    }
    public static void StartGame()
    {
        gameStarted = true;
    }

    public static void EndGame()
    {
        gameStarted = false;
        
    }

    private void FixedUpdate()
    {
        if(gameStarted)
        {
            GetInput();
            Accelerate();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
        else
        {
            currentAccForce = .0f;
            isBreaking = true;
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
       
    }

    private void Accelerate()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentAccForce = accelerationForce;
            accText.text = "Turbo!";

        } else
        {
            currentAccForce = 0.0f;
            accText.text = "";
        }
        
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        Debug.Log("L = " + frontLeftWheelCollider.motorTorque);
        Debug.Log("R = " + frontRightWheelCollider.motorTorque);

        frontLeftWheelCollider.motorTorque = verticalInput * motorForce + currentAccForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce + currentAccForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        
        ApplyBreaking();
        
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        rearRightWheelCollider.brakeTorque = currentBreakForce;
        rearLeftWheelCollider.brakeTorque = currentBreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);

    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        float tmp = rot.x;
        //rot.y -= (float)Math.PI / 2;
        //rot.x = rot.z;
        //rot.z = tmp;
        wheelTransform.rotation = rot;
    }

    private void OnTriggerEnter(Collider coinCollider)
    {
        if (coinCollider.gameObject.tag == "Coin")
        {
            Coins++;
            coinCollider.gameObject.SetActive(false);
            coinCnt.text = "Coins: " + Coins;
        }
    }
}
