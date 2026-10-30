using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ProjectPostapo.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class ItemSO : ScriptableObject, IGetComponent
    {
        public Texture2D Icon;
        public SOType ObjectType;
        public ItemSubtypes[] Subtypes;
        public List<ComponentEntry> Components = new();

        public ComponentEntry? GetComponent(ItemComponent component)
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (Components[i].component == component)
                {
                    return Components[i];
                }
            }
            return null;
        }

        public string GetComponentValue(ItemComponent component)
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

        public float GetNumericValueOfComponent(ItemComponent component)
        {
            string number = Regex.Match(GetComponentValue(component), @"[-+]?\d*\.?\d+").Value;

            if (float.TryParse(number, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
            {
                return value;
            }
            return 0f;
        }

        public void AddComponent(ItemComponent component, string value) => Components.Add(new ComponentEntry(component, value));
        public void AddComponent(ref ComponentEntry component) => Components.Add(component);
    }
}