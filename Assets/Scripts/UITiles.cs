using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UITiles : MonoBehaviour
{
    [SerializeField] private TileItem _tileItem;
    [SerializeField] private Transform _tileContent;

    [field: SerializeField] public ScrollRect ScrollRect { get; private set; }
    [field: SerializeField] public Canvas Canvas;

    public event Action<int> TileValueChanged;

    private List<TileData> _tiles = new();

    private void Awake()
    {
        string tileDataPath = Path.Combine(Application.streamingAssetsPath, $"TileData.json");

        if (File.Exists(tileDataPath))
            LoadTileData(tileDataPath);
        else
            CreateTileData(tileDataPath);

        SetTileData();
    }

    private void SetTileData()
    {
        var tiles = this;

        foreach (var tile in _tiles)
        {
            var tileItem = Instantiate(_tileItem, ScrollRect.content);
            tileItem.Number.text = tile.value.ToString();
            tileItem.Label.text = tile.label;
            tileItem.InitParent(ref tiles);
            tileItem.DragEnded += OnTileEndDrag;
        }
    }

    private void OnTileEndDrag(TileItem item)
    {
        TileValueChanged?.Invoke(item.ScrollRectCount);
    }

    private void CreateTileData(string path)
    {
        for (int i = 0; i <= 10; i++)
        {
            TileData tileData = new()
            {
                label = "Label",
                value = i
            };
            _tiles.Add(tileData);
        }

        JsonData.SaveData(_tiles, path);
    }

    private void LoadTileData(string path)
    {
        _tiles = JsonData.LoadData(path);
    }

    private void OnApplicationQuit()
    {
        var tileItems = ScrollRect.content.GetComponentsInChildren<TileItem>();
        foreach (var item in tileItems)
        {
            item.DragEnded -= OnTileEndDrag;
        }
    }

    public int GetCountOfTiles()
    {
        return _tileContent.childCount;
    }
}