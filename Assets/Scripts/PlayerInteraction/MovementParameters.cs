using UnityEngine;

namespace Anivision.PlayerInteraction
{
    /// <summary>
    /// Class that contains all of the movement information for an animal
    /// </summary>
    public class MovementParameters
    {
        public bool CanFly { get; private set; }
        public bool CanTeleport { get; private set; }
        public LayerMask ValidRaycastLayers { get; private set; }
        public LayerMask ValidTeleportLayers { get; private set; }
        public float TeleportRange { get; private set; }

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