using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectThanatos2.Content.Source
{
    static class EntityManager
    {
        static List<Entity> entities= new List<Entity>();
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

            entities = entities.Where(x => !x.isExpired).ToList();
        }

        static void handleCollisions()
        {
            // Nothing, Yet
        }

    }
}
