using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Data;
using UnityEngine.SceneManagement;

namespace game
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float timeRemaining; // Starting amount of time
        [SerializeField] TextMeshProUGUI timeText; // UI displaying time remaining

        private bool timerIsRunning = false;

        private void Start()
        {
            timerIsRunning = true; // Starts timer
        }

        // Update is called once per frame
        private void Update()
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime; // Decrease time
                    DisplayTime(timeRemaining);
                }
                else
                {
                    Debug.Log("Time has run out!");
                    timeRemaining = 0;
                    StopTimer();

                    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reset scene
                }
            }
        }

        public void StopTimer()
        {
            timerIsRunning = false; // Stops timer
        }

        // Displays text
        private void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        // Used for hourglass pickup (that may or may not exist)
        private void AddTime(float extraTime)
        {

        }
    }
}
