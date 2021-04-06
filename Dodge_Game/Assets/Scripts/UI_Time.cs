using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Time : MonoBehaviour
{
    public Text timeText;
    private float timer = 0f;
    private int secondTimer = 0;
    private bool isPlaying = true;

    public void OffTimer()
    {
        isPlaying = false;
    }

    public float getPlayingTime()
    {
        return timer;
    }
    
    private void Awake()
    {
        timeText = GetComponent<Text>();
    }

    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
            if (Mathf.Floor(timer) > secondTimer)
            {
                secondTimer++;
                timeText.text = "버틴 시간 : " + secondTimer + "초";
            }
        }
    }
}
