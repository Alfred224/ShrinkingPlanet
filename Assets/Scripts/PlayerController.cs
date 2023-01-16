using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10f;
	public float rotationSpeed = 10f;
	public TextMeshProUGUI fpsCounter;
	public GameObject deathEffect;

	private float rotation;
	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		StartCoroutine(FPSCounter());
	}

	void Update ()
	{
		rotation = Input.GetMouseButton(0) ? Input.GetAxisRaw("Mouse X") : 0;
	}

	void FixedUpdate ()
	{
		rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
		Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
		Quaternion deltaRotation = Quaternion.Euler(yRotation);
		Quaternion targetRotation = rb.rotation * deltaRotation;
		rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.deltaTime));
		//transform.Rotate(0f, rotation * rotationSpeed * Time.fixedDeltaTime, 0f, Space.Self);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Meteor")
		{
			Instantiate(deathEffect, transform.position, transform.rotation);
			GameManager.instance.EndGame();

			AudioManager.instance.Play("PlayerDeath");

			Destroy(gameObject);
		}
	}

	IEnumerator FPSCounter()
    {
		fpsCounter.text = $"FPS: {Mathf.Round(1 / Time.deltaTime)}";
		yield return new WaitForSeconds(1);
		StartCoroutine(FPSCounter());
    }

}
