using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyColor : MonoBehaviour
{
    [SerializeField] private Transform Left;
    [SerializeField] private Transform Top;
    [SerializeField] private Transform Right;
    [SerializeField] private Transform Bottom;

    private float timeLeft = 30f;
    private bool rand = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            rand = !rand;
            timeLeft = 30f;
        }

        if (rand)
        {
            Left.GetComponent<Renderer>().material.color = Color.red;
            Right.GetComponent<Renderer>().material.color = Color.red;
            Top.GetComponent<Renderer>().material.color = Color.green;
            Bottom.GetComponent<Renderer>().material.color = Color.green;
        }
        else 
        {
            Left.GetComponent<Renderer>().material.color = Color.green;
            Right.GetComponent<Renderer>().material.color = Color.green;
            Top.GetComponent<Renderer>().material.color = Color.red;
            Bottom.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
