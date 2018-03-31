using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AppleTree : MonoBehaviour {
	[Header("Set in Inspector")]
	// Prefab for instantiating apples
	public GameObject applePrefab;

	// Speed at which the AppleTree moves
	public float speed = 1f;

	// Distance where AppleTree turns around
	public float leftAndRightEdge = 10f;

	// Chance that the AppleTree will change directions
	public float chanceToChangeDirections = 0.1f;

	// Rate at which Apples will be instantiated
	public float secondsBetweenAppleDrops = 1f;

	void Start () 
	{
		// Dropping apples every second
		//The Invoke() function calls a named function in a certain number of seconds. 
		//In this case, it is calling the new function DropApple(). 
		//The second parameter, 2f, tells Invoke() to wait 2 seconds before it calls DropApple()
		Invoke("DropApple", 2f);
	}

	//DropApple() is a custom function to instantiate an Apple at the AppleTree's location.
	void DropApple()
	{
		GameObject apple = Instantiate<GameObject> (applePrefab);//DropApple() creates an instance of applePrefab and assigns it to the GameObject variable apple.
		apple.transform.position = transform.position;//The position of this new apple GameObject is set to the position of the AppleTree.
		//Invoke() is called again. This time, it will call the DropApple() function in secondsBetweenAppleDrops seconds 
		//(in this case, in 1 second based on the default settings in the Inspector). Because DropApple() invokes itself every time it is called,
		//the effect will be for an Apple to be dropped every second that the game runs.
		Invoke ("DropApple", secondsBetweenAppleDrops);
	}


	//set the AppleTree's Rigidbody to be kinematic, meaning that we can move it via code, but it will not react to collisions with other objects.
	void Update ()
	{
		// Basic Movement
		Vector3 pos = transform.position;//Define Vector3 position to be current position of the AppleTree
		pos.x += speed * Time.deltaTime;//the x component of pos is increased by the speed * Time.deltaTime
		transform.position = pos;		//assigns the modified pos back to transformposition(moves AppleTree to a new position)

		// Changing Direction
		if ( pos.x < -leftAndRightEdge ) //Test whether the new pos.x that was just set in the previous lines is less than the negative side-to-side limit that is set by leftAndRightEdge.
		{
			speed = Mathf.Abs(speed); // Move right
		} 
		else if ( pos.x > leftAndRightEdge ) 
		{ 
			speed = -Mathf.Abs(speed); // Move left
		}
		else if (Random.value < chanceToChangeDirections)
		{
			speed *= -1; //change direction
		}
	}

	void FixedUpdate()
	{
		if(Random.value < chanceToChangeDirections)
		{
			speed *= -1; //change direction
		}
	}
}