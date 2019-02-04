using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedControl : MonoBehaviour {
    Animator m_Animator;
    public float speed = 2.0f;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        m_Animator.speed = speed;
    }
}
