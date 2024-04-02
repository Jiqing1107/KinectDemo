using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadCalibration : MonoBehaviour
{
    public Transform parent;
    string path = "multicam_config_Yasser.json";
    string jsonString;

    [System.Serializable]
    public class CamInfo
    {
        public int sensorType;
        public int sensorIndex;
        public string sensorId;
        public Vector3 position;
        public Vector3 rotation;
    }

    public class CalibrationData
    {
        public int version;
        public string[] settings;
        public List<CamInfo> camPose;
        public ulong estimatedAtTime;
        public string estimatedDateTime;
    }
   
    List<CamInfo> campose = new List<CamInfo>();
    CalibrationData calibrationData = new CalibrationData();

    void Start()
    {
        jsonString = File.ReadAllText(path);
        calibrationData = JsonUtility.FromJson<CalibrationData>(jsonString);
        campose = calibrationData.camPose;

        foreach (CamInfo child in campose)
        {
            GameObject childObject = transform.GetChild(child.sensorIndex).gameObject;
            childObject.transform.localPosition = child.position;
            childObject.transform.localRotation = Quaternion.Euler(child.rotation);
        }
    }
}
