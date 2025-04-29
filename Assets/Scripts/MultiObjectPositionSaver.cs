using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class MultiObjectPositionSaver : MonoBehaviour
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            savePositions();
        }
    }

    void OnApplicationQuit()
    {
        savePositions();
    }

    void savePositions()
    {
        GameObject[] objectsToSave = GameObject.FindGameObjectsWithTag("SaveTarget");
        ObjectDataList dataList = new ObjectDataList();

        foreach (GameObject obj in objectsToSave) {

            ObjectData data = new ObjectData
            {
                name = obj.name,
                x = obj.transform.position.x,
                y = obj.transform.position.y,
                z = obj.transform.position.z,
            };
            dataList.objects.Add(data);
        }

        string json = JsonUtility.ToJson(dataList, true);
        string path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);

        Debug.Log("Saved positions to: " + path);
    }
}
