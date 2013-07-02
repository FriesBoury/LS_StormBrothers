using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GrabAnchorState
{
	Active,
	Broken
}

public class TemporaryGrabTrigger : MonoBehaviour 
{
	public float TimeLeft = 3.0f; 
	public bool RechargeTimer = false;
	
	private float _originalTime = 3.0f;
	private GrabAnchorState _currentState = GrabAnchorState.Active;
	private int _numAttachedPlayers = 0;
	private GameObject _attachedPlayer;
	private Vector3 _startPos;
	private Quaternion _startRot;
	
	private static List<TemporaryGrabTrigger> _AllTriggerInstances = new List<TemporaryGrabTrigger>();
	// Use this for initialization
	void Start () 
	{
		_originalTime = TimeLeft;
		_startPos = transform.position;
		_startRot = transform.rotation;
		_AllTriggerInstances.Add(this);
	}
	
	public static void ResetAll()
	{
		foreach(TemporaryGrabTrigger tr in _AllTriggerInstances)
		{
			tr.Reset();
		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(_numAttachedPlayers == 1)
		{
			TimeLeft -= Time.deltaTime;
		}
		
		if(TimeLeft <= 0 && _attachedPlayer != null)
		{
			rigidbody.useGravity = true;
			_currentState = GrabAnchorState.Broken;
			_attachedPlayer.GetComponent<PlayerController>().ReleaseAnchor(transform);
		}
		
		// Recharge time
		if(RechargeTimer == true && _numAttachedPlayers == 0)
		{
			if(TimeLeft < _originalTime)
			{
				TimeLeft += Time.deltaTime;
			}
		}
	}
	public void Reset()
	{
		transform.position = _startPos;
		TimeLeft = _originalTime;
		transform.rotation = _startRot;
		rigidbody.useGravity = false;
		rigidbody.velocity = Vector3.zero;
		_currentState = GrabAnchorState.Active;
		_numAttachedPlayers = 0;
	}
	// Disable gravity and velocity when entered a grabpoint trigger
	void OnTriggerEnter(Collider other) 
	{	
		if(other.name == "Player 1" || other.name == "Player 2")
		{
			++_numAttachedPlayers;
			
			if(_originalTime < 4f)
			Sounds.PlaySoundEffect(SoundEffectType.Crack4,other.gameObject.GetComponent<PlayerController>().CurrentCharacter);
			else
			Sounds.PlaySoundEffect(SoundEffectType.Crack6,other.gameObject.GetComponent<PlayerController>().CurrentCharacter);

			if(_numAttachedPlayers == 1 && _currentState == GrabAnchorState.Active)
			{
				other.GetComponent<PlayerController>().GrabAnchor(transform);
				_attachedPlayer = other.gameObject;
			}
			
		}
    }
	
	// Enable gravity again when out of grabpoint trigger zone
	void OnTriggerExit(Collider other) 
	{
		
		if(other.name == "Player 1" || other.name == "Player 2")
		{
			--_numAttachedPlayers;
			if(_numAttachedPlayers <= 0)
			{
				_numAttachedPlayers = 0;
				other.GetComponent<PlayerController>().ReleaseAnchor(transform);
				_attachedPlayer = null;
			}
		}
    }
}
