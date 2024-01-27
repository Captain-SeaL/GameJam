using UnityEngine;

namespace Code
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] Transform _target;
		Vector3 _offset;

		void Awake()
		{
			_offset = transform.position - _target.position;
		}

		void LateUpdate()
		{
			if(_target)
				transform.position = Vector3.Lerp(transform.position, _target.position + _offset, Time.deltaTime * 10f);
		}
	}
}