using UnityEngine;
using System.Collections;

public class GrabTrigger : MonoBehaviour 
{
	public int _numAttachedPlayers = 0;
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	// Disable gravity and velocity when entered a grabpoint trigger
	void OnTriggerEnter(Collider other) 
	{	
		if(other.name == "Player 1" || other.name == "Player 2")
		{
			++_numAttachedPlayers;

			if(_numAttachedPlayers == 1 )
			{
				other.GetComponent<PlayerController>().GrabAnchor(transform);
			}
			
		}
    }
	
	// Enable gravity again when out of grabpoint trigger zone
	void OnTriggerExit(Collider other) 
	{
		if(other.name == "Player 1" || other.name == "Player 2")
		{
			--_numAttachedPlayers;
			if(_numAttachedPlayers == 0)
			{
				other.GetComponent<PlayerController>().ReleaseAnchor(transform);
				
			}
		}
    }
}
