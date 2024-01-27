using UnityEngine;

namespace Code
{
	public class CopyBoneFromOtherRoot : MonoBehaviour
	{
		[SerializeField] Transform _root;
		[SerializeField] Transform _boneToCopy;
		[SerializeField] bool _inverseRotation;
		[SerializeField] bool _inversePosition;
		[SerializeField] Vector3 _rotationNewOffset;
		
		Vector3 _offset;
		Quaternion _rotationOffset;

		void Awake()
		{
			if(_boneToCopy != null) return;
			// Find in all children of root a bone with the same name as this gameobject
			if (_root.gameObject.name == "mixamorig:" + gameObject.name)
			{
				_boneToCopy = _root;
				_offset = transform.position - _boneToCopy.position;
				_rotationOffset = _boneToCopy.rotation * Quaternion.Inverse(transform.rotation);
			}
			else
			{
				_boneToCopy = GetChildWithNameRecursive(_root, "mixamorig:" + gameObject.name);
				_offset = transform.position - _boneToCopy.position;
				_rotationOffset = _boneToCopy.rotation * Quaternion.Inverse(transform.rotation);
			}
		}

		Transform GetChildWithNameRecursive(Transform aParent, string aName)
		{
			var result = aParent.Find(aName);
			if (result != null)
				return result;
			foreach (Transform child in aParent)
			{
				result = GetChildWithNameRecursive(child, aName);
				if (result != null)
					return result;
			}
			return null;
		}
		void Update()
		{
			if (_boneToCopy)
			{
				transform.position = _boneToCopy.position + _offset;
				transform.rotation = _boneToCopy.rotation * Quaternion.Inverse(_rotationOffset);
				// transform.rotation = _boneToCopy.rotation;
			}
		}
	}
}