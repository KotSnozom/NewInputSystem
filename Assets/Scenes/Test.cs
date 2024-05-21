using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]private Vector2 _oldPos,_pos;
    [SerializeField] private float _dist,_translate;
    private void Start()
    {
        InputManager.OnStart += Starts;
        InputManager.OnStop += Stops;
        InputManager.OnPosition += Position;
    }
    private void Starts()
    {
        StartCoroutine(nameof(Dess));
    }
    private void Stops()
    {
        StopCoroutine(nameof(Dess));
        _oldPos = default;
    }
    IEnumerator Dess()
    {
        while (true)
        {
            if(Vector2.Distance(_oldPos,_pos) >= _dist)
            {
                Vector2 _dir = (_oldPos - _pos).normalized;
               
                transform.Translate(new Vector3(_dir.x,_dir.y)* _translate);
                _oldPos = _pos;
            }
            Debug.DrawLine(_oldPos,_pos);
            yield return null;
        }
    }
    private void Position(Vector2 pos)
    {
        _pos = pos;
        _pos = Camera.main.ScreenToWorldPoint(_pos);
    }
}
