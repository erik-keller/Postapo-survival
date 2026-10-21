using System;
using System.Collections.Generic;
using UnityEngine;
using static ProjectPostapo.ScriptableObjects.ItemSO;

namespace ProjectPostapo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemSO : ScriptableObject
    {
        public Texture2D Icon;
        public SOType ObjectType;
        public ItemSubtypes[] Subtypes;
        public List<ComponentEntry> Components = new();

        [Serializable]
        public class ComponentEntry
        {
            public ItemComponent component;
            public string value;

            public ComponentEntry(ItemComponent component, string value)
            {
                this.component = component;
                this.value = value;
            }
        }

        public string GetComponent(ItemComponent component)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].component == component)
                {
                    return Components[i].value;
                }
            }
            return null;
        }

        public void AddComponent(ItemComponent component, string value) => Components.Add(new ComponentEntry(component, value));
        public void AddComponent(ComponentEntry component) => Components.Add(component);
    }
}