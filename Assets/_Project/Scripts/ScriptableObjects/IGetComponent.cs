using UnityEngine;

namespace ProjectPostapo.ScriptableObjects
{
    public interface IGetComponent
    {
        public ComponentEntry? GetComponent(ItemComponent component);
        public string GetComponentValue(ItemComponent component);
        public float GetNumericValueOfComponent(ItemComponent component);
    }
}
