using UnityEngine;

namespace ProjectPostapo.Stats
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private float baseValue;

        public float Value
        {
            get
            {
                return baseValue;
            }
        }

        public Stat(float baseValue)
        {
            this.baseValue = baseValue;
        }

        public void Modify(float amount)
        {
            baseValue += amount;
        }

        public void Set(float newValue)
        {
            baseValue = newValue;
        }
    }
}