using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class BattleEffectTest : BaseTest {
	public BattleEffect testEffect;
	public GameObject effectPrefab;
	public GameObject[] allEffectPrefab;
	public GameObject projectilePrefab;

	public int testEffectIndex = 0;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		ShowScreenLog();

	}

	[Test]
	public void PlayOnce()
	{
		UpdateLog("Test PlayOnce");
		testEffect.PlayOnce(
			() => { AppendLog("OnEnd"); },
			() => { AppendLog("OnHit"); }
		);
	}

	[Test]
	public void PlayRepeat()
	{
		testEffect.PlayRepeat(5, 
			() => { AppendLog("OnEnd"); },
			() => { AppendLog("OnHit"); }
		);
	}

	[Test]
	public void Projectile()
	{
		Vector3 from = new Vector3(-3, 2, 0);
		Vector3 to = new Vector3(3, 2, -1);
		testEffect.Move(from, to, 1.0f, 
			() => { AppendLog("OnEnd"); }
		);
	}

	[Test]
	public void TestEffect()
	{
		GameObject effectPrefab = allEffectPrefab[testEffectIndex];
		UpdateLog("Test Effect: effectPrefab=" + effectPrefab.name);
		// Vector3 from = new Vector3(-3, 2, 0);
		// Vector3 to = new Vector3(3, 2, -1);
		// testEffect.Move(from, to, 1.0f, 
		// 	() => { AppendLog("OnEnd"); }
		// );
		GameObject obj = GameObject.Instantiate(effectPrefab);
		obj.transform.position = Vector3.zero;
		Effect effect = obj.GetComponent<Effect>();
		effect.PlayOnce(
			() => { AppendLog("OnEnd");  GameObject.Destroy(obj); },
			() => { AppendLog("OnHit"); }
		);
	}

	[Test]
	public void SpawnEffect()
	{
		UpdateLog("Test SpawnEffect");
		// Vector3 from = new Vector3(-3, 2, 0);
		// Vector3 to = new Vector3(3, 2, -1);
		// testEffect.Move(from, to, 1.0f, 
		// 	() => { AppendLog("OnEnd"); }
		// );
		GameObject obj = GameObject.Instantiate(effectPrefab);
		obj.transform.position = Vector3.zero;
		Effect effect = obj.GetComponent<Effect>();
		effect.PlayOnce(
			() => { AppendLog("OnEnd");  GameObject.Destroy(obj); },
			() => { AppendLog("OnHit"); }
		);
	}

	[Test]
	public void SpawnProjectile()
	{
		UpdateLog("Test SpawnProjectile");
		
		Vector3 from = new Vector3(-3, 2, 0);
		Vector3 to = new Vector3(3, 2, -1);
		

		GameObject obj = GameObject.Instantiate(projectilePrefab);
		obj.transform.position = from;
		
		Effect effect = obj.GetComponent<Effect>();

		effect.Move(from, to, 1.0f, 
			() => { 
				AppendLog("OnEnd"); 
				GameObject.Destroy(obj); 
			}
		);
	}
}
