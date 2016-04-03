using System;
using UnityMVC;
using TurtleTime.Models;
using TurtleTime.Utils;
using UnityEngine;

namespace TurtleTime.Views
{
    abstract class BillboardSpriteView<M> : View3D<M> where M : Model, IPhysicalModel
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
            offset.y += GetComponent<Sprite>().rect.height / 2 * GetComponent<Sprite>().pixelsPerUnit;
        }

        protected override void UpdateView()
        {
            transform.position = TurtleUtils.CafeSpaceToWorldCoordinates(Model.Position) + offset;
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
