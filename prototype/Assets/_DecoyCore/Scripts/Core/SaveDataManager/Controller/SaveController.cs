namespace Decoy.Core.SaveSystem {
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using UnityEngine;
    using Decoy.Core.EventSystem;
    /// <summary>
    /// SaveController
    /// <summary>
    /// Author: Thomas van Opstal
    /// <summary>
    /// Load or Creates a Config file(xml based). Adds functionality to save/load data into it
    /// </summary>
    public class SaveController : MonoBehaviour {
        [Header("Version of the current savegame")]
        public string saveGameVersion;
        [Header("Template for the Config file")]
        public TextAsset config;

        private const string FILE_NAME = "Settings.cfg";
        private string FILE_PATH = "";

        private XDocument saveData;
        private static SaveController manager;

        public static SaveController Instance {
            get {
                if (!manager) {
                    manager = FindObjectOfType(typeof(SaveController)) as SaveController;

                    if (!manager)
                        Debug.LogError("There needs to be one active SaveController active in your scene.");
                    else
                        manager.Init();
                }
                return manager;
            }
        }

        private void Awake() {
            FILE_PATH = Application.persistentDataPath + "/SavedData";
        }

        private void Init() {
            if (!Directory.Exists(FILE_PATH))
                Directory.CreateDirectory(FILE_PATH);

            if (File.Exists(FILE_PATH + "/" + FILE_NAME)) {
                saveData = new XDocument(XDocument.Load(FILE_PATH + "/" + FILE_NAME));

                if (saveData.Element(SaveTypes.ROOT).Element(SaveTypes.GameSettings).Element(SaveTypes.SaveGameVersion).Value != saveGameVersion) {
                    XDocument newData = XDocument.Parse(config.text);

                    List<XElement> newDataElements = newData.Descendants().ToList();
                    List<XElement> previousDataElements = saveData.Descendants().ToList();

                    foreach (XElement oldElement in previousDataElements) {
                        if (!oldElement.HasElements) {
                            foreach (XElement newElement in newDataElements) {
                                if (!newElement.HasElements) {
                                    if (newElement.Name == oldElement.Name)
                                        newElement.SetValue(oldElement.Value);
                                }
                            }
                        }
                    }

                    saveData = newData;
                    saveData.Element(SaveTypes.ROOT).Element(SaveTypes.GameSettings).Element(SaveTypes.SaveGameVersion).Value = saveGameVersion;
                    SaveData();
                }
            } else {
                saveData = XDocument.Parse(config.text);
                saveData.Element(SaveTypes.ROOT).Element(SaveTypes.GameSettings).Element(SaveTypes.SaveGameVersion).Value = saveGameVersion;
                SaveData();
            }

            EventManager.TriggerEvent(SaveGameEventTypes.SAVE_GAME_READY);
        }

        public void WriteData(string section, string key, string value) {
            saveData.Element(SaveTypes.ROOT).Element(section).Element(key).Value = value;
        }

        public string ReadData(string section, string key) {
            return saveData.Element(SaveTypes.ROOT).Element(section).Element(key).Value;
        }

        public void WriteAndSaveData(string section, string key, string value) {
            saveData.Element(SaveTypes.ROOT).Element(section).Element(key).Value = value;
            SaveData();
        }

        public void SaveData() {
            saveData.Save(FILE_PATH + "/" + FILE_NAME);
        }

        public void SaveCustomConfigFile(string fileName, string xmlString) {
            XDocument saveFile = XDocument.Parse(xmlString);
            saveFile.Save(FILE_PATH + "/" + fileName + ".cfg");
        }

        public string GetCustomConfigFile(string fileName) {
            if (File.Exists(FILE_PATH + "/" + fileName))
                return XDocument.Load(FILE_PATH + "/" + fileName + ".cfg").ToString();
            else
                return null;
        }

        public string[] GetMultipleCustomConfigFile(string keyWord) {
            string[] allFiles = Directory.GetFiles(FILE_PATH + "/");
            List<string> matchingFiles = new List<string>();

            foreach (string file in allFiles) {
                if (file.Contains(keyWord))
                    matchingFiles.Add(XDocument.Load(file).ToString());
            }

            if (matchingFiles.Count != 0)
                return matchingFiles.ToArray();
            else
                return null;
        }
    }
}
