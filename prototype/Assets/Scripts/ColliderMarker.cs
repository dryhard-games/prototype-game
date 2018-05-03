using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMarker : MonoBehaviour
{
    private GameObject _oldTarget;
    private MeleeCombat _meleeCombat;

	private void Awake()
	{
        _meleeCombat = GetComponentInParent<MeleeCombat>();
	}

	public void ResetMarker()
    {
        _oldTarget = null;
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject != _oldTarget)
        {
            _meleeCombat.RegisterHit(other.gameObject);
            _oldTarget = other.gameObject;
        }
	}
}