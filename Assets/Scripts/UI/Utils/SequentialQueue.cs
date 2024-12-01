using System;
using System.Collections.Generic;
using UnityEngine;
public struct Callbackable
{
    public Action Callback;
    public float Time;
    public Callbackable(Action callback, float time)
    {
        Callback = callback;
        this.Time = time;
    }
}

public class SequentialQueue : MonoBehaviour
{
    private float globalEndTime;
    private List<Callbackable> callbackables = new();

    public void Update()
    {
        globalEndTime = Mathf.Max(globalEndTime, Time.time);
        for (int i = 0; i < callbackables.Count; i++)
        {
            Callbackable callbackable = callbackables[i];
            if(callbackable.Time <= Time.time)
            {
                callbackable.Callback?.Invoke();
                callbackables.RemoveAt(i--); // Delete the current element (and adapt current index i)
            }
        }
    }

    public void Enqueue(Action callback, float blockingTime = 0f, float timeOffset = 0f)
    {
        // Compute starting time
        float startTime = Mathf.Max(globalEndTime + timeOffset, Time.time);
        // Adapt estimated end time for next callbacks
        this.globalEndTime = startTime + blockingTime;

        // Start immediatly or queue for later
        if(startTime == Time.time)
        {
            callback.Invoke();
        }
        else
        {
            this.callbackables.Add(new Callbackable(callback, startTime));
        }
    }

    public void Postpone(float blockingTime)
    {
        float availlableTime = Mathf.Max(this.globalEndTime, Time.time); 
        this.globalEndTime = availlableTime + blockingTime;
    }

    public void Complete()
    {
        for (int i = 0; i < callbackables.Count; i++)
        {
            callbackables[i].Callback?.Invoke();
        }
        globalEndTime = Time.time;
        callbackables.Clear();
    }
    public void Kill()
    {
        globalEndTime = Time.time;
        callbackables.Clear();
    }
}
