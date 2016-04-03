using System;
using UnityMVC;
using UnityEngine;

namespace TurtleTime
{
    abstract class BillboardSpriteView<M> : View3D<M> where M : PhysicalModel
    {
        // TODO: Don't do this
        Camera mainCamera;
        Vector3 offset;

        protected abstract string SpriteName { get; }

        protected override void Load()
        {
            mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
            offset = TurtleUtils.CafeSpaceToWorldCoordinates(new Vector2(1, 0));
            UnityUtils.CreateSprite(gameObject, SpriteName);
        }

        protected override void UpdateView()
        {
            transform.position = TurtleUtils.CafeSpaceToWorldCoordinates(Model.Position) + offset;
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
