using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class EffectActionTest : BaseTest {
	public AnimeActionManager actionManager;
	public GameObject effectPrefab;
	public GameObject projectilePrefab;
	public GameObject particlePrefab;

	[Test]
	public void PointEffect()
	{
		EffectAction effectAction = EffectAction.CreatePointEffect(effectPrefab, Vector3.zero);

		actionManager.RunAction(effectAction);
	}

	[Test]
	public void ParticleEffect()
	{
		EffectAction effectAction = EffectAction.CreatePointEffect(particlePrefab, Vector3.zero);

		actionManager.RunAction(effectAction);
	}


	[Test]
	public void ProjectileEffect()
	{
		Vector3 from = new Vector3(3, 2, 0);
		Vector3 to = new Vector3(-2, 3, 4);
		float duration = 1.0f;

		EffectAction effectAction = EffectAction.CreateProjectileEffect(projectilePrefab, from, to, duration);

		actionManager.RunAction(effectAction);
	}

	[Test]
	public void EffectCloud()
	{
		ParallelAction parallel = new ParallelAction();

		int effectCount = 30;

		for(int i=0; i<effectCount; i++) {
			float x = Random.Range(-4f, 4f);
			float y = Random.Range(-2f, 2f);

			Vector3 spawnPos = new Vector3(x, y, -5);
			EffectAction effectAction = EffectAction.CreatePointEffect(effectPrefab, spawnPos, null, 3);
			parallel.AddAction(effectAction);
		}

		actionManager.RunAction(parallel);
	}
}
