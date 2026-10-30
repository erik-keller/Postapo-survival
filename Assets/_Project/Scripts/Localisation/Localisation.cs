using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace ProjectPostapo.LocalisationSystem
{
    public class Localisation : MonoBehaviour
    {
        public static Localisation Instance; // Instance of ingame object

        private Dictionary<string, string> dictionary = new(); // Dictionary of words - <key, translated word>

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

        /// <summary>
        /// This method loads dictionary base from json file with name in format ISO-3166.json
        /// </summary>
        /// <param name="language">Name of file in ISO-3166 format without .json</param>
        public void LoadLanguage(string language)
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>($"Languages/{language}");
            dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonTextFile.ToString());
        }

        /// <summary>
        /// This method gives exact translation of key from dictionary
        /// </summary>
        /// <param name="id">id is equal for key in dictionary</param>
        /// <returns>If the word was found in dictionary it returns it, if not it return an id</returns>
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
