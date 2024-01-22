using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UISort : MonoBehaviour
{
    [SerializeField] private UITiles _uiTiles;
	[SerializeField] private Toggle _numberSortToggle;
	[SerializeField] private Toggle _stringSortToggle;

	private void OnEnable()
	{
		_numberSortToggle.onValueChanged.AddListener(delegate { SortTilesByNumbers(_numberSortToggle); });
		_stringSortToggle.onValueChanged.AddListener(delegate { SortTilesByString(_stringSortToggle); });
	}

	private void OnDisable()
	{
		_numberSortToggle.onValueChanged.RemoveListener(delegate { SortTilesByNumbers(_numberSortToggle); });
		_stringSortToggle.onValueChanged.RemoveListener(delegate { SortTilesByString(_stringSortToggle); });
	}

	private void SortTilesByNumbers(Toggle toggle)
	{
		List<TileItem> tileItems = _uiTiles.ScrollRect.content.GetComponentsInChildren<TileItem>().ToList();

		if (!toggle.isOn)
			BubbleSort(tileItems, (tile1, tile2) => tile1.Number.text.CompareTo(tile2.Number.text));
		else
			BubbleSort(tileItems, (tile1, tile2) => tile2.Number.text.CompareTo(tile1.Number.text));

		RepositionTiles(tileItems);
	}

	private void SortTilesByString(Toggle toggle)
	{
		List<TileItem> tileItems = _uiTiles.ScrollRect.content.GetComponentsInChildren<TileItem>().ToList();

		if (toggle.isOn)
		{
			BubbleSort(tileItems, (tile1, tile2) =>
			{
				return string.Compare(tile1.Label.text, tile2.Label.text);
			});
		}
		else
		{
			BubbleSort(tileItems, (tile1, tile2) =>
			{
				return string.Compare(tile2.Label.text, tile1.Label.text);
			});
		}

		RepositionTiles(tileItems);
	}

	private void BubbleSort(List<TileItem> tiles, Comparison<TileItem> comparison)
	{
		int n = tiles.Count;
		bool swapped;

		do
		{
			swapped = false;

			for (int i = 1; i < n; i++)
			{
				if (comparison(tiles[i - 1], tiles[i]) > 0)
				{
					var temp = tiles[i - 1];
					tiles[i - 1] = tiles[i];
					tiles[i] = temp;

					swapped = true;
				}
			}
			n--;
		} while (swapped);
	}

	private void RepositionTiles(List<TileItem> sortedTiles)
	{
		for (int i = 0; i < sortedTiles.Count; i++)
		{
			sortedTiles[i].transform.SetSiblingIndex(i);
		}
	}
}
