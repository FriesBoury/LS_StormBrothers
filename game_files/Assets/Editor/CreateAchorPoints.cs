using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateAnchorPoints : ScriptableWizard {
 
 	private GameObject _anchor_point;
	private GameObject _prefab;
	
    [MenuItem("BrainsEden/Create Anchor Points from selected")]
    static void CreateWizard()
    {
		ScriptableWizard.DisplayWizard<CreateAnchorPoints>("Create Light", "Create", "Apply");
    }
 
    void OnWizardCreate()
    {
		_prefab = GameObject.FindGameObjectWithTag("Manager").GetComponent<AnchorGenerator>().prefab;
        _anchor_point = Instantiate(_prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }
}