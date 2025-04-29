using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MultiObjectPositionLoader : MonoBehaviour
{
    [System.Serializable]
    public class ObjectData
    {
        public string name;
        public float x, y, z;
    }

    [System.Serializable]
    public class ObjectDataList
    {
        public List<ObjectData> objects = new List<ObjectData>();
    }

    public string fileName = "object_positions.json";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadPositions()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (!File.Exists(path)) {
            Debug.LogWarning("Cannot find JSON file: " + path);
            return;
        }

        string json = File.ReadAllText(path);
        ObjectDataList dataList = JsonUtility.FromJson<ObjectDataList>(json);

        foreach (ObjectData data in dataList.objects)
        {
            GameObject obj = GameObject.Find(data.name);
            if (obj != null)
            {
                obj.transform.position = new Vector3(data.x, data.y, data.z);
                Debug.Log($"Set position {obj.name} to {data.x} {data.y} {data.z}");
            }
            else
            {
                Debug.LogWarning($"Cannot find object: {data.name}");
            }

        }
    }
}
