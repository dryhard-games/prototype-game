using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private ColliderMarker[] _markers;

    private Animator _anim;
    private bool _isPlaying;
    private List<GameObject> _oldTargets = new List<GameObject>();

    private int _counter;

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
            if (_counter == 2)
            {
                obj.GetComponentInParent<Animator>().SetTrigger("BattleCry");
            }
            else
            {
                obj.GetComponentInParent<Animator>().SetTrigger("TakeHit");
            }

			Debug.Log("call iHittable: " + obj.GetComponentInParent<Rigidbody>().name + "'s " + obj.name);
            _oldTargets.Add(obj);
			_counter++;
        }
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.F))
        {
			_anim.SetTrigger("MeleeAttack");
        }

        if (!_isPlaying)
        {
            return;
        }
	}
}