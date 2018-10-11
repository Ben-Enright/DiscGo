using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Colors : MonoBehaviour {

    public GameObject disc;
    public GameObject bg;

	private Renderer discRenderer;
	private Renderer bgRenderer;

	private int discColor;
	private int bgColor;


    void Start() {

        bgRenderer = bg.GetComponent<Renderer>();
        discRenderer = disc.GetComponent<Renderer>();

        StartCoroutine(ColorSwitcher());

    }

    IEnumerator ColorSwitcher() {

		discColor = Random.Range(1, 21);
        bgColor = Random.Range(1, 21);

        //Change Disc Color
        switch (discColor)
        {
            case 1:
                discRenderer.sharedMaterial.color = Color.red;
                break;
            case 2:
                discRenderer.sharedMaterial.color = Color.yellow;
                break;
            case 3:
                discRenderer.sharedMaterial.color = Color.blue;
                break;
            case 4:
                discRenderer.sharedMaterial.color = Color.green;
                break;
            case 5:
                discRenderer.sharedMaterial.color = Color.magenta;
                break;
            case 6:
                discRenderer.sharedMaterial.color = Color.cyan;
                break;
            case 7:
                discRenderer.sharedMaterial.color = Color.white;
                break;
            case 8:
                discRenderer.sharedMaterial.color = Color.black;
                break;
            case 9:
                discRenderer.sharedMaterial.color = Color.yellow + Color.red;
                break;
            case 10:
                discRenderer.sharedMaterial.color = Color.green + Color.yellow;
                break;
            case 11:
                discRenderer.sharedMaterial.color = Color.green + Color.blue;
                break;
            case 12:
                discRenderer.sharedMaterial.color = Color.blue + Color.red;
                break;
            case 13:
                discRenderer.sharedMaterial.color = Color.blue + Color.magenta;
                break;
            case 14:
                discRenderer.sharedMaterial.color = Color.red + Color.magenta;
                break;
            case 15:
                discRenderer.sharedMaterial.color = Color.white + Color.blue;
                break;
            case 16:
                discRenderer.sharedMaterial.color = Color.white + Color.red;
                break;
            case 17:
                discRenderer.sharedMaterial.color = Color.white + Color.green;
                break;
            case 18:
                discRenderer.sharedMaterial.color = Color.white + Color.yellow;
                break;
            case 19:
                discRenderer.sharedMaterial.color = Color.white + Color.cyan;
                break;
            case 20:
                discRenderer.sharedMaterial.color = Color.white + Color.magenta;
                break;
            default:
                break;
        }

        //Change BG Color
        switch (bgColor)
        {
            case 1:
                bgRenderer.material.color = Color.red;
                break;
            case 2:
                bgRenderer.material.color = Color.yellow;
                break;
            case 3:
                bgRenderer.material.color = Color.blue;
                break;
            case 4:
                bgRenderer.material.color = Color.green;
                break;
            case 5:
                bgRenderer.material.color = Color.magenta;
                break;
            case 6:
                bgRenderer.material.color = Color.cyan;
                break;
            case 7:
                bgRenderer.material.color = Color.white;
                break;
            case 8:
                bgRenderer.material.color = Color.black;
                break;
            case 9:
                bgRenderer.material.color = Color.yellow + Color.red;
                break;
            case 10:
                bgRenderer.material.color = Color.green + Color.yellow;
                break;
            case 11:
                bgRenderer.material.color = Color.green + Color.blue;
                break;
            case 12:
                bgRenderer.material.color = Color.blue + Color.red;
                break;
            case 13:
                bgRenderer.material.color = Color.blue + Color.magenta;
                break;
            case 14:
                bgRenderer.material.color = Color.red + Color.magenta;
                break;
            case 15:
                bgRenderer.material.color = Color.white + Color.blue;
                break;
            case 16:
                bgRenderer.material.color = Color.white + Color.red;
                break;
            case 17:
                bgRenderer.material.color = Color.white + Color.green;
                break;
            case 18:
                bgRenderer.material.color = Color.white + Color.yellow;
                break;
            case 19:
                bgRenderer.material.color = Color.white + Color.cyan;
                break;
            case 20:
                bgRenderer.material.color = Color.white + Color.magenta;
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ColorSwitcher());
    }
}