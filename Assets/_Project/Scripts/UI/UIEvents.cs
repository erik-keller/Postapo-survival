using System;
using UnityEngine;

namespace ProjectPostapo.UI
{
    public class UIEvents
    {
        public static Action<int, string, int> t_OnInventorySlotChanged;
        public static Action<string> t_OnShowCenteredText;
        public static Action t_OnHideCenteredText;
    }
}
