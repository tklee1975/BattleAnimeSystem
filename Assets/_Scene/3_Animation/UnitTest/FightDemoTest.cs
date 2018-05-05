using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class FightDemoTest : BaseTest {
	[Header("Team Formation")]
	public Model[] leftTeam;
	public Model[] rightTeam;

	[Header("Projectile & Effect")]
	public GameObject[] projectilePrefab;
	public GameObject[] effectPrefab;


	[Header("Effects")]
	public AnimationClip[] skillEffect;
	public AnimationClip[] slashEffect;

	[Header("Other Setting")]
	public GameObject hitValuePrefab;
	public AnimeActionManager actionManager;


	const float zOrderVfx = -3;

	[Test]
	public void OneOnOne()
	{
		Debug.Log("###### TEST 1 ######");
		SequenceAction demoFight = new SequenceAction();

		Model actor, target;


		// Right-0 attack Left-0
		actor = rightTeam[0];  target=leftTeam[0];
		AnimeAction p1Attack = CreateAttackAction(actor, target, 0, 
							true, CreateHitDamageAction(target));
		demoFight.AddAction(p1Attack);

		// Left-0 attack Right-1
		actor = leftTeam[0];  target=rightTeam[0];
		AnimeAction p2Attack = CreateAttackAction(actor, target, 0, 
							true, CreateHitDamageAction(target, slashEffect[0]));
		demoFight.AddAction(p2Attack);

		actionManager.RunAction(demoFight);
	}

	[Test]
	public void CrowdFight()
	{
		SequenceAction demoFight = new SequenceAction();
		AttackFromTeam(demoFight, rightTeam, leftTeam);
		AttackFromTeam(demoFight, leftTeam, rightTeam);
		actionManager.RunAction(demoFight);
	}

	[Test]
	public void SkillAttack()
	{
		SequenceAction demoFight = new SequenceAction();
		Model actor, target;

		
		// -----


		AnimeAction projectileAttack;
		AnimeAction attack;

		// Right-0 attack Left-0
		actor = rightTeam[0]; 
		target = leftTeam[0];

		projectileAttack = CreateProjectileAction(actor, target, projectilePrefab[0], effectPrefab[0]);
		attack = CreateAttackAction(actor, target, 0, 
							false, projectileAttack);
		demoFight.AddAction(attack);


		// Right-1 attack Left-1
		actor = rightTeam[1]; 
		target = leftTeam[1];

		projectileAttack = CreateProjectileAction(actor, target, projectilePrefab[0], effectPrefab[0]);
		attack = CreateAttackAction(actor, target, 1, 
							false, projectileAttack);
		demoFight.AddAction(attack);

		// -----
		actionManager.RunAction(demoFight);
	}


	[Test]
	public void TestProject()
	{
		Model actor, target;
		actor = rightTeam[0]; 
		target = leftTeam[0];

		AnimeAction action = CreateProjectileAction(actor, target, projectilePrefab[0], effectPrefab[0]);

		actionManager.RunAction(action);
	}

	
	public void AttackFromTeam(SequenceAction sequence, Model[] attackTeam, Model[] targetTeam) {
		for(int i=0; i<attackTeam.Length; i++) {
			Model actor = attackTeam[i];
			Model target = targetTeam[i];

			short style = (short) Random.Range(0, 2);

			AnimeAction attackAttack = CreateAttackAction(actor, target, style, 
							true, CreateHitDamageAction(target, slashEffect[0]));
			sequence.AddAction(attackAttack);
			//for(Model actor in attackTeam) {	
		}
	}

	
	#region Action Creation Helper
	public AnimeAction CreateProjectileAction(Model actor, 
											  Model target, 
											  GameObject projectile,
											  GameObject hitEffect) 
											  {
		// Sequence: Projectile Movement, Hit Effect, ModelHit HitValue 										  
		
		SequenceAction sequence = new SequenceAction();

		Vector3 launchPos = actor.GetLaunchPosition();
		Vector3 targetCenterPos = (Vector3) target.GetCenterPosition() + new Vector3(0, 0, -5);
		Vector3 targetPos = (Vector3) target.GetOriginPosition() + new Vector3(0, 0, -5);
		float duration = 0.5f;

		// Projectile Move
		EffectAction projectileAction = EffectAction.CreateProjectileEffect(
										projectile, launchPos, targetCenterPos, duration);		
		sequence.AddAction(projectileAction);

		// Hit Effect
		EffectAction effectAction = EffectAction.CreatePointEffect(hitEffect, targetPos);
		sequence.AddAction(effectAction);

		ModelHitAction hitAction = new ModelHitAction();
		hitAction.name = "enemyHit";
		hitAction.actor = target;
		sequence.AddAction(hitAction);


		HitValueAction damageAction = new HitValueAction();
		damageAction.valueTextPrefab = hitValuePrefab;
		damageAction.hitValue = 1000;
		damageAction.position = target.transform.position + new Vector3(0, 2, -2);
		sequence.AddAction(damageAction);

		return sequence;
	}


	public AnimeAction CreateHitDamageAction(Model targetModel, 
					AnimationClip hitEffect=null) {
		SequenceAction sequence = new SequenceAction();
		sequence.name = "HitSequence";

		if(hitEffect != null) {
			SimpleAnimationAction effectAction = new SimpleAnimationAction();
			effectAction.clip = hitEffect;
			effectAction.spawnPosition = targetModel.transform.position  + new Vector3(0, 1, -2);
			sequence.AddAction(effectAction);
		}

		ModelHitAction hitAction = new ModelHitAction();
		hitAction.name = "enemyHit";
		hitAction.actor = targetModel;
		sequence.AddAction(hitAction);


		HitValueAction damageAction = new HitValueAction();
		damageAction.valueTextPrefab = hitValuePrefab;
		damageAction.hitValue = 1000;
		damageAction.position = targetModel.transform.position + new Vector3(0, 2, -2);
		sequence.AddAction(damageAction);

		return sequence;
	}


	public ModelAttackAction CreateAttackAction(Model actor, Model target, 
							short style, bool isMoving, AnimeAction onHitAction) {
		ModelAttackAction attackAction = new ModelAttackAction();
		attackAction.actor = actor;
		attackAction.style = style;
		attackAction.isMoving = isMoving;
		attackAction.targetPostion = target.GetHitPosition();
		attackAction.onHitAction = onHitAction;
		
		return attackAction;
	}

	public AnimeAction CreateProjectileAction(Vector3 startPos, Vector3 targetPos, 
					GameObject projectilePrefab, 
					AnimationClip explodeEffect) {
		ObjectMoveAction projectAction = new ObjectMoveAction();
		projectAction.startPosition = startPos + new Vector3(0, 0, zOrderVfx);;
		projectAction.endPosition = targetPos + new Vector3(0, 1, zOrderVfx);
		projectAction.objectPrefab = projectilePrefab;
		projectAction.SetDuration(0.5f);

		SimpleAnimationAction explodeAction = new SimpleAnimationAction();
		explodeAction.clip = explodeEffect;
		explodeAction.spawnPosition = targetPos + new Vector3(0, 0, zOrderVfx);
		explodeAction.repeat = 1;
		
		SequenceAction fullFireAction = new SequenceAction();
		fullFireAction.AddAction(projectAction);
		fullFireAction.AddAction(explodeAction);

		return fullFireAction;
	}

	#endregion
}
