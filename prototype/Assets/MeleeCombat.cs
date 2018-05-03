using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
	[SerializeField] private ColliderMarker[] _markers;

    private Animator _anim;
    private bool _isPlaying;
    private List<GameObject> _oldTargets = new List<GameObject>();

	private void Awake()
	{
        _anim = GetComponent<Animator>();
	}

    public void AnimationState(int animState)
    {
        if (animState != 0)
        {
            _oldTargets.Clear();
            ResetMarkers();
            _isPlaying = true;
        }
        else
        {
            _isPlaying = false;
        }
    }

    private void ResetMarkers()
    {
        for (int i = 0; i < _markers.Length; i++)
        {
            _markers[i].ResetMarker();
        }
    }

    public void RegisterHit(GameObject obj)
    {
        if (!_oldTargets.Contains(obj))
        {
            Debug.Log("HITTING " + obj);
            _oldTargets.Add(obj);
        }
    }

	private void Update()
	{
		_anim.SetTrigger("MeleeAttack");
        if (Input.GetKeyDown(KeyCode.F))
        {
        }

        if (!_isPlaying)
        {
            return;
        }
	}
}