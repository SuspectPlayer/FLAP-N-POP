﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class SwipeManager : MonoBehaviour
{
    public float swipeThreshold = 50f;
    public float swipeTimeThreshold = 0.3f;
    public float clickTimeThreshold = 0.2f;

    public UnityEvent OnScreenPress;
    public UnityEvent OnSwipeLeft;
    public UnityEvent OnSwipeRight;
    public UnityEvent OnSwipeUp;
    public UnityEvent OnSwipeDown;

    private Vector2 fingerDown;
    private DateTime fingerDownTime;
    private Vector2 fingerUp;
    private DateTime fingerUpTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.fingerDown = Input.mousePosition;
            this.fingerUp = Input.mousePosition;
            this.fingerDownTime = DateTime.Now;
        }
        if (Input.GetMouseButtonUp(0))
        {
            this.fingerDown = Input.mousePosition;
            this.fingerUpTime = DateTime.Now;
            this.CheckClick();
            this.CheckSwipe();     
        }
    }

    private void CheckSwipe()
    {
        float duration = (float)this.fingerUpTime.Subtract(this.fingerDownTime).TotalSeconds;
        if (duration > this.swipeTimeThreshold) return;

        float deltaX = this.fingerDown.x - this.fingerUp.x;
        float deltaY = this.fingerDown.y - this.fingerUp.y;

        if (Mathf.Abs(deltaX) > this.swipeThreshold)
        {
            if (deltaX > 0 && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                this.OnSwipeRight.Invoke();
            }
            else if (deltaX < 0 && Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                this.OnSwipeLeft.Invoke();
            }
        }
        
        if (Mathf.Abs(deltaY) > this.swipeThreshold)
        {
            if (deltaY > 0 && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                this.OnSwipeUp.Invoke();
            }
            else if (deltaY < 0 && Mathf.Abs(deltaY) > Mathf.Abs(deltaX))
            {
                this.OnSwipeDown.Invoke();
            }
        }
        this.fingerUp = this.fingerDown;
    }

    private void CheckClick()
    {
        float duration = (float)this.fingerUpTime.Subtract(this.fingerDownTime).TotalSeconds;
        if (duration > this.clickTimeThreshold) return;
        this.OnScreenPress.Invoke();
    }
}
