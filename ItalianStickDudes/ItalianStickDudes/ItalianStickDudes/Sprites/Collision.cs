using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ItalianStickDudes
{
    class Collision
    {
        public void CheckCollisions(List<Player> players, List<Sprite> tiles)
        {
            for (int x = 0; x < players.Count; x++)
            {
                for (int y = 0; y < tiles.Count; y++)
                {
                    Player player = players[x];
                    Sprite tile = tiles[y];
                    if (tile.GetDepth() == 0.4f)
                    {
                        if (player.BoundingBox.Intersects(tile.BoundingBox))
                        {

                            float left = tile.BoundingBox.Left - player.BoundingBox.Right;
                            float right = tile.BoundingBox.Right - player.BoundingBox.Left;
                            float minXPush = Math.Abs(left) < Math.Abs(right) ? left : right;

                            float top = tile.BoundingBox.Top - player.BoundingBox.Bottom;
                            float bottom = tile.BoundingBox.Bottom - player.BoundingBox.Top;
                            float minYPush = Math.Abs(top) < Math.Abs(bottom) ? top : bottom;

                            if (Math.Abs(minXPush) < Math.Abs(minYPush)) // x pushing distance is minor than y
                            {
                                player.Position.X += minXPush;

                                if (minXPush > 0)
                                    player.moveState.canMoveRight = false;
                                else
                                    player.moveState.canMoveLeft = false;
                            }
                            else
                            {
                                player.Position.Y += minYPush;

                                if (minXPush > 0)
                                {
                                    player.moveState.canMoveDown = false;
                                   // player.OnGround = true;
                                }
                                else
                                    player.moveState.canMoveUp = false;

                                
                            }

                            
                            
                        }
                    }

                }
            }
        }
        
    }
}
