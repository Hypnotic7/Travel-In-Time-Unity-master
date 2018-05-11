using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Video
{
    public class VideoController : MonoBehaviour
    {
        private void Start()
        {
            MovieTexture movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            movie.Play();

        }
    }
}
