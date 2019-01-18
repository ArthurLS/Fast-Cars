using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBehaviour : MonoBehaviour
{

    public float engineRevs;
    public float exhaustRate;

    ParticleSystem exhaust;

    // Use this for initialization

    void Start()
    {
        exhaust = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.EmissionModule emis = exhaust.emission;
        emis.rateOverTime = 10 + engineRevs * exhaustRate;
    }
}

