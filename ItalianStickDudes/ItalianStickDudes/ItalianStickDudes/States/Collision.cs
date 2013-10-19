using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItalianStickDudes
{
    class Collision
    {
        public Collision()
        {

        }

        public void CheckPlayerCollisions(List<Player> players, List<Sprite> Tiles)
        {
            for (int pl = 0; pl < players.Count; pl++)
            {
                for (int tp = 0; tp < Tiles.Count; tp++)
                {
                    Player player = players[pl];
                    if (Tiles[tp].GetDepth() == 0.4f)
                    {
                        Sprite tile = Tiles[tp];
                        if (player.BoundingBox.Intersects(tile.BoundingBox))
                        {
                            if (player.Position.Y < tile.GetPosition().Y)
                            {
                                player.collisionState.moveDown = false;
                                if(!player.Jumping && !player.Falling)
                                    player.OnGround = true;
                            }
                            else if (player.Position.Y > tile.GetPosition().Y)
                            {
                                player.collisionState.moveUp = false;
                            }

                            if (player.Position.X < tile.GetPosition().X)
                            {
                                if (player.OnGround)
                                {
                                    if (tile.GetPosition().Y < player.Position.Y + 150)
                                        player.collisionState.moveRight = false;
                                }
                                else
                                    player.collisionState.moveRight = false;
                            }
                            else if (player.Position.X > tile.GetPosition().X)
                            {
                                if (player.OnGround)
                                {
                                    if (tile.GetPosition().Y < player.Position.Y + 150)
                                        player.collisionState.moveLeft = false;
                                }
                                    else
                                        player.collisionState.moveLeft = false;
  
                            }

                            

                            
                        }
                    }
                }
            }
        }
    }
}
