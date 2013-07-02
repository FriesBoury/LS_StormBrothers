using UnityEngine;
using System.Collections;

public class PlayerKiller : MonoBehaviour {
	
	Checkpoints _CheckpointManager;
	public GameObject Player1;
	public GameObject Player2;
	private float _RespawnTimer;
	public float RespawnTime;
	
	// Use this for initialization
	void Start () 
	{
		_RespawnTimer = 0;
		_CheckpointManager = GetComponent<Checkpoints>();
		RespawnPlayer();
	}
	
	public void PlayerDied()
	{
		Camera.mainCamera.GetComponent<CameraFollow>().PlayerDied();
		_RespawnTimer = RespawnTime;
	}
	
	public void RespawnPlayer()
	{
		TemporaryGrabTrigger.ResetAll();
		
		Camera.mainCamera.GetComponent<CameraFollow>().PlayerRespawned();
		Vector3 spawnPos = _CheckpointManager.GetSpawnPos();
		Player1.transform.position = spawnPos;
		Player2.transform.position = spawnPos;
		GetComponent<Chain>().Reset();
		
		Player1.rigidbody.isKinematic = false;
		Player2.rigidbody.isKinematic = false;
		Player1.rigidbody.useGravity = true;
		Player2.rigidbody.useGravity = true;
		Player1.rigidbody.velocity = Vector3.zero;
		Player2.rigidbody.velocity = Vector3.zero;
		Player1.GetComponent<PlayerController>().IsGrabbingAnchor = false;
		Player2.GetComponent<PlayerController>().IsGrabbingAnchor = false;
	}
	// Update is called once per frame
	void Update () 
	{
		if(_RespawnTimer > 0)
		{
			_RespawnTimer -= Time.deltaTime;
			if(_RespawnTimer <= 0)
			{
				RespawnPlayer();
			}
		}
		
		
	}
}
