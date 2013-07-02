using UnityEngine;
using System.Collections;

public class KillZoneScript : MonoBehaviour {
	
	PlayerKiller _Killer;
	// Use this for initialization
	void Start () {
	_Killer = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerKiller>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			_Killer.PlayerDied();
		}
	}
}
