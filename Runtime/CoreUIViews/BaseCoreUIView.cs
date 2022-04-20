using System;
using UnityEditor;
using UnityEngine;

namespace CoreUI
{
	public abstract class BaseCoreUIView : MonoBehaviour
	{
		[SerializeField] private int _widthPixels;
		[SerializeField] private int _heightPixels;
		[SerializeField] private string _style;
		[SerializeField] private bool _overridePixelSize;
		[SerializeField] private float _pixelSize;
		private CoreUIElement _currentCoreUIElement;
		private BaseCoreUIView _parentCoreUIView;
		private Transform _transform;
		private bool _initializedTransform;
		[SerializeField] private bool _static;
		
		protected Transform Transform
		{
			get
			{
				if (!_initializedTransform)
				{
					_transform = transform;
					_initializedTransform = true;
				}
				return _transform;
			}
		}

		protected string Style => _style;
		protected float PixelSize => _pixelSize;
		protected float Width => _widthPixels * PixelSize;
		protected float Height => _heightPixels * PixelSize;
		protected Rect Rect => new Rect(Transform.localPosition.x, Transform.localPosition.y, Width, Height);
		public CoreUIElement CurrentUIElement => _currentCoreUIElement;

		public event EventHandler<BaseCoreUIView> CreateContainer;
		
		protected virtual void Awake()
		{
			var parentTransform = Transform.parent;
			if (parentTransform == null) return;
			_parentCoreUIView = parentTransform.GetComponent<BaseCoreUIView>();
			if (_parentCoreUIView != null) _parentCoreUIView.CreateContainer += OnCreateContainer;
		}
		
		private void Start()
		{
			if (_parentCoreUIView == null) DrawElement(null);
		}

		protected virtual void Update()
		{
			_widthPixels = Mathf.Max(0, _widthPixels);
			_heightPixels = Mathf.Max(0, _heightPixels);
			_currentCoreUIElement.Resize(_widthPixels * PixelSize, _heightPixels * PixelSize);
			if (_static) return;
			_currentCoreUIElement.Position = Transform.position;
		}

		private void OnCreateContainer(object sender, BaseCoreUIView container)
		{
			if (!_overridePixelSize) _pixelSize = container._pixelSize;
			DrawElement(container);
		}

		private void DrawElement(BaseCoreUIView container)
		{
			if (container == null) _currentCoreUIElement = DrawElementInternal(null);
			else _currentCoreUIElement = DrawElementInternal((CoreUIContainer) container._currentCoreUIElement);
			var handler = CreateContainer;
			if (handler != null) handler(this, this);
		}

		protected abstract CoreUIElement DrawElementInternal(CoreUIContainer parentContainer);
		
		private void OnDrawGizmos()
		{
			Handles.DrawSolidRectangleWithOutline(new Rect(transform.position + Vector3.down * Height, 
					new Vector2(Width, Height)), 
				new Color(.1f, .1f, .1f, .1f), Color.black);
		}
	}
}