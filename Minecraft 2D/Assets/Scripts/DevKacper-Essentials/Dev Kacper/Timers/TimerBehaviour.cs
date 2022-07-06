using System;
using UnityEngine;

namespace DevKacper.Timers
{
    public class TimerBehaviour : MonoBehaviour
    {
        private float time;
        public Action OnTimerEnd;

        public void SetupTimer(float time, Action OnTimerTimeOut)
        {
            this.time = time;
            this.OnTimerEnd = OnTimerTimeOut;
        }

        private void Update()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                DestroyTimer();
            }
        }

        private void DestroyTimer()
        {
            OnTimerEnd?.Invoke();
            Destroy(this);
        }

        public float GetTime()
        {
            return time;
        }
    }
}

