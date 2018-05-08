using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject audio;

    //Destroys all the game objects that are being instanciated during the start game.
    void Start()
    {
        if(!GameObject.Find("Audio(Clone)"))
            Instantiate(audio);
        Destroy(GameObject.Find("GameplayChecker(Clone)"));
        Destroy(GameObject.Find("GUI(Clone)"));
        Destroy(GameObject.Find("Main Camera(Clone)"));
        Destroy(GameObject.Find("Character(Clone)"));
        
    }

    //Plays the game
    public void PlayGame()
    {

    }


    //Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
