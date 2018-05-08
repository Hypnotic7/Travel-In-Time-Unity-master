using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Interactables.Pictures
{
    public class PictureImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector2 _offset;
        public Picture Picture { get; set; }
        private PicturesManager _picturesManager;
        public int SlotNumber;
        private int currentSlot;

        void Start()
        {
            _picturesManager = GameObject.Find("PicturesPanel").GetComponent<PicturesManager>();

        }

        //Whenever the user has started dragging the image it gets the data related to the item
        public void OnBeginDrag(PointerEventData eventData)
        {
            currentSlot = SlotNumber;
            if (Picture.ID != -1)
            {
                _offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
                this.transform.position = eventData.position - _offset;
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
           
        }

        //While dragging the image it updates the position of the image depending on mouse position
        public void OnDrag(PointerEventData eventData)
        {
            if (Picture.ID != -1)
            {
                this.transform.position = eventData.position - _offset;
            }
        }


        //When the image is dropped it sets the position back from where it was dragged if not dropped on any of the slots else it drops it in the slot
        public void OnEndDrag(PointerEventData eventData)
        {
            this.transform.SetParent(_picturesManager.ImageSlots[SlotNumber].transform);
            this.transform.position = _picturesManager.ImageSlots[SlotNumber].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            if (!currentSlot.Equals(SlotNumber))
            {
                _picturesManager.Counter++;
            }
            
        }
    }
}
