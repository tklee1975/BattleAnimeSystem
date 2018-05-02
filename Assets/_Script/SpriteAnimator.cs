using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference:
//		https://pastebin.com/icV6tA64

using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour
{
	[System.Serializable]
	public class AnimationTrigger
	{
		public string name;
		public int frame;
	}

	[System.Serializable]
	public class Animation
	{
		public string name;
		public int fps;
		public Sprite[] frames;

		public string sequenceCode;
		public string cue;

		public AnimationTrigger[] triggers;
	}

	// sequence code format:
	// startFrame-endFrame:time(chance)
	// time: also be set to "forever" - this will loop the sequence indefinitely
	// chance: float value from 0-1, chance that the sequence will play (if not played, it will be skipped)
	// time and chance can both be ignored, this will mean the sequence plays through once

	// sequence code examples:
	// TV: 0-1:3, 2-3:3, 4-5:4, 6-7:4, 8:3, 9:3
	// Idle animation with random fidgets: 0-59, 60-69, 10-59, 0-59(.25), 70-129(.75)
	// Jump animation with looping finish: 0-33, 20-33:forever

	public SpriteRenderer spriteRenderer;
	public Animation[] animations;

	public bool playing { get; private set; }
	public Animation currentAnimation { get; private set; }
	public int currentFrame { get; private set; }
	[HideInInspector]
	public bool loop;
	public float speedMultiplier = 1f;

	public string playAnimationOnStart;

	bool looped;

	void Start()
	{
		if (!spriteRenderer)
			spriteRenderer = GetComponent<SpriteRenderer>();

		if (playAnimationOnStart != "")
			Play(playAnimationOnStart);
	}
	
	void OnDisable()
	{
		playing = false;
		currentAnimation = null;
	}

	public void Play(string name, bool loop = true, int startFrame = 0)
	{
		Animation animation = GetAnimation(name);
		if (animation != null )
		{
			if (animation != currentAnimation)
			{
				ForcePlay(name, loop, startFrame);
			}
		}
		else
		{
			Debug.LogWarning("could not find animation: " + name);
		}
	}

	public void ForcePlay(string name, bool loop = true, int startFrame = 0)
	{
		Animation animation = GetAnimation(name);
		if (animation != null)
		{
			this.loop = loop;
			currentAnimation = animation;
			playing = true;
			currentFrame = startFrame;
			spriteRenderer.sprite = animation.frames[currentFrame];
			StopAllCoroutines();
			StartCoroutine(PlayAnimation(currentAnimation));
		}
		else
		{
			Debug.LogWarning("Could not find animation: " + name);
		}
	}

	public void SlipPlay(string name, int wantFrame, params string[] otherNames)
	{
		for (int i = 0; i < otherNames.Length; i++)
		{
			if (currentAnimation != null && currentAnimation.name == otherNames[i])
			{
				Play(name, true, currentFrame);
				break;
			}
		}
		Play(name, true, wantFrame);
	}

	public bool IsPlaying(string name)
	{
		return (currentAnimation != null && currentAnimation.name == name);
	}

	public Animation GetAnimation(string name)
	{
		foreach (Animation animation in animations)
		{
			if (animation.name == name)
			{
				return animation;
			}
		}
		return null;
	}

	IEnumerator CueAnimation(string animationName, float minTime, float maxTime)
	{
		yield return new WaitForSeconds(Random.Range(minTime, maxTime));
		ForcePlay(animationName, false);
	}

	IEnumerator PlayAnimation(Animation animation)
	{
		playing = true;

		speedMultiplier = 1f;
		//Debug.Log("Playing animation: " + animation.name);

		float timer = 0f;
		float delay = 1f / (float)animation.fps;
		string cueOnComplete = "";

		if (animation.cue != null && animation.cue != "")
		{
			if (animation.cue.IndexOf(':') != -1)
			{
				string[] dataBits = animation.cue.Trim().Split(':');

				string animationName = dataBits[1];
				dataBits = dataBits[0].Split('-');
				
				float minTime = float.Parse(dataBits[0], System.Globalization.CultureInfo.InvariantCulture);
				float maxTime = minTime;
				
				if (dataBits.Length > 1)
					maxTime = float.Parse(dataBits[1], System.Globalization.CultureInfo.InvariantCulture);
				
				StartCoroutine(CueAnimation(animationName, minTime, maxTime));

				loop = true;
			}
			else
			{
				cueOnComplete = animation.cue.Trim();
			}
		}

		if (animation.sequenceCode != null && animation.sequenceCode != "")
		{
			while (true)
			{
				string[] split = animation.sequenceCode.Split(',');
				for (int i = 0; i < split.Length; i++)
				{
					string data = split[i].Substring(0, split[i].Length);
					float duration = 0f;
					float chance = 1f;
					string[] dataBits;

					if (data.IndexOf('(') != -1)
					{
						int startIndex = data.IndexOf('(');
						int endIndex = data.IndexOf(')');
						string chanceString = data.Substring(startIndex+1, endIndex - (startIndex+1));
						chance = float.Parse(chanceString, System.Globalization.CultureInfo.InvariantCulture);
						data = data.Substring(0, startIndex);
					}

					if (Random.value > chance)
						continue;

					bool readFrames = true;

					if (data.IndexOf(':') != -1)
					{
						dataBits = data.Trim().Split(':');
						if (dataBits[0] == "fps")
						{
							readFrames = false;
						}
						else if (dataBits[0] == "goto")
						{
							readFrames = false;
						}
						else if (dataBits[0] == "label")
						{
							readFrames = false;
						}
						else
						{
							if (dataBits.Length > 1)
							{
								if (dataBits[1] == "forever")
									duration = -1f;
								else
									duration = float.Parse(dataBits[1], System.Globalization.CultureInfo.InvariantCulture);
							}

							dataBits = dataBits[0].Split('-');
						}
					}
					else
					{
						dataBits = data.Trim().Split('-');
					}

					if (readFrames)
					{
						int startFrame = -1;
						int endFrame = -1;

						startFrame = int.Parse(dataBits[0], System.Globalization.CultureInfo.InvariantCulture);
						endFrame = startFrame;

						if (dataBits.Length > 1)
							endFrame = int.Parse(dataBits[1], System.Globalization.CultureInfo.InvariantCulture);

						currentFrame = startFrame;

						//Debug.Log ("startFrame: " + startFrame + " endFrame: " + endFrame + " duration: " + duration);

						if (duration <= 0f)
						{
							while (duration < 0f || currentFrame < endFrame)
							{
								while (timer < delay)
								{
									timer += Time.deltaTime * speedMultiplier;
									yield return null;
								}
								
								while (timer >= delay)
								{
									timer -= delay;
									NextFrame(animation);
								}
								
								spriteRenderer.sprite = animation.frames[currentFrame];
							}
						}
						else
						{
							while (duration > 0f)
							{
								while (timer < delay)
								{
									duration -= Time.deltaTime * speedMultiplier;
									timer += Time.deltaTime * speedMultiplier;
									yield return null;
								}
								while (timer >= delay)
								{
									timer -= delay;
									currentFrame++;
									if (currentFrame > endFrame)
										currentFrame = startFrame;
								}

								spriteRenderer.sprite = animation.frames[currentFrame];
							}
						}
					}
				}
				//Debug.LogWarning("cueOnComplete: " + cueOnComplete);
				if (cueOnComplete != "")
					ForcePlay(cueOnComplete, loop);
			}
		}
		else
		{
			while (loop || currentFrame < animation.frames.Length-1)
			{
				while (timer < delay)
				{
					timer += Time.deltaTime * speedMultiplier;
					yield return null;
				}

				while (timer >= delay)
				{
					timer -= delay;
					NextFrame(animation);
				}

				spriteRenderer.sprite = animation.frames[currentFrame];
			}
			if (cueOnComplete != "")
				ForcePlay(cueOnComplete, loop);
		}

		currentAnimation = null;
		playing = false;
	}

	void NextFrame(Animation animation)
	{
		looped = false;
		currentFrame++;
		foreach (AnimationTrigger animationTrigger in animation.triggers)
		{
			if (animationTrigger.frame == currentFrame)
			{
				gameObject.SendMessageUpwards(animationTrigger.name);
			}
		}

		if (currentFrame >= animation.frames.Length)
		{
			if (loop)
				currentFrame = 0;
			else
				currentFrame = animation.frames.Length - 1;
		}
	}

	public int GetFacing()
	{
		return (int)Mathf.Sign(spriteRenderer.transform.localScale.x);
	}

	public void FlipTo(float dir)
	{
		if (dir < 0f)
			spriteRenderer.transform.localScale = new Vector3(-1f, 1f, 1f);
		else
			spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	public void FlipTo(Vector3 position)
	{
		float diff = position.x - transform.position.x;
		if (diff < 0f)
			spriteRenderer.transform.localScale = new Vector3(-1f, 1f, 1f);
		else
			spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
	}
}