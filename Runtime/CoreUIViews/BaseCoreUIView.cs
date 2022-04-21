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
			TryResize();
			if (_static) return;
			_currentCoreUIElement.Position = Transform.position;
		}

		protected virtual void TryResize()
		{
			var width = _widthPixels * PixelSize;
			var height = _heightPixels * PixelSize;
			if (Mathf.Abs(_currentCoreUIElement.Width - width) > Mathf.Epsilon || Mathf.Abs(_currentCoreUIElement.Height - height) > Mathf.Epsilon)
				_currentCoreUIElement.Resize(width, height);
		}
		
		private void OnCreateContainer(object sender, BaseCoreUIView container)
		{
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
		
#if UNITY_EDITOR

		public int WidthPixels
		{
			get => _widthPixels;
			set => _widthPixels = value;
		}
		
		public int HeightPixels
		{
			get => _heightPixels;
			set => _heightPixels = value;
		}
		
		public float PixelSizeEditor => _pixelSize;
		
		private void OnDrawGizmos()
		{
			// if (Selection.objects.Length == 1 && Selection.objects[0] == gameObject)
			// {
			// 	Handles.DrawSolidRectangleWithOutline(new Rect(transform.position + Vector3.down * Height,
			// 			new Vector2(Width, Height)),
			// 		new Color(.1f, .1f, .1f, .1f), Color.black);
			// 	Handles.BeginGUI();
			// 	if (GUI.Button(new Rect(transform.position, new Vector2(100, 30)), "test"))
			// 		Selection.objects = new[] {gameObject};
			// 	Handles.EndGUI();
			// 	
			// } 
			// else
			// {
				Handles.DrawSolidRectangleWithOutline(new Rect(transform.position + Vector3.down * Height,
						new Vector2(Width, Height)),
					new Color(.1f, .1f, .1f, .01f), Color.black);
			// }
		}
#endif		
	}
}