using UnityEngine;
using System.Collections;

public class AnchorGenerator : MonoBehaviour {

	public GameObject prefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnDrawGizmos ()
	{
		GameObject[] anchor_points = GameObject.FindGameObjectsWithTag("AnchorPoint");
		for (int i = 0; i < anchor_points.Length; i++)
		{
			//Gizmos.DrawIcon(anchor_points[i].transform.position, "Anchor");
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(anchor_points[i].transform.position, 0.5f);
		}
	}
}
