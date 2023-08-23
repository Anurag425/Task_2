using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;

        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, pos);
    }

    private bool CanAppend(Vector2 pos)
    {
        if(_renderer.positionCount == 0) return true;

        return Vector2.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > Manager.RESOLUTION;
    }
}
