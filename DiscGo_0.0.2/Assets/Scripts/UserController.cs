using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class UserController : MonoBehaviour
{
	public float speed;
    public Boundary boundary;

	public GameObject scoreSound;
	public GameObject lifeSound;
	public GameObject damageSound;

	private Rigidbody rb;
	private AudioSource holeAudio;

    void Start()
    {
		scoreSound.gameObject.SetActive (false);
		lifeSound.gameObject.SetActive (false);
		damageSound.gameObject.SetActive (false);

		rb = GetComponent<Rigidbody>();
		holeAudio = GetComponent<AudioSource>();
    }


    void Update()
    {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(-moveHorizontal, moveVertical, 0.0f);
            rb.velocity = movement * speed;
            rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
                16.83f
            );
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Win_Boundary"))
        {
			holeAudio.Play ();
            GameController.controller.score += 10;
            GameController.controller.safeguard += 1;
            GameController.controller.SetScoreText();
        }

        if (other.gameObject.CompareTag("Lose_Boundary"))
        {

            if (GameController.controller.safeguard > 0)
            {
                GameController.controller.safeguard -= 1;
            }

            else if (GameController.controller.safeguard == 0)
            {
				StartCoroutine (damageFX());
                    GameController.controller.lives -= 1;
                    GameController.controller.SetLivesText();
                    GameController.controller.SetScoreText();
            }
        }

        if (other.gameObject.CompareTag("LifePU"))
        {
			Destroy (other.gameObject);
			StartCoroutine (lifeFX());
            GameController.controller.lives += 1;
            GameController.controller.SetLivesText();
        }

		if (other.gameObject.CompareTag("Points"))
		{
			Destroy (other.gameObject);
			StartCoroutine (pointsFX ());
			GameController.controller.score += 15;
            GameController.controller.SetScoreText();
		}
        
    }

	IEnumerator pointsFX ()
	{
		scoreSound.gameObject.SetActive (true);
		yield return new WaitForSeconds (2);
		scoreSound.gameObject.SetActive (false);
	}

	IEnumerator lifeFX ()
	{
		lifeSound.gameObject.SetActive (true);
		yield return new WaitForSeconds (5);
		lifeSound.gameObject.SetActive (false);
	}

	IEnumerator damageFX ()
	{
		damageSound.gameObject.SetActive (true);
		yield return new WaitForSeconds (1.5f);
		damageSound.gameObject.SetActive (false);
	}
}
