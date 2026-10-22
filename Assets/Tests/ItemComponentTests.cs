using UnityEngine;
using NUnit.Framework;
using ProjectPostapo.ScriptableObjects;

namespace ProjectPostapo.Tests
{
    public class ItemComponentTests
    {
        [Test]
        public void VolumeNumericValueComponentAssert()
        {
            ItemSO item = new();
            ComponentEntry comp = new ComponentEntry(ItemComponent.Volume, "400 ml");
            item.AddComponent(ref comp);

            float expected = 400f;
            float actual = item.GetNumericValueOfComponent(ItemComponent.Volume);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void VolumeValueComponentAssert()
        {
            ItemSO item = new();
            ComponentEntry comp = new ComponentEntry(ItemComponent.Volume, "400 ml");
            item.AddComponent(ref comp);

            string expected = "400 ml";
            string actual = item.GetComponentValue(ItemComponent.Volume);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void NullComponent()
        {
            ItemSO item = new();

            Assert.IsNull(item.GetComponent(ItemComponent.Volume));
        }
    }
}
