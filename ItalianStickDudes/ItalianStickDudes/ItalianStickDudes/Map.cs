using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ItalianStickDudes
{
    public enum EntityType
    {
        PLAYER_START,
        POWER_UP,
        TRAP
    }

    class MapTile
    {
        public Vector2 Position;
        public string TextureName;
        public float Depth;
    }

    class Map
    {
        public string NameOfMap;
        public List<MapTile> MapTiles;
        
    }

}
