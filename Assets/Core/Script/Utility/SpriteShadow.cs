using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteShadow : MonoBehaviour {
	public float shadowScale = 0.1f;
	public Color shadowColor = new Color(0, 0, 0, 0.5f);
	

	protected SpriteRenderer mSourceRenderer;		// the real body of the shadow
	protected SpriteRenderer mShadowRenderer;

	protected float mMaxShadowAlpha;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		mShadowRenderer = GetComponent<SpriteRenderer>();
		if(mShadowRenderer == null) {
			mShadowRenderer = gameObject.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		}
		mShadowRenderer.color = shadowColor;
		mMaxShadowAlpha = shadowColor.a;

		Vector3 localScale = transform.localScale;
		localScale.y = -shadowScale;
		transform.localScale = localScale;
	}

	// Use this for initialization
	void Start () {
		mSourceRenderer = transform.parent.GetComponent<SpriteRenderer>();

		//Debug.Log("OriginRenderer=" + originRenderer);
		//Debug.Log("shadowRenderer=" + shadowRenderer.ToString());
	}

	void UpdateAlpha(float a) {
		if(a >= mMaxShadowAlpha) {
			a = mMaxShadowAlpha;
		}
		Color color = mShadowRenderer.color;
		color.a = a; 
		mShadowRenderer.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		mShadowRenderer.sprite = mSourceRenderer.sprite;
		UpdateAlpha(mSourceRenderer.color.a);

	}
}
