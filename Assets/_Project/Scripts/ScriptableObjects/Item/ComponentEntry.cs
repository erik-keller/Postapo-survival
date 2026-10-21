using System;

namespace ProjectPostapo.ScriptableObjects
{
    [Serializable]
    public struct ComponentEntry
    {
        public ItemComponent component;
        public string value;

        public ComponentEntry(ItemComponent component, string value)
        {
            this.component = component;
            this.value = value;
        }
    }
}
