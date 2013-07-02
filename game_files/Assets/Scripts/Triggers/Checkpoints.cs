using UnityEngine;
using System.Collections.Generic;

public class Checkpoints : MonoBehaviour 
{
	public List<GameObject> CheckPointsList = new List<GameObject>();
	
	
	public int CurrentCheckPoint = 0;
	public void Start()
	{
		for(int i = 0; i < CheckPointsList.Count; ++i)
		{
			CheckPointsList[i].GetComponent<Checkpoint>().Index = i;
		}
	}
	
	public void CheckpointReached(int index)
	{
		CurrentCheckPoint = Mathf.Max(index,CurrentCheckPoint);
	}
	
	public Vector3 GetSpawnPos()
	{
		return CheckPointsList[CurrentCheckPoint].GetComponent<Checkpoint>().SpawnPos.transform.position;
	}

}
