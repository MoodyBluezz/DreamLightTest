using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour
{
	[SerializeField] private UITiles _uiTilesFirst;
	[SerializeField] private UITiles _uiTilesSecond;
	[SerializeField] private TextMeshProUGUI _uiLabelOne;
	[SerializeField] private TextMeshProUGUI _uiLabelTwo;

	private void Start()
	{
		UpdateTileCounter(_uiTilesFirst.GetCountOfTiles(), _uiTilesSecond.GetCountOfTiles());
	}

	private void OnEnable()
	{
		_uiTilesFirst.TileValueChanged += OnTileEndDrag;
		_uiTilesSecond.TileValueChanged += OnTileEndDrag;
	}

	private void OnDisable()
	{
		_uiTilesFirst.TileValueChanged -= OnTileEndDrag;
		_uiTilesSecond.TileValueChanged -= OnTileEndDrag;
	}

	private void OnTileEndDrag(int scrollRectCount)
	{
		UpdateTileCounter(_uiTilesFirst.GetCountOfTiles(), _uiTilesSecond.GetCountOfTiles());
	}

	private void UpdateTileCounter(int firstCounter, int secondCounter)
	{
		_uiLabelOne.text = $"Count: {firstCounter}";
		_uiLabelTwo.text = $"Count: {secondCounter}";
	}
}
