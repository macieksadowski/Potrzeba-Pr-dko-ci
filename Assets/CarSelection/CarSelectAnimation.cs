using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelectAnimation : MonoBehaviour
{

    [SerializeField] private Vector3 finPos;
    private Vector3 initPos;


    void Awake()
    {
        initPos = transform.position;
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, finPos, 0.1f);
    }

    void OnDisable()
    {
        transform.position = initPos;
    }
}
