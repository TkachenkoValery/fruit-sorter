using UnityEngine;

public class Item
{
    public ItemConfig Config { get; private set; }
    public GameObject InstanceGameObject { get; private set; }

    public Item (ItemConfig config, GameObject instanceGameObject)
    {
        Config = config;
        InstanceGameObject = instanceGameObject;
    }

    private float _StartY;
    public float StartY
    {
        get
        {
            if (!WasStartYGot)
            {
                return float.NegativeInfinity;
            }
            return _StartY;
        }
        private set
        {
            _StartY = value;
            WasStartYGot = true;
        }
    }

    public bool WasStartYGot { get; private set; }

    public void InitializeStartY()
    {
        StartY = InstanceGameObject.transform.position.y;
    }
}