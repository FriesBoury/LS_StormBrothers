using UnityEngine;
using System.Collections;

public class Chain : MonoBehaviour {
	
	public Material mat;
	public GameObject Player1Anchor;
	public GameObject Player2Anchor;
	public static float MaximumDistance;
	public int NumObjects;
	public float MaxDistance;
	private GameObject[] ChainLinks;
	public GameObject ChainLinkPrefab;
	public float Radius;
	private Mesh _Mesh;
	public float zOffset = 1f;
	
	public void Reset()
	{
		if(ChainLinks != null)
		{
			foreach(GameObject obj in ChainLinks)
			{
				Destroy(obj);
			}
			
		}
		
		//set the limit of the joint to this distance
		SoftJointLimit limit = Player1Anchor.transform.parent.GetComponent<ConfigurableJoint>().linearLimit;
		limit.limit = MaxDistance;
		Player1Anchor.transform.parent.GetComponent<ConfigurableJoint>().linearLimit = limit;
		
		MaximumDistance = MaxDistance*0.5f;
//		MaximumDistance = MaxDistance;
		
//		Player1Anchor.transform.parent.GetComponent<ConfigurableJoint>().linearLimit.limit = MaxDistance;
		if(NumObjects %2 == 0)++NumObjects;
		ChainLinks = new GameObject[NumObjects];
		
		float distStep = MaximumDistance / (NumObjects-1);
		
		
		
		for(int i= 0; i < NumObjects; ++i)
		{
			ChainLinks[i] = (GameObject)Instantiate(ChainLinkPrefab,Player1Anchor.transform.position + Vector3.right*distStep*i,Quaternion.identity);
			if(i == 0)
			{
				ChainLinks[i].GetComponent<CharacterJoint>().anchor = -(Vector3.right*distStep);
				ChainLinks[i].GetComponent<CharacterJoint>().connectedBody = Player1Anchor.rigidbody;
				
			}
			
			
			
//			else if (i == NumObjects - 1)
//			{
//				ChainLinks[i].GetComponent<HingeJoint>().connectedBody = Player2Anchor.rigidbody;
//				HingeJoint joint = (HingeJoint)ChainLinks[i].AddComponent(typeof(HingeJoint));
//				joint.connectedBody = Player2Anchor.rigidbody;
//			}
			else
			{
				ChainLinks[i].GetComponent<CharacterJoint>().anchor = -(Vector3.right*distStep);
				ChainLinks[i].GetComponent<CharacterJoint>().connectedBody = ChainLinks[i-1].rigidbody;
			}
			
			
			
		}
		
		Player2Anchor.transform.parent.position = Player1Anchor.transform.position + Vector3.right*MaximumDistance;
		Player2Anchor.GetComponent<CharacterJoint>().connectedBody = ChainLinks[NumObjects-1].rigidbody;
		GenerateMesh();
	}
	// Use this for initialization
	void Start () 
	{
		transform.position = new Vector3(0,0,-zOffset);
		Physics.gravity = Vector3.down*20f;
		
		Reset();
	}
	

	void GenerateMesh()
	{
		float radius = Radius;
		
		
		int numSides = 5;
				
		Vector3[] vertices = new Vector3[numSides*NumObjects];
		Vector2[] UVs = new Vector2[numSides*NumObjects];
		int[] indices = new int[(NumObjects-1)*numSides* 6];
		//iterate over objects
		
		
		for(int i= 0; i < NumObjects; ++i)
		{
			Quaternion rot = ChainLinks[i].transform.rotation;
//			Quaternion rot = Quaternion.identity;
//			Vector3 localPos = Vector3.right*20*i;
			
			Vector3 localPos = ChainLinks[i].transform.position;
			
			Vector3 dir = rot * Vector3.left;
			Vector3 up = Vector3.Cross(dir,new Vector3(0,0,1));

			//iterate over sides
			float angleStep = -360f/(float)numSides;
			for(int j = 0; j < numSides; ++j)
			{
				float angle = 240f+ j*angleStep;
				Vector3 offset = Quaternion.AngleAxis(angle,dir)*up*radius;
				
				vertices[j+numSides*i] = localPos + offset;
				
				UVs[j+numSides*i] = new Vector2((float)0.5f*j/(float)numSides,i);
			}
			
		}
		
//		if(alsoIndices)
//		{
		//Indices

			for(int i= 0; i < (NumObjects-1)*numSides* 6; ++i)
			{
				//triangle 1:
				int quadNum = i/6; // = start vertex
				int ringNum = quadNum/numSides;
				int sideNum = quadNum%numSides;
				switch(i%6)
				{
				case 3:
				case 0:
					
					indices[i] = quadNum;
					break;
				case 1:
					indices[i] = ringNum*numSides + (quadNum+1)%numSides;
					break;
				case 4:
				case 2:
					indices[i] = (ringNum+1)*numSides + (quadNum+1)%numSides;
					break;
				case 5:
					indices[i] = (ringNum+1)*numSides + quadNum%numSides;
					break;
				}
			}
		
		
		
		
		GetComponent<MeshFilter>().mesh = _Mesh = new Mesh();
		
			
		_Mesh.vertices = vertices;	
		_Mesh.uv = UVs;
		_Mesh.SetTriangles(indices,0);	
		_Mesh.RecalculateNormals();
//		 = _Mesh;
		
	}
	
	
//	void GenerateMesh()
//	{
//		
//		    float size = 1;
//       Vector3[] vertexList = new Vector3[]{
//          new Vector3(-size, -size, -size),
//          new Vector3(-size,  size, -size),
//          new Vector3( size,  size, -size),
//          new Vector3( size, -size, -size),
//          new Vector3( size, -size,  size),
//          new Vector3( size,  size,  size),
//          new Vector3(-size,  size,  size),
//          new Vector3(-size, -size,  size)
//		};
//       
//        int[] faceList = new int[]{
//          0, 1, 3, //   1: face arrière
//          0, 2, 3,
//          3, 2, 5, //   2: face droite
//          3, 5, 4,
//          5, 2, 1, //   3: face dessue
//          5, 1, 6,
//          3, 4, 7, //   4: face dessous
//          3, 7, 0,
//          0, 7, 6, //   5: face gauche
//          0, 6, 1,
//          4, 5, 6, //   6: face avant
//          4, 6, 7
//		};
//         
//         Vector2[] textureCoordinate= new Vector2[]
//		{
//        new Vector2(0.0f,  0.0f),
//        new Vector2(1.0f,  0.0f),
//        new Vector2(0.0f,  1.0f),
//        new Vector2(1.0f,  0.0f)
//		};
//       
//     
//       Mesh mesh = new Mesh();
//       mesh.vertices = vertexList;
//       mesh.triangles = faceList;
//       
//       mesh.uv = textureCoordinate;
//       mesh.RecalculateNormals();
//       //mesh.transform.position = Vector3(0, 0.5, 0)
//       //mesh.position = Vector3(0, 0.5, 0)
//       GetComponent<MeshFilter>().mesh = mesh;  
//       
//     
//    }
     
   
		
		
		
	
	void Update () {
	
		GenerateMesh();
	}
}
