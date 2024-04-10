using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string username;
    public string recordHolder;
    public int record;
    // Start is called before the first frame update
    public void Awake()
    {
        if (Instance  != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    [System.Serializable]
    class Savedata
    {
        public string recordHolder;
        public string username;
        public int record;
    }
    public void SaveData()
    {
        Savedata data = new Savedata();
        data.recordHolder = recordHolder;
        data.username = username;
        data.record = record;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Savedata data = JsonUtility.FromJson<Savedata>(json);

            recordHolder = data.recordHolder;
            username = data.username;
            record = data.record;
        }
    }


}
