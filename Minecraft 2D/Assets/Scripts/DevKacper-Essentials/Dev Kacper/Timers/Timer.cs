using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DevKacper.Timers
{
    public class Timer
    {
        private float time;
        public Action OnTimerEnd;

        public void SetupTimer(float time, Action OnTimerTimeOut)
        {
            this.time = time;
            this.OnTimerEnd = OnTimerTimeOut;
        }

        private void Update(float deltaTime)
        {
            if (time > 0)
            {
                time -= deltaTime;
            }
            else
            {
                TimerInvoke();
            }
        }

        private void TimerInvoke()
        {
            OnTimerEnd?.Invoke();
        }

        public float GetTime()
        {
            return time;
        }
    }
}