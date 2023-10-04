using UnityEngine;

namespace BarberShopDemo
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]AudioSource ambientMusic;

        public void QuitGame()
        {
            Application.Quit();
        }

        public void ToggleAudio()
        {
            if(ambientMusic != null)
            {
                if (ambientMusic.isPlaying)
                {
                    ambientMusic.Pause();
                }
                else
                {
                    ambientMusic.Play();
                }
            }
            
        }
    }
}
