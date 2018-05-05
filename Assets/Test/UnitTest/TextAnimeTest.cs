using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class TextAnimeTest : BaseTest {
	public ITextAnimePlayer testPlayer;
	public GameObject testObject;

	public BattleText damageText;
	public BattleText buffText;

	public GameObject prefab;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		testPlayer = new TextAnimePlayerAnimator(testObject);

		ShowScreenLog();
	}

	[Test]
	public void PlayerTest()
	{
		UpdateLog("Damage Animation");
		testPlayer.SetText("123456");
		testPlayer.Play(GameText.Style.Damage, () => {
			AppendLog("End of Animation");
		});
	}

	[Test]
	public void DamageText()
	{
		UpdateLog("Damage Text");
		damageText.Play(() => {
			AppendLog("End of Animation");
		});
	}

	[Test]
	public void BuffText()
	{
		UpdateLog("Buff Text");
		buffText.Play(() => {
			AppendLog("End of Animation");
		});
	}

	[Test]
	public void SpawnText()
	{
		UpdateLog("SpawnText");

		GameObject newObject = GameObject.Instantiate(prefab);
		newObject.transform.position = Vector3.zero;

		BattleText text = newObject.GetComponent<BattleText>();
		text.text = "10000";
		text.Play(() => {
			AppendLog("End of Animation");
		});
	}
}
