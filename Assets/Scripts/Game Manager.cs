using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private Timer timer;
        [SerializeField] private int totalKeys;
        [SerializeField] TextMeshProUGUI keyText; // UI displaying keys remaining
        [SerializeField] private GameObject winText; // UI displaying keys remaining
        [SerializeField] private Player player;
        [SerializeField] private Door door;

        private int keysCollected = 0;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("Two Instances ! !");
            }
            DontDestroyOnLoad(gameObject);
        }

        public void CollectKey()
        {
            keysCollected++;

            UpdateUI();

            if (keysCollected >= totalKeys)
            {
                Debug.Log("All keys collected!");
                // trigger door unlock or win condition
                door.Unlock();
            }
        }

        // Gives number of keys remaining
        public int KeysRemaining()
        {
            return totalKeys - keysCollected;
        }

        // Updates Key UI
        private void UpdateUI()
        {
            keyText.text = string.Format("Keys Remaining: {0}", KeysRemaining());
            if (keysCollected >= totalKeys)
            {
                keyText.text = "Get to the door!";
            }
        }

        public void Win()
        {
            timer.StopTimer();
            winText.SetActive(true);
            player.enabled = false;
        }
    }
}
