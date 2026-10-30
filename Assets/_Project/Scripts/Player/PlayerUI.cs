using ProjectPostapo.CreatureSystem;
using ProjectPostapo.InventorySystem;
using ProjectPostapo.ScriptableObjects;
using ProjectPostapo.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectPostapo.Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Creature playerCreature;

        [SerializeField] private GameObject centeredText;
        [SerializeField] private GameObject frames;
        [SerializeField] private GameObject framePrefab;

        private void Start()
        {
            Inventory inventory = playerCreature.Inventory;

            if (inventory == null)
            {
                Debug.LogWarning("inventory is null");
                return;
            }

            inventory.OnInventorySlotChanged.AddListener(UpdateSlot);

            UIEvents.t_OnShowCenteredText += ShowCenteredText;
            UIEvents.t_OnHideCenteredText += HideCenteredText;

            for (int i = 0; i < inventory.Capacity; i++)
            {
                Instantiate(framePrefab, frames.transform);
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

            GameObject frame = frames.transform.GetChild(index).gameObject;
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

            if (itemId == null || itemId == string.Empty || quantity <= 0)
            {
                ClearSlot(frameClass);
                return;
            }

            ItemSO item = ItemDatabase.Instance.GetItem(itemId);

            if (item == null)
            {
                Debug.LogWarning($"item is null");
                return;
            }

            frameClass.FrameImgObject.SetActive(true);
            frameClass.FrameImage.texture = item.Icon;

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
                Debug.LogWarning("centeredText GameObject is not assigned.");
                return;
            }

            centeredText.SetActive(true);
            centeredText.GetComponentInChildren<Text>().text = message;
        }
        public void HideCenteredText()
        {
            if (centeredText == null)
            {
                Debug.LogWarning("centeredText GameObject is not assigned.");
                return;
            }

            centeredText.SetActive(false);
            centeredText.GetComponentInChildren<Text>().text = string.Empty;
        }
    }
}