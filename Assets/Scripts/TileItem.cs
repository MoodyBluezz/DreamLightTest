using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    [field: SerializeField] public TextMeshProUGUI Number { get; private set; }
    [field: SerializeField] public TextMeshProUGUI Label { get; private set; }

    public event Action<TileItem> DragEnded;
    public int ScrollRectCount { get; private set; }

    private UITiles _uiTile;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
    }

	public void OnPointerUp(PointerEventData eventData)
	{
		_canvasGroup.blocksRaycasts = true;
	}

	public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _uiTile.Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;

        var dropTarget = eventData.pointerEnter;

        if (dropTarget == null) return;

        var targetUITile = dropTarget.GetComponentInParent<UITiles>();

        if (targetUITile == null) return;

        ScrollRectCount = targetUITile.ScrollRect.content.childCount;
        transform.SetParent(targetUITile.ScrollRect.content);
        InitParent(ref targetUITile);
        DragEnded?.Invoke(this);
    }

    public void InitParent(ref UITiles tiles)
    {
        _uiTile = tiles;
    }
}