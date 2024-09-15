using UnityEngine;
using System;
using Zenject;

public class ColliderPressingHandler : ITickable
{
    public Collider Collider { get; private set; }
    public Camera Camera { get; private set; }

    public event Action OnClick;
    
    public ColliderPressingHandler(Collider collider, Camera camera)
    {
        Collider = collider;
        Camera = camera;
    }

    public void Tick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray RayForCheckingIfTheButtonWasPressed = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] AllRaycastHits = Physics.RaycastAll(RayForCheckingIfTheButtonWasPressed);
            for (int i = 0; i < AllRaycastHits.Length; i++)
            {
                if (AllRaycastHits[i].collider.Equals(Collider))
                {
                    OnClick?.Invoke();
                }
            }
        }
    }
}