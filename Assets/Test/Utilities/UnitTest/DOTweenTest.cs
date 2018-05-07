using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using DG.Tweening;

// Reference:
//		http://dotween.demigiant.com/documentation.php
public class DOTweenTest : BaseTest {
	[Header("Setting")]
	public Vector3 punchVector = Vector3.one;
	[Range(0.1f, 1f)] public float duration = 0.1f;
	[Range(1, 10)] public int vibrato = 1;
	[Range(0f, 1f)] public float elasticity = 0.5f;
	

	[Header("UI Components")]
	public Text testingText;

	public Text numberText;

	public Image testingImage;
	
	public RectTransform testPanel;

	public SpriteRenderer testSprite;

	[Test]
	public void MoveUIPanel()
	{
		float startY = 0;
		float endY = startY - 100;

		// Setup starting pos
		Vector2 startPos = testPanel.anchoredPosition;
		startPos.y = startY;
		testPanel.anchoredPosition = startPos;


//		numberText.
		//testPanel
		// Start animation (Falling from the Top)
		//testPanel.DOAnchorPosY(endY, 0.5f, true);	// No Ease
		testPanel.DOAnchorPosY(endY, 0.5f, true).SetEase(Ease.OutBack);	// Eased
	}

	[Test]
	public void TweenSprite()
	{
		testSprite.DOFade(0, 1f).SetDelay(5);
	}

	// [Test]
	// public void UpAndFade()
	// {
	// 	Transform tf = testingText.transform;
		

	// 	Vector3 newPos = testingText.transform.position + new Vector3(0, 50, 0);

	// 	// Grab a free Sequence to use
	// 	Sequence mySequence = DOTween.Sequence();
	// 	// Add a movement tween at the beginning
	// 	mySequence.Append(tf.DOMoveY(newPos.y, 1));
	// 	mySequence.Append(testingText.DOColor(Color.clear, 1));
	// 	// mySequence.Append(tf.DOMoveY(50, 1));
	// 	// 			// Add a rotation tween as soon as the previous one is finished
	// 	// 			mySequence.Append(transform.DORotate(new Vector3(0,180,0), 1));
	// 	// 			// Delay the whole Sequence by 1 second
	// 	// 			mySequence.PrependInterval(1);
	// 	// 			// Insert a scale tween for the whole duration of the Sequence
	// 	// 			mySequence.Insert(0, transform.DOScale(new Vector3(3,3,3), mySequence.Duration()));

	// 	// //Debug.Log("###### TEST 1 ######");
	// 	// 

	// 	// testingText.transform.DOMove(newPos, 1);
	// }

	// [Test]
	// public void UpAndFadeParallel()
	// {
	// 	Transform tf = testingText.transform;
		

	// 	Vector3 newPos = testingText.transform.position + new Vector3(0, 50, 0);

	// 	tf.DOMoveY(newPos.y, 1);
	// 	testingText.DOColor(Color.clear, 1);
	// }

	// [Test]
	// public void TestFadeOut() {
	// 	testingText.DOFade(0, 1);
	// 	testingImage.DOFade(0, 1);	
	// }

	// [Test]
	// public void MoveYAndFadeOut()
	// {
	// 	//Debug.Log("###### TEST 2 ######");
	// 	iconTextTween.Reset();
	// 	iconTextTween.MoveYAndFadeOut(30.0f, 1.0f);
	// }

	// [Test]
	// public void Punch() {
	// 	Transform tf = numberText.transform;
		
	// 	tf.DOPunchScale(punchVector, duration, vibrato, elasticity);

	// }

	// [Test]
	// public void ChangeText() {
	// 	Transform tf = numberText.transform;
		
	// 	numberText.DOText("200", duration, true, ScrambleMode.Numerals);

	// }

	// [Test]
	// public void ResetText() {
	// 	Transform tf = numberText.transform;
		
	// 	numberText.DOText("0", duration, true, ScrambleMode.Numerals);

	// }

	// [Test]
	// public void PopNumber() {
	// 	int from = 0;
	// 	int to = 100;

	// 	numberText.text = from.ToString();

	// 	Transform tf = numberText.transform;
	// 	numberText.DOText(to.ToString(), duration, true, ScrambleMode.Numerals);
	// 	tf.DOPunchScale(Vector3.one / 2, duration, 2, 0.5f);
	// }

	
}
