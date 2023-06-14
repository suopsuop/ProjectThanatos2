using Microsoft.Xna.Framework.Graphics;
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
        static List<Entity> entities = new List<Entity>();
        static List<Entity> addedEntities = new List<Entity>();

        static bool isUpdating;

        public static int entityCount { get { return entities.Count; } }

        public static void Add(Entity entity)
        {
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

            foreach (var entity in addedEntities)
                addEntity(entity);

            addedEntities.Clear();

            entities = entities.Where(x => !x.isExpired).ToList(); // Only adds entities that still wish to exist
        }

        static void handleCollisions()
        {
            // Nothing, Yet


            // Split bullets collision each update to multiple movement checks (e.g., if it moves 10 units in a frame, check it at 5 as well)
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);
        }
    }
}
