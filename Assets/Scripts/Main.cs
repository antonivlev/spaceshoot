using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
	public GameObject cam_obj;

	public GameObject player_obj;
	public GameObject planet_obj;

	private Rigidbody2D player_body;

	private Vector2 gravity;
	private float grav_const = 0.1f;
	private float vconst = 0.6f;


	// Use this for initialization
	void Start () {
		cam_obj.transform.position = new Vector3(player_obj.transform.position.x, player_obj.transform.position.y, cam_obj.transform.position.z);
		player_body = player_obj.GetComponent<Rigidbody2D> ();
	}
	
	// FixedUpdate is called once every 0.02 secs
	void FixedUpdate () {
		gravity = CalcGravity (planet_obj, player_obj);
		DrawVector (gravity, Color.blue, player_obj);
		player_body.velocity += gravity;
		CheckInputSetVelocity ();

		cam_obj.transform.position = new Vector3(player_obj.transform.position.x, player_obj.transform.position.y, cam_obj.transform.position.z);	
	}

	private Vector2 CalcGravity(GameObject m1, GameObject m2) {
		gravity = m1.transform.position - m2.transform.position; 
		gravity.Normalize();
		gravity = gravity * grav_const;

		return gravity;
	}

	private void CheckInputSetVelocity() {
		//Get inputs, move player
		float input_h = vconst * Input.GetAxisRaw ("Horizontal");
		float input_v = 2 * Input.GetAxisRaw ("Vertical");

		//away from planet surface direction, and along surface direction
		Vector2 away = -gravity;
		Vector2 along = new Vector2(away.y, -away.x);

		player_body.velocity = player_body.velocity + input_h * along + input_v * away; 

		//Rotate camera
		Vector2 cam_up = cam_obj.transform.up;
		cam_obj.transform.up = away;

		//Get scrollwheel input, set cam size
		Camera cam = cam_obj.GetComponent<Camera> ();
		float input_scroll = Input.GetAxis ("Mouse ScrollWheel");

		float size = cam.orthographicSize;
		size = input_scroll < 0 ? size + 0.5f : size;
		size = input_scroll > 0 ? size - 0.5f : size;
		size = size > 20 ? 20 : size;
		size = size < 1 ? 1 : size;
		cam.orthographicSize = size;

		//Debugging 
		DrawVector (away, Color.red, player_obj);
		DrawVector (along, Color.green, player_obj);
		DrawVector (cam_up/5, Color.cyan, cam_obj);
	}

	private void DrawVector(Vector2 vector, Color col, GameObject origin) {
		Debug.DrawLine (origin.transform.position, (Vector2) origin.transform.position + 5*vector, col);
	}
}

