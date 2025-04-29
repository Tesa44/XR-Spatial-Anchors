using UnityEngine;
using System.IO;

[System.Serializable]

public class PositionData
{
    public float x, y, z;
}

public class cubePositionSaver : MonoBehaviour
{

    private string filePath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "cube_position.json");
        
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PositionData data = JsonUtility.FromJson<PositionData>(json);
            transform.position = new Vector3(data.x, data.y, data.z);
        }
    }

    void OnApplicationQuit()
    {
        PositionData data = new PositionData()
        {
            x = transform.position.x,
            y = transform.position.y,
            z = transform.position.z
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
    }

    void SavePosition()
    {
        PositionData data = new PositionData()
        {
            x = transform.position.x,
            y = transform.position.y,
            z = transform.position.z
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Pozycja zapisana do: " + filePath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SavePosition();
        }
        
    }
}
