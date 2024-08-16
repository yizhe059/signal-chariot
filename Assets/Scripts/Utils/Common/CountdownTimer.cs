using UnityEngine.Events;

namespace Utils.Common
{
    public class CountdownTimer
    {
        private float duration;
        private float currentTime;
        private bool isRunning;

        public UnityEvent OnTimerComplete;

        public CountdownTimer(float duration)
        {
            this.duration = duration;
            this.currentTime = duration;
            this.isRunning = false;
            OnTimerComplete = new();
        }

        public void StartTimer()
        {
            isRunning = true;
        }

        public void StopTimer()
        {
            isRunning = false;
        }

        public void ResetTimer()
        {
            currentTime = duration;
            isRunning = false;
        }

        public void Update(float deltaTime)
        {
            if (!isRunning) return;

            currentTime -= deltaTime;

            if (currentTime <= 0f)
            {
                isRunning = false;
                OnTimerComplete?.Invoke();
            }
        }

        public float GetRemainingTime()
        {
            return currentTime;
        }

        public bool IsRunning()
        {
            return isRunning;
        }
    }
}