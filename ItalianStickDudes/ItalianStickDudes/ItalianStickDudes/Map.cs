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

    [Serializable]
    public class MapTile
    {
        public Vector2 Position = new Vector2(0, 0);
        public string TextureName = "";
        public float Depth = 1.0f;
    }

    [Serializable]
    public class Map
    {
        public string NameOfMap = "";
        public List<MapTile> MapTiles = new List<MapTile>();        
    }

}
