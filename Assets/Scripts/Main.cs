using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

	public GameObject player_obj;
	public GameObject planet_obj;

	private Rigidbody2D player_body;

	private Vector2 gravity;
	private float grav_const = 0.1f;
	private float vconst = 0.6f;


	// Use this for initialization
	void Start () {
		player_body = player_obj.GetComponent<Rigidbody2D> ();
		CalcGravity ();
	}
	
	// FixedUpdate is called once every 0.02 secs
	void FixedUpdate () {
		CalcGravity ();
		DrawVector (gravity, Color.blue);
		player_body.velocity += gravity;
		CheckInputSetVelocity ();
	}

	private void CalcGravity() {
		gravity = planet_obj.transform.position - player_obj.transform.position; 
		gravity.Normalize();
		gravity = gravity * grav_const;
	}

	private void CheckInputSetVelocity() {
		float input_h = vconst * Input.GetAxisRaw ("Horizontal");
		float input_v = 2 * Input.GetAxisRaw ("Vertical");

		Vector2 away = -gravity;
		DrawVector (away, Color.red);
		Vector2 along = new Vector2(away.y, -away.x);
		DrawVector (along, Color.green);

		player_body.velocity = player_body.velocity + input_h * along + input_v * away; 
		Debug.Log (input_v);
	}

	private void DrawVector(Vector2 vector, Color col) {
		Debug.DrawLine (player_obj.transform.position, (Vector2) player_obj.transform.position+5*vector, col);
	}
}

