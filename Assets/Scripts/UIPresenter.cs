using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIPresenter : MonoBehaviour
{
	[SerializeField] private MonteCarloModel monteCarloModel;
	[SerializeField] private UIView uiView;

	void Start()
	{
		uiView.SliderValue.Subscribe(sliderValue => monteCarloModel.waitTime.Value = sliderValue).AddTo(this);
		monteCarloModel.Points.ObserveAdd().Subscribe(_ =>
		{
			uiView.SetPIText($"{monteCarloModel.CurrentPI}");
			uiView.SetCountText($"{monteCarloModel.Points.Count}");
			uiView.SetDiffText($"{monteCarloModel.RealPIDiff}");
		}).AddTo(this);
	}
}