using System;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPostapo.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject centeredText;
        [SerializeField]
        private GameObject Slots;

        public static Action<InventorySlot[]> OnInventorySlotChanged;
        public static Action<string> OnShowCenteredText;
        public static Action OnHideCenteredText;

        private void Start()
        {
            OnInventorySlotChanged += UpdateSlots;
            OnShowCenteredText += ShowCenteredText;
            OnHideCenteredText += HideCenteredText;
        }

        public void ShowCenteredText(string message)
        {
            try
            {
                if (centeredText == null)
                {
                    throw new System.NullReferenceException("Centered Text GameObject is not assigned.");
                }
                centeredText.SetActive(true);
                centeredText.GetComponentInChildren<Text>().text = message;

            }
            catch (System.Exception ex)
            {
                Debug.LogError($"An error occurred while showing centered text: {ex.Message}");
            }
        }
        public void HideCenteredText()
        {
            try
            {
                if (centeredText == null)
                {
                    throw new System.NullReferenceException("Centered Text GameObject is not assigned.");
                }
                centeredText.SetActive(false);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"An error occurred while hiding centered text: {ex.Message}");
            }
        }

        public void UpdateSlots(InventorySlot[] items)
        {
            try
            {
                if (Slots == null)
                {
                    throw new System.NullReferenceException("Slots GameObject is not assigned.");
                }
                for (int i = 0; i < Slots.transform.childCount; i++)
                {
                    GameObject slot = Slots.transform.GetChild(i).gameObject;

                    GameObject slotImgObject = slot.transform.GetChild(0).gameObject;
                    GameObject slotTextObject = slot.transform.GetChild(1).gameObject;

                    RawImage slotImage = slotImgObject.GetComponent<RawImage>();
                    Text slotText = slotTextObject.GetComponent<Text>();

                    if (!items[i].IsEmpty())
                    {
                        slotImgObject.SetActive(true);
                        slotImage.texture = items[i].Item.icon;
                        slotImage.color = Color.white;

                        if (items[i].Quantity == 0)
                        {
                            slotText.text = "0";
                            slotTextObject.SetActive(false);
                        }
                        else
                        {
                            slotText.text = items[i].Quantity.ToString();
                            slotTextObject.SetActive(true);
                        }
                    }
                    else
                    {
                        slotText.text = "0";
                        slotTextObject.SetActive(false);

                        slotImgObject.SetActive(false);
                        slotImage.texture = null;
                        slotImage.color = Color.clear;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"An error occurred while updating slots: {ex.Message}");
            }
        }
    }
}