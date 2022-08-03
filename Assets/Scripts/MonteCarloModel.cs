using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

public class MonteCarloModel : MonoBehaviour
{
	[SerializeField] private SpriteRenderer pointPrefab;

	[HideInInspector] public FloatReactiveProperty waitTime = new FloatReactiveProperty(0.05f);

	[HideInInspector] public BoolReactiveProperty isRunning = new BoolReactiveProperty(true);

	public IReadOnlyReactiveCollection<Vector2> Points => _points;
	private readonly ReactiveCollection<Vector2> _points = new ReactiveCollection<Vector2>();

	public double CurrentPI => 4.0 * InCircleCount / _points.Count;
	public double RealPIDiff => Math.Abs(CurrentPI - Math.PI);

	/// <summary>
	/// 円の中に存在している点の個数
	/// </summary>
	private int InCircleCount => _points.Count(p => p.magnitude <= 1.0f);

	private void Start()
	{
		var token = this.GetCancellationTokenOnDestroy();
		Solution(token).Forget();
	}

	private async UniTask Solution(CancellationToken token)
	{
		token.ThrowIfCancellationRequested();
		while (isRunning.Value)
		{
			var newPoint = new Vector2(Random.value * 2 - 1, Random.value * 2 - 1);
			CreatePoint(newPoint);
			_points.Add(newPoint);
			await UniTask.Delay(TimeSpan.FromSeconds(waitTime.Value), cancellationToken: token);
		}
	}

	private void CreatePoint(Vector2 point)
	{
		var isCircle = point.magnitude <= 1.0f;
		SpriteRenderer newPoint = Instantiate(pointPrefab, point, Quaternion.identity);
		newPoint.transform.parent = this.transform;
		if (isCircle)
		{
			newPoint.color = Color.red;
			;
		}
	}
}