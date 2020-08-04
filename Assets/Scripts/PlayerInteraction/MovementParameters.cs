using UnityEngine;

namespace Anivision.PlayerInteraction
{
    /// <summary>
    /// Class that contains all of the movement information for an animal
    /// </summary>
    public class MovementParameters
    {
        public bool CanFly { get; }
        public bool CanTeleport { get; }
        public LayerMask ValidRaycastLayers { get;}
        public LayerMask ValidTeleportLayers { get; }
        public float TeleportRange { get; }

        public MovementParameters(bool canFly, bool canTeleport, LayerMask validRaycastLayers,
            LayerMask validTeleportLayers, float teleportRange)
        {
            CanFly = canFly;
            CanTeleport = canTeleport;
            ValidRaycastLayers = validRaycastLayers;
            ValidTeleportLayers = validTeleportLayers;
            TeleportRange = teleportRange;
        }
    }
}