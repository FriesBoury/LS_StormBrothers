using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CameraMode
{
	Menu,
	Game
}

public class CameraFollow : MonoBehaviour {
	
	public static CameraMode CurrentCameraMode = CameraMode.Menu;
	
	public Transform Player1;
	public Transform Player2;
	public float CamDist;
	public float CamDistFar;
	public float CamHeightOffset;
	
	private Vector3 _DesiredTargetPos;
	private Vector3 _CurrentTargetPos;
	
	
	private bool _PlayerDied = false;
	public float Damping = 0.4f;
	
	private float _CurrentCamDist = 0;
	private float _DesiredCamDist = 0;
	
	private Vector3 _CurrentCamPos;
	private Vector3 _DesiredCamPos;
	
	// Variables for intro screen
	private int _CamPosIndex = 0;
	public List<Transform> _CamPosList = new List<Transform>();
	public Transform EndTarget;
	
	private Vector3 _LookAtPos = new Vector3(0,0,0);
	
	// Use this for initialization
	void Start ()
	{		
		// Menu 
		transform.position = _CamPosList[_CamPosIndex].position;
		
		// Game
		_CurrentCamDist = _DesiredCamDist = CamDist;
		CalcDesiredPos();
		_CurrentTargetPos = _DesiredTargetPos;
	}
	void CalcLookat()
	{
		
		
		transform.position = _CurrentCamPos;
		
		transform.LookAt(_CurrentTargetPos);
	}
	void CalcDesiredPos()
	{
		_DesiredTargetPos = (Player1.transform.position + Player2.transform.position)*.5f;
		_DesiredCamPos = _CurrentTargetPos - Vector3.forward*_CurrentCamDist + Vector3.up*CamHeightOffset;
		
	}
	public void PlayerRespawned()
	{
		_PlayerDied = false;
	}
	public void PlayerDied()
	{
		_PlayerDied = true;
	}
	// Update is called once per frame
	void Update () 
	{
		float lerpVal = Time.deltaTime*0.1f*60f;
		
//		if(Input.GetKeyDown(KeyCode.Return))
//		{
//			CurrentCameraMode = CameraMode.Game;
//			_CurrentCamPos = transform.position;
//			_CurrentTargetPos = _LookAtPos;
//		}
		// Menu camera
		if(CurrentCameraMode == CameraMode.Menu)
		{
			if(_CamPosIndex < _CamPosList.Count-1)
			{
				
				
				
				transform.position = Vector3.Lerp(transform.position, _CamPosList[_CamPosIndex+1].position, lerpVal/4);
				
				if(_CamPosList[_CamPosIndex+1] && Mathf.Abs(_CamPosList[_CamPosIndex+1].position.x - transform.position.x) < 0.25f)
				{
					Debug.Log("close enough");
					++_CamPosIndex;
				}
		
					_LookAtPos = transform.position;
					_LookAtPos.y -= 1.0f;
					transform.LookAt(_LookAtPos);

			}
			else if(_CamPosIndex == _CamPosList.Count - 1)
			{
				_LookAtPos = Vector3.Lerp(_LookAtPos, EndTarget.position, lerpVal/20);
				transform.LookAt(_LookAtPos);
			}
		}
		// Game camera
		else if(CurrentCameraMode == CameraMode.Game)
		{
			if(!_PlayerDied)
			{
				CalcDesiredPos();
			}

			if(Input.GetKeyDown(KeyCode.Space))
			{
				_DesiredCamDist = CamDistFar;
			}
			if(Input.GetKeyUp(KeyCode.Space))
			{
				_DesiredCamDist = CamDist;
			}
			_CurrentCamPos = Vector3.Lerp(_CurrentCamPos,_DesiredCamPos,lerpVal);
			_CurrentTargetPos = Vector3.Lerp(_CurrentTargetPos,_DesiredTargetPos,lerpVal);
			_CurrentCamDist = Mathf.Lerp(_CurrentCamDist,_DesiredCamDist,lerpVal);
			
			CalcLookat();
		}
	}
}
