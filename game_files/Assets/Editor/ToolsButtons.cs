using UnityEditor;
using System.Collections;
using UnityEngine;

public class ToolsButtons: EditorWindow
{
	private GameObject _prefab;
	private GameObject _anchor_point;
	private bool _visibile = false;
    [MenuItem("Window/Tools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ToolsButtons),false,"Tools");
    }

    void OnGUI()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("AnchorPoints: Create from selected"))
        {
            _prefab = GameObject.FindGameObjectWithTag("Manager").GetComponent<AnchorGenerator>().prefab;
			GameObject[] _selection= Selection.gameObjects;
			for (int i = 0; i < _selection.Length; i++)
			{
				_anchor_point = PrefabUtility.InstantiatePrefab(_prefab) as GameObject;
				//_anchor_point = Instantiate(_prefab, _selection[i].transform.position, Quaternion.identity) as GameObject;
				_anchor_point.transform.position = _selection[i].transform.position;
				_anchor_point.transform.parent = _selection[i].transform;
			}

        }
		if (GUILayout.Button("AnchorPoints: 0 zpos"))
        {
			GameObject[] _points = GameObject.FindGameObjectsWithTag("AnchorPoint");
			for (int i = 0; i < _points.Length; i++)
			{
				Vector3 _pos = _points[i].transform.position;
				_pos.z = 0;
				_points[i].transform.position = _pos;
			}
		}
		if (GUILayout.Button("AnchorPoints: toggle visibility"))
        {
			_visibile = !_visibile;
			GameObject[] _points = GameObject.FindGameObjectsWithTag("AnchorPoint");
			for (int i = 0; i < _points.Length; i++)
			{
				_points[i].renderer.enabled = _visibile;
			}
		}
		/*
        if (GUILayout.Button("Conditions"))
        {
            ConditionEditor.ShowWindow();
        }
		if (GUILayout.Button("Level Order"))
        {
            LevelOrderEditor.ShowWindow();
        }*/
//        if (GUILayout.Button("Localisation"))
//        {
//            LocalisationEditor.ShowWindow();
//        }
//        if (GUILayout.Button("Event Order"))
//        {
//            EventOrderEditor.ShowWindow();
//        }
//        if (GUILayout.Button("Cleanup Assets"))
//        {
//            CleanupProject.ShowWindow();
//        }


        GUILayout.EndVertical();
    }
}
