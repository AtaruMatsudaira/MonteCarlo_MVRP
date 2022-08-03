using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
	[SerializeField] private Slider timeSlider;
	[SerializeField] private Text countText;
	[SerializeField] private Text piText;
	[SerializeField] private Text diffText;

	public System.IObservable<float> SliderValue => timeSlider.OnValueChangedAsObservable();
	
	public void SetCountText(string str)
	{
		countText.text = str;
	}

	public void SetPIText(string str)
	{
		piText.text = str;
	}

	public void SetDiffText(string str)
	{
		diffText.text = str;
	}
}