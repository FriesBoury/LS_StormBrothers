using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
	
	public int Index;
	public GameObject SpawnPos;
	public LevelType Type;
	// Use this for initialization
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player" )
		{
			Debug.Log("Checkpoint reached");
			GameObject manager = GameObject.FindGameObjectWithTag("Manager");
			manager.GetComponent<Checkpoints>().CheckpointReached(Index);
			manager.GetComponent<LevelManager>().SetLevelType(Type);
			
		}
	}
}
