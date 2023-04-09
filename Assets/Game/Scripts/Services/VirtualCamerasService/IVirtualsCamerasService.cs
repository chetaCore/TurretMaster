using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Services.VirtualCamerasService
{
    public interface IVirtualsCamerasService : IService
    {
        void CreateCameras();
    }
}