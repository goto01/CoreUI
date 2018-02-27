using System.Collections.Generic;
using UICore.Presentation.Presentations;
using UnityEngine;

namespace UICore
{
	public class CoreUIPresentationParent : MonoBehaviour
	{
		public List<CoreUISimplePresentation> Presentations;
		private Transform _transform;

		public Vector3 Position
		{
			get { return transform.position; }
			set { _transform.position = value; }
		}
		public Transform Transform{get { return _transform; }}

		public void Init()
		{
			_transform = transform;
		}
	}
}