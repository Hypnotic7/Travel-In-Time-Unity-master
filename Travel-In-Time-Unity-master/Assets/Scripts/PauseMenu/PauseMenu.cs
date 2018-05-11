using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool GameIsPaused = false;
        public GameObject pauseMenuUI;
        public GameObject inventoryUI;
        public GameObject interactionUI;
        public GameObject tooltipUI;


        //Whenever Escape key is pressed it pauses or resumes the game
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }


        //Resume the game
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            inventoryUI.SetActive(true);
            interactionUI.SetActive(true);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }


        //Pause the game
        void Pause()
        {
            pauseMenuUI.SetActive(true);
            inventoryUI.SetActive(false);
            tooltipUI.SetActive(false);
            interactionUI.SetActive(false);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        //Loading main menu
        public void LoadMainMenu()
        {
            Time.timeScale = 1f;
            if (!GameplayChecker.EmptyFlaskPickedUp)
                GameObject.Find("Flask").SetActive(true);

            cleanGameplayChecker();

            if (GameplayChecker.CurrentTime.Contains("Past"))
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
            else
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 2);
            
            DontDestroyOnLoad(GameObject.Find("Audio(Clone)"));
        }


        //Cleans gameplay checker
        private void cleanGameplayChecker()
        {
            GameplayChecker.VinylPickUpEast = false;
            GameplayChecker.VinylPickUpNorthEast = false;
            GameplayChecker.VinylPickUpSouth = false;
            GameplayChecker.VinylPickUpWest = false;
            GameplayChecker.InvisibilityMode = false;
            GameplayChecker.PicturesPastPuzzleSolved = false;
            GameplayChecker.PicturesPresentPuzzleSolved = false;
            GameplayChecker.SafePuzzleSolved = false;
            GameplayChecker.PianoPuzzleSolved = false;
            GameplayChecker.CraftedInvisiblityFlask = false;
            GameplayChecker.GramophonePuzzle = false;
            GameplayChecker.AreDoorsOpen = false;
            GameplayChecker.EmptyFlaskPickedUp = false;
            GameplayChecker.FirstTimeOpened = false;
            GameplayChecker.PlayerHasTeleported = false;
        }

       
    }
}
