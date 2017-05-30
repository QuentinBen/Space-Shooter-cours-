using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{

	public float speed;
	public float tilt;
	public Rigidbody rb;

	public Boundary boundary;

	//SHOTS
	public GameObject shot;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire = 0.0f;

	private AudioSource shotAudio;

	void Start()
	{
		shotAudio = GetComponent<AudioSource> ();
	}

	void Update()
	{
		//Fire Primary Weapon
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			shotAudio.Play ();
		}
	}

	void FixedUpdate()
	{
		//movements
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.velocity = movement * speed;

			rb.position = new Vector3 (
				Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
			);

		//lateral rotations by movements
		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	}
}