using ProjectPostapo.InventorySystem;
using ProjectPostapo.UI;
using System;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPostapo.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private GameObject centeredText;
        [SerializeField] private GameObject Frames;
        [SerializeField] private Inventory inventory;
        [SerializeField] private ItemDatabase itemDatabase;

        private void Start()
        {
            UIEvents.t_OnInventorySlotChanged += UpdateSlot;
            UIEvents.t_OnShowCenteredText += ShowCenteredText;
            UIEvents.t_OnHideCenteredText += HideCenteredText;

            for(int i = 0; i < inventory.Capacity; i++)
            {
                UpdateSlot(i, inventory.GetSlotItemId(i), inventory.GetSlotQuantity(i));
            }
        }

        public void UpdateSlot(int index, string itemId, int quantity)
        {
            if (index < 0)
            {
                Debug.LogWarning($"index: {index}");
                return;
            }

            GameObject frame = Frames.transform.GetChild(index).gameObject;
            if (frame == null)
            {
                Debug.LogWarning($"Frame: {frame} is null");
                return;
            }
            if (!frame.TryGetComponent<Frame>(out Frame frameClass))
            {
                Debug.LogWarning($"Frame class: {frameClass} not found");
                return;
            }

            if (itemId == null || quantity <= 0)
            {
                ClearSlot(frameClass);
            }

            ItemSO item = itemDatabase.Instance.GetItem(itemId);
            if (item == null)
            {
                Debug.LogWarning($"{item} is null");
            }

            frameClass.FrameImgObject.SetActive(true);
            frameClass.FrameImage.texture = item.icon;

            frameClass.FrameText.text = quantity.ToString();
            frameClass.FrameTextObject.SetActive(true);
        }
        private void ClearSlot(Frame frameClass)
        {
            frameClass.FrameImgObject.SetActive(false);
            frameClass.FrameTextObject.SetActive(false);
        }

        public void ShowCenteredText(string message)
        {
            if (centeredText == null)
            {
                Debug.LogWarning("Centered Text GameObject is not assigned.");
            }
            centeredText.SetActive(true);
            centeredText.GetComponentInChildren<Text>().text = message;
        }
        public void HideCenteredText()
        {
            if (centeredText == null)
            {
                Debug.LogWarning("Centered Text GameObject is not assigned.");
            }
            centeredText.SetActive(false);
        }
    }
}