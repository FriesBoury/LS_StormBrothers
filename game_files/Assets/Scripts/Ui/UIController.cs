using UnityEngine;
using System.Collections;

public enum UIState
{
	None,
	Controls,
	About
}

public class UIController : MonoBehaviour 
{
	public GameObject Player1;
	public GameObject Player2;
	// Main menu overlays
	public Texture2D ControlsTexture;
	public Texture2D AboutTexture;
	public Texture2D ControlsTextureAndroid;
	public Texture2D AboutTextureAndroid;
	
	private UIState _activeUIState;
	
	public bool IsPauzeMenuActive = false;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Don't allow the player to make UI changes during the game
		if(CameraFollow.CurrentCameraMode == CameraMode.Menu)
		{
			// Select player
			if (Input.GetButtonDown ("Fire1")) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				
				RaycastHit hit = new RaycastHit();
							
				if (Physics.Raycast (ray, out hit)) 
				{
					// A unit has been selected
					if (hit.transform != null)
					{
						switch(hit.transform.tag)
						{
							case("StartGame"):
								Debug.Log("Start clicked");
								_activeUIState = UIState.None;
								// Make both  turn around 180°
								Player1.transform.Rotate(Vector3.up, 180.0f);
								Player2.transform.Rotate(Vector3.up, 180.0f);
								
								CameraFollow.CurrentCameraMode = CameraMode.Game;
								break;
							case("Controls"):
								Debug.Log("Controls clicked");
								_activeUIState = UIState.Controls;
								OnGUI();
								break;
							case("About"):
								Debug.Log("About clicked");
								_activeUIState = UIState.About;
								OnGUI();
								break;
							case("ExitGame"):
								Debug.Log("Exit clicked");
								Application.Quit();
								break;
						}
					}
				}
			}
		}
		
		// Pauze menu
		if(CameraFollow.CurrentCameraMode == CameraMode.Game)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				IsPauzeMenuActive = !IsPauzeMenuActive;
				OnGUI();
			}
		}
			
		// Close the UI image
		if(Input.GetButtonDown("Fire1"))
		{
			_activeUIState = UIState.None;
			OnGUI();
		}
	}
	
	void OnGUI() 
	{
		// Pauze menu buttons
		if(CameraFollow.CurrentCameraMode == CameraMode.Game && IsPauzeMenuActive)
		{
			if(GUI.Button(new Rect(Screen.width/2 - 250/2, 250, 250, 50), "View Controls"))
			{
				Debug.Log("View Controls clicked");
				_activeUIState = UIState.Controls;
			}
			
			if(GUI.Button(new Rect(Screen.width/2 - 250/2, 350, 250, 50), "Exit Game"))
			{
				Debug.Log("Exit Game clicked");
				Application.Quit();
			}
		}
		
		// Switch UI State
		switch(_activeUIState)
		{
			case(UIState.None):
				break;
				
			case(UIState.Controls):
				
				if(Application.platform == RuntimePlatform.Android)
				{
					if(ControlsTextureAndroid == null)return;
					GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), ControlsTextureAndroid);
				}
				else
				{
					if(ControlsTexture == null)return;
					GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), ControlsTexture);
				}
				break;
				
			case(UIState.About):
				if(Application.platform == RuntimePlatform.Android)
					GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), AboutTextureAndroid);
				else
					GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), AboutTexture);
				break;
		}
	}
}
