using System;
using UnityEngine;

namespace game
{
    public class Door : MonoBehaviour
    {
        private bool unlocked = false;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Player collided with " + this);
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Player is detected");
                if (unlocked)
                {
                    Debug.Log("The player got to the unlocked door");
                    GameManager.Instance.Win();
                }
            }
        }

        public void Unlock()
        {
            Debug.Log("Door is unlocked");
            unlocked = true;
        }
    }
}
