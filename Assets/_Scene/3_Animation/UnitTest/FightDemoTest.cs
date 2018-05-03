using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class FightDemoTest : BaseTest {
	[Header("Team Formation")]
	public BattleModel[] leftTeam;
	public BattleModel[] rightTeam;

	[Header("Projectile Prefab")]
	public GameObject[] projectilePrefab;


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

		BattleModel actor, target;


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

	public void AttackFromTeam(SequenceAction sequence, BattleModel[] attackTeam, BattleModel[] targetTeam) {
		for(int i=0; i<attackTeam.Length; i++) {
			BattleModel actor = attackTeam[i];
			BattleModel target = targetTeam[i];

			short style = (short) Random.Range(0, 2);

			AnimeAction attackAttack = CreateAttackAction(actor, target, style, 
							true, CreateHitDamageAction(target, slashEffect[0]));
			sequence.AddAction(attackAttack);
			//for(BattleModel actor in attackTeam) {	
		}
	}

	
	#region Action Creation Helper
	public AnimeAction CreateHitDamageAction(BattleModel targetModel, 
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


	public ModelAttackAction CreateAttackAction(BattleModel actor, BattleModel target, 
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
