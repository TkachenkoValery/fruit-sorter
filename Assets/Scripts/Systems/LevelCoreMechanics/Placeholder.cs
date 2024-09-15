using UnityEngine;

public class Placeholder : ColliderPressingHandler
{
    public PlaceholderConfig Config { get; private set; }

    public Placeholder(Collider collider, Camera camera, PlaceholderConfig config) : base(collider, camera)
    {
        Config = config;
    }
}