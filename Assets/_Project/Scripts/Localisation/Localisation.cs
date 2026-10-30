using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ProjectPostapo.LocalisationSystem
{
    public class Localisation : MonoBehaviour
    {
        public static Localisation Instance;

        private Dictionary<string, string> dictionary = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                LoadLanguage("eng");
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadLanguage(string language)
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>($"Languages/{language}");
            dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonTextFile.ToString());
        }

        public string GetTranslation(string id)
        {
            if (dictionary == null)
            {
                Debug.LogWarning("dictionary is null");
                return id;
            }

            string translated = dictionary[id];

            if (translated == null || translated == string.Empty)
            {
                Debug.LogWarning("id not found in disctionary");
                return id;
            }

            return translated;
        }
    }
}
