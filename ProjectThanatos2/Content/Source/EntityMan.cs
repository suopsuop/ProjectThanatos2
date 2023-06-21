using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectThanatos2.Content.Source;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos.Content.Source
{
    static class EntityMan
    {
        private static List<Entity> entities = new List<Entity>();
        private static List<Entity> addedEntities = new List<Entity>();

        static bool isUpdating;

        public static int entityCount { get { return entities.Count; } }

        public static void Add(Entity entity)
        {
            // Only adds entities if not already updating entity list
            if(!isUpdating)
            {
                addEntity(entity);
            }
            else
            {
                addedEntities.Add(entity);
                
            }
        }

        private static void addEntity(Entity entity)
        {
            entities.Add(entity);            
        }

        public static void Update()
        {
            isUpdating = true;
            handleCollisions();

            foreach (var entity in entities)
                entity.Update();

            isUpdating = false;

            // Adds entities waiting to be added
            foreach (var entity in addedEntities)
                addEntity(entity);

            // Clears list of entities to be added, to avoid double-ups
            addedEntities.Clear();

            // Only adds entities that still wish to exist
            entities = entities.Where(x => !x.isExpired).ToList();

            
        }

        static void handleCollisions()
        {
            foreach (var entity in entities)
            {
                // Resets colour
                entity.color = Color.White;

                // Only bother with enemy bullets & enemies
                if (entity.GetType() == typeof(EnemyBullet) || entity.GetType() == typeof(Enemy)) 
                {
                    if (entity.collisionBox.Intersects(Player.Instance.collisionBox))
                    {
                        Player.Instance.Kill();
                    }
                }
                // Only go through Playerbullets. This isn't economical,but it works
                if (entity.GetType() == typeof(Enemy))
                {
                    foreach (var entity2 in entities)
                    {
                        if (entity.GetType() == typeof(Enemy) && entity2.GetType() == typeof(PlayerBullet))
                        {
                            if (entity.collisionBox.Intersects(entity2.collisionBox))
                            {
                                entity.Hurt(entity2.damage);
                                entity.color = Color.Red;
                                entity2.Kill();
                            }
                        }
                    }
                }
            }

            // Split bullets collision each update to multiple movement checks (e.g., if it moves 10 units in a frame, check it at 5 as well)
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);

            // Draws Debug Rectangles
            if (GameMan.shouldDrawDebugRectangles)
            {
                foreach (var entity in entities)
                {
                    spriteBatch.Begin();
                    spriteBatch.DrawRectangle(entity.collisionBox, Color.Red);
                    spriteBatch.End();
                }
            }
        }

        public static bool DoesEntityExist(Entity entity)
        {
            return entities.Contains(entity);
        }

        public static void KillAllEnemyBullets()
        {
            foreach (var entity in entities)
            {
                if (entity.GetType() == typeof(EnemyBullet))
                {
                    entity.direction = RiceLib.DirectionAwayFromVector(entity.position, Player.Instance.position);
                    entity.speed = 1f;
                    entity.acceleration = 1.75f;
                    entity.curve = 0f;
                }
            }
        }
    }
}
