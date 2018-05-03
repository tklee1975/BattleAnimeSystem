using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class AttackActionTest : BaseTest {
	public BattleModel hero;
	public BattleModel enemy;		// the one on the Left
	public Animator target;
	public GameObject hitValuePrefab;

	public GameObject projectilePrefab;
	public AnimationClip explodeEffect;
	public AnimationClip slashEffect;


	public AnimeActionManager actionManager;

	const float zOrderVfx = -3;

	public AnimeAction CreateTargetHitDamageAction() {
		return CreateHitDamageAction(enemy);
	}

	public AnimeAction CreateHitDamageAction(BattleModel targetModel, AnimationClip hitEffect=null) {
		SequenceAction sequence = new SequenceAction();
		sequence.name = "HitSequence";

		if(hitEffect != null) {
			SimpleAnimationAction effectAction = new SimpleAnimationAction();
			effectAction.clip = slashEffect;
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

	public ModelAttackAction CreateActorAttackAction(short style, bool isMoving, AnimeAction onHitAction) {
		return CreateAttackAction(hero, enemy, style, isMoving, onHitAction);
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

	public AnimeAction CreateProjectileAction(Vector3 startPos, Vector3 targetPos) {
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

	[Test]
	public void Melee1()
	{
		ModelAttackAction attackAction = CreateActorAttackAction(0, true, CreateTargetHitDamageAction());
		actionManager.RunAction(attackAction);
	}

	[Test]
	public void Melee2()
	{
		ModelAttackAction attackAction = CreateActorAttackAction(1, true, CreateTargetHitDamageAction());
		actionManager.RunAction(attackAction);
		
	}

	[Test]
	public void Range1()
	{
		ModelAttackAction attackAction = CreateActorAttackAction(1, false, CreateTargetHitDamageAction());
		actionManager.RunAction(attackAction);
	}

	[Test]
	public void Range2()
	{
		// Projectile Action
		SequenceAction sequence = new SequenceAction();
		AnimeAction projectileAction = CreateProjectileAction(hero.GetLaunchPosition(), target.transform.position);
		sequence.AddAction(projectileAction);
		sequence.AddAction(CreateTargetHitDamageAction());

		// 
		ModelAttackAction attackAction = CreateActorAttackAction(1, false, sequence);
		actionManager.RunAction(attackAction);	
	}

	[Test]
	public void TargetAttack()
	{
		AnimeAction targetHit = CreateHitDamageAction(hero);
		AnimeAction actorAttack = CreateAttackAction(enemy, hero, 0, true, targetHit);
		actionManager.RunAction(actorAttack);
	}


	[Test]
	public void FightDemo1()
	{
		SequenceAction demoFight = new SequenceAction();


		AnimeAction p1Attack = CreateAttackAction(enemy, hero, 0, 
							true, CreateHitDamageAction(hero));
		demoFight.AddAction(p1Attack);

		AnimeAction p2Attack = CreateAttackAction(hero, enemy, 0, 
							true, CreateHitDamageAction(enemy, slashEffect));
		demoFight.AddAction(p2Attack);

		actionManager.RunAction(demoFight);
	}
}
