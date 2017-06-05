using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {

    public Slider slider;
	public float updateInterval = 0.1f;

	private Text text;

	void Start()
	{
		text = GetComponent<Text>();
		StartCoroutine(UpdateText());
	}

	IEnumerator UpdateText()
	{
		while (true)
		{
			text.text = string.Format("Thrust\n{0:0}", slider.value);
			yield return new WaitForSeconds(updateInterval);
		}
	}
}
