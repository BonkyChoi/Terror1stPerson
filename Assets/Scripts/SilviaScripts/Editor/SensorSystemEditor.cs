using System;

using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SC_SensorSystem))]
    public class SensorSystemEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            SC_SensorSystem sensor = (SC_SensorSystem) target;
            Handles.color = Color.white;
            Handles.DrawWireArc (sensor.transform.position, Vector3.up, Vector3.forward, 360, sensor.SensorAngle);
            Vector3 viewAngleA = sensor.DirFromAngle (-sensor.VisionAngle / 2);
            Vector3 viewAngleB = sensor.DirFromAngle (sensor.VisionAngle / 2);

            Handles.DrawLine (sensor.transform.position, sensor.transform.position + viewAngleA * sensor.SensorAngle);
            Handles.DrawLine (sensor.transform.position, sensor.transform.position + viewAngleB * sensor.SensorAngle);
        }
    }

    
}
