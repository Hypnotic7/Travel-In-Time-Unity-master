using UnityEngine;
using Wilberforce;

namespace Assets.Scripts.Interactables.Craft
{
    public class CheckForVisibility : MonoBehaviour
    {
        //Checks is the visibility flask activated, if it is it activates the color blindness
        void Start()
        {
            if (PlayerPrefs.GetInt("Invisibility").Equals(1))
            {
                 var colorBlind = GameObject.Find("Main Camera(Clone)").GetComponent<Colorblind>();
               
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(colorBlind.enabled);
                }
            }
        }
    }
}
