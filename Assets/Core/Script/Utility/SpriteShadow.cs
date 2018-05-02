using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteShadow : MonoBehaviour {
	public float shadowScale = 0.1f;
	public Color shadowColor = new Color(0, 0, 0, 0.5f);

	protected SpriteRenderer originRenderer;
	protected SpriteRenderer shadowRenderer;



	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		shadowRenderer = GetComponent<SpriteRenderer>();
		if(shadowRenderer == null) {
			shadowRenderer = gameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		}
		shadowRenderer.color = shadowColor;

		Vector3 localScale = transform.localScale;
		localScale.y = -shadowScale;
		transform.localScale = localScale;
	}

	// Use this for initialization
	void Start () {
		originRenderer = transform.parent.GetComponent<SpriteRenderer>();

		//Debug.Log("OriginRenderer=" + originRenderer);
		//Debug.Log("shadowRenderer=" + shadowRenderer.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		shadowRenderer.sprite = originRenderer.sprite;
	}
}
