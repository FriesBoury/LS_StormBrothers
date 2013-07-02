using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	private LevelType _currentLevelType;

	public GameObject Player1;
	public GameObject Player2;
	
	// Use this for initialization
	void Start () {
//		Sounds.PlayLevelBackground(LevelType.Menu);
	}
	
	
	public void SetLevelType(LevelType type)
	{
		if(_currentLevelType != type)
		{
			_currentLevelType = type;
			Sounds.PlayLevelBackground(type);
		}
	}
	
	public void EndLevelReached()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
