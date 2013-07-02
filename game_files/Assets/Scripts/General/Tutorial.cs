using UnityEngine;
using System;
using System.Collections.Generic;
using System;

public class Tutorial : MonoBehaviour {
	
	public GameObject Player;
	public bool Enabled = false;
	public List<Transform> Anchors = new List<Transform>();
	int currentAnchorIndex = 0;
	
	public float WaitInterval = 1f;
	
	private float _waitTimer;
	// Use this for initialization
	void Start () {
		Player.GetComponent<PlayerController>().AnchorGrabbed += AnchorGrabbed;
		_waitTimer = WaitInterval;
	}


	
	public void Play()
	{
		Enabled = true;
		
	}
	
	void AnchorGrabbed(object sender, EventArgs e)
	{
		//start timer
		_waitTimer = WaitInterval;
		
	}
	// Update is called once per frame
	void Update () 
	{
		if(!Enabled)return;
		
			if(_waitTimer > 0)
			{
				_waitTimer -= Time.deltaTime;
				if(_waitTimer <=0)
				{
					//Jump to next part
					Vector3 nextPos = Anchors[currentAnchorIndex].position;
					Vector3 toNextPos = nextPos - transform.position;
					toNextPos*= 5f;
					Debug.Log(String.Format("Launching Player: {0}", toNextPos));
					Player.GetComponent<PlayerController>().LaunchCharacter(Player.transform.position,Player.transform.position + toNextPos);
					
					++currentAnchorIndex;
					if(currentAnchorIndex >= Anchors.Count) Enabled = false;
					
				}
			}
		
		
		
		
		
	}
}
