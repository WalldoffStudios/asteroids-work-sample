using System;
using System.Collections;
using UnityEngine;

namespace Asteroids.Utilities
{
    public interface ITimerAction
    {
        void PerformActionAfterTimer(float delay, Action action);
    }

    public class TimerUtilities : MonoBehaviour, ITimerAction
    {
        // Performs an action after a specified delay
        public void PerformActionAfterTimer(float delay, Action action)
        {
            StartCoroutine(ExecuteAfterDelay(delay, action));
        }

        private IEnumerator ExecuteAfterDelay(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}