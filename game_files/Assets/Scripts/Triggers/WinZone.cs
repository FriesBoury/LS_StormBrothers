using UnityEngine;
using System.Collections;

public class WinZone: MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
			{
				if(other.gameObject == player)continue;
				if(player.GetComponent<PlayerController>()._Finished)continue;
				
				player.GetComponent<PlayerController>().LaunchCharacter(Vector3.zero,Vector3.up*300f);
			}
			other.GetComponent<PlayerController>().Finish(gameObject);
		}
	}
}
