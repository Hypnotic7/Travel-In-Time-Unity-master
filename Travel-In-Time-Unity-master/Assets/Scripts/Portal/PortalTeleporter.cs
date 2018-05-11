using UnityEngine;

namespace Assets.Scripts.Portal
{
    public class PortalTeleporter : MonoBehaviour
    {
        public Transform Destination;

        public string TagList = "|Player|";

        public void OnTriggerEnter(Collider other)
        {
            if (!IsCoolingDown && !GameplayChecker.PlayerHasTeleported)
            {
                if (TagList.Contains(string.Format("|{0}|", other.tag)))
                {
                    other.transform.position = Destination.transform.position;
                    other.transform.rotation = Destination.transform.rotation;
                    IsCoolingDown = true;
                    timeStamp = Time.time + CooldownInSeconds;
                    GameplayChecker.PlayerHasTeleported = true;
                }
            }

        }

        public bool IsCoolingDown = false;

        public float timeStamp;

        public float CooldownInSeconds = 3;

        // Update is called once per frame
        void Update()
        {
            if (IsCoolingDown)
            {
                if (timeStamp <= Time.time)
                {
                    IsCoolingDown = false;
                    GameplayChecker.PlayerHasTeleported = false;
                }
            }


            //public Transform player;
            //public Transform receiver;
            //public bool playerIsOverlapping = false;

            //public bool IsCoolingDown = false;

            //public float timeStamp;

            //public float CooldownInSeconds = 3;
            //// Update is called once per frame
            //void Update()
            //{
            //    if (IsCoolingDown)
            //    {
            //        if (timeStamp <= Time.time)
            //        {
            //            IsCoolingDown = false;
            //            GameplayChecker.PlayerHasTeleported = false;
            //        }
            //    }


            //}

            //void OnTriggerEnter(Collider collision)
            //{
            //    if (collision.gameObject.tag == "Player")
            //    {
            //        if (!GameplayChecker.PlayerHasTeleported)
            //        {
            //            playerIsOverlapping = true;
            //            if (playerIsOverlapping)
            //            {
            //                Vector3 portalToPlayer = player.position - transform.position;
            //                Debug.Log(receiver.gameObject.name);
            //                float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            //                if (dotProduct > 0f)
            //                {
            //                    float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
            //                    rotationDiff += 180;
            //                    player.Rotate(Vector3.up, rotationDiff);

            //                    Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
            //                    positionOffset.x += 5f;
            //                    player.position = receiver.position + positionOffset;
            //                }

            //                GameplayChecker.PlayerHasTeleported = true;
            //                IsCoolingDown = true;
            //                timeStamp = Time.time + CooldownInSeconds;
            //            }



            //        }
            //        playerIsOverlapping = false;
            //    }
            //}

            //void OnTriggerExit(Collider collision)
            //{
            //    if (collision.gameObject.tag == "Player")
            //    {
            //        playerIsOverlapping = false;
            //    }
            //}
        }
    }
}

