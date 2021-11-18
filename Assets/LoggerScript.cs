using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LoggerScript : MonoBehaviour
{
    // Start is called before the first frame update

    private float time;
    private List<string[]> rowData = new List<string[]>();
    public Camera camera;
    private string filePath;
    public GameObject pizza;
    public GameObject player;
    private GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        enemy = GameObject.Find("Enemy(Clone)");
        if (time >= 1f)
        {
            time = 0;
            SaveToCSV();
        }
    }

    private void Start()
    {
        filePath = getPath();
        string[] rowDataTemp = new string[8];
        rowDataTemp[0] = "Time";
        rowDataTemp[1] = "Position";
        rowDataTemp[2] = "Rotation";
        rowDataTemp[3] = "player Position";
        rowDataTemp[4] = "pizza Rotation";
        rowDataTemp[5] = "enemy Position";
        rowDataTemp[6] = "Variant";
        rowDataTemp[7] = "Condition";
        rowData.Add(rowDataTemp);
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string seperator = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(seperator, output[index]));

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    void SaveToCSV()
    {
        string[] rowDataTemp = new string[8];
        rowDataTemp = new string[8];
        rowDataTemp[0] = System.DateTime.Now.ToLongTimeString();
        rowDataTemp[1] = camera.transform.position.ToString();
        rowDataTemp[2] = camera.transform.rotation.ToString();
        rowDataTemp[3] = player.transform.position.ToString();
        rowDataTemp[4] = pizza.transform.rotation.ToString();
        if (enemy != null)
            rowDataTemp[5] = enemy.transform.position.ToString();
        if (pizza.GetComponent<MeshCollider>().material.bounciness == 0)
            rowDataTemp[6] = "Variant A";
        else
            rowDataTemp[6] = "Variant B";
        rowDataTemp[7] = "Hardware Prototype";
        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string seperator = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(seperator, output[index]));



        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }


    private string getPath()
    {
#if UNITY_EDITOR
        for (int i = 0; i < 100; i++)
        {
            if (!File.Exists(Application.dataPath + "/CSV/" + "Oculus_Quest_VRBall(" + i + ").txt"))
            {
                return Application.dataPath + "/CSV/" + "Oculus_Quest_VRBall(" + i + ").txt";
            }
        }
        return "No Path found";
#elif UNITY_ANDROID
        for(int i = 0;i< 100; i++)
        {
            if (!File.Exists(Application.persistentDataPath + "/CSV/" + "Oculus_Quest_VRBall(" + i + ").txt"))
            {
                return Application.persistentDataPath + "/CSV/" + "Oculus_Quest_VRBall(" + i + ").txt";
            }
        }
                    return "No Path found";
#else
        for(int i = 0;i< 100; i++)
        {
            if (!File.Exists(Application.dataPath + "/CSV/" + "Saved_data(" + i + ").txt"))
            {
                return Application.dataPath + "/CSV/" + "Saved_data(" + i + ").txt";
            }
        }
                    return "No Path found";
#endif
    }
}
