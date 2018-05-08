
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wilberforce;

namespace Assets.Scripts
{
    //This class is in charge of all GameObjects that are created during the startup of the game
    public class GameManager : MonoBehaviour
    {
        public GameObject GUI;
        public GameObject Character;
        public GameObject MainCamera;
        public GameObject EventSystem;
        public GameObject AudioSystem;
        public GameObject GameplayCheckerObj;

        private GameObject Spiral;
        private IList<GameObject> gameObjectsList;


        // Use this for initialization
        void Start()
        {
            gameObjectsList = new List<GameObject>()
            {
                GUI,
                Character,
                MainCamera,
                EventSystem,
                AudioSystem,
                GameplayCheckerObj
            };

            gameObjectsList = InstanciateIfCannotFind(gameObjectsList).ToList();
            SetSmoothFollowTarget(gameObjectsList[1].transform);
            Spiral = GetSpiral();
            SpiralActivate(false);
            CheckAreTheVinylsAreActive();
        }

        //Check are the vinyls active if not activate them
        private void CheckAreTheVinylsAreActive()
        {
            if (GameplayChecker.InvisibilityMode)
            {
                var colorBlind = gameObjectsList[2].GetComponent<Colorblind>();
                if (GameplayChecker.CurrentTime.Contains("Past"))
                {
                    var vinyls = GameObject.Find("Vinyls").transform;
                    for (int i = 0; i < vinyls.childCount; i++)
                    {
                        vinyls.GetChild(i).gameObject.SetActive(colorBlind.enabled);
                    }
                }
            }
        }

        //Load the scene by passing scene name eg. Past or Present
        public void LoadScene(string sceneName)
        {
            SpiralActivate(true);
            StartCoroutine(Rotate(3));
            SceneManager.LoadSceneAsync(sceneName);
            foreach (var currentGameObject in gameObjectsList)
            {
                DontDestroyOnLoad(currentGameObject);
            }
        }

        //initialize if the game objects are not found
        private IEnumerable<GameObject> InstanciateIfCannotFind(IEnumerable<GameObject> gameObjects)
        {
            IList<GameObject> tempGameObjects = new List<GameObject>();

            foreach (var currentGameObject in gameObjects)
            {
                var tempGameObj = GameObject.Find($"{currentGameObject.name}(Clone)");
                tempGameObjects.Add(tempGameObj ?? Instantiate(currentGameObject));
            }
            return tempGameObjects;
        }
        
        //Activate Spiral
        private void SpiralActivate(bool condition)
        {
            Spiral.SetActive(condition);
        }


        //Get Spiral game object
        private GameObject GetSpiral()
        {
            return gameObjectsList[0].transform.GetChild(3).gameObject;
        }


        //Rotate the spiral
        IEnumerator Rotate(float duration)
        {
            float startRotation = transform.eulerAngles.z;
            float endRotation = startRotation + 360.0f;
            float t = 0.0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                Spiral.transform.GetChild(0).transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, zRotation);
                yield return null;
            }
        }


        //Set a smooth follow on camera to follow target e.g. character
        private void SetSmoothFollowTarget(Transform target)
        {
            if (gameObjectsList != null && gameObjectsList[2].GetComponent<CameraController>().target != target)
                gameObjectsList[2].GetComponent<CameraController>().target = target;
        }
    }
}
