using DeenGames.HavenIsland.Events;
using DeenGames.HavenIsland.Map.Entities;
using DeenGames.HavenIsland.Map.UI;
using DeenGames.HavenIsland.Model;
using Puffin.Core;
using Puffin.Core.Ecs;
using Puffin.Core.Events;
using Puffin.Core.Tiles;
using System.IO;

namespace DeenGames.HavenIsland.Scenes
{
    public class MapScene : Scene
    {
        private const int MAP_WIDTH = 40;
        private const int MAP_HEIGHT = 23;

        public MapScene()
        {
            // Setup ground, trees, player, etc.
            var groundTileMap = new TileMap(MAP_WIDTH, MAP_HEIGHT, Path.Join("Content", "Images", "Tilesets", "Outside.png"), 32, 32);
            groundTileMap.Define("Grass", 0, 0);
            for (var y = 0; y < MAP_HEIGHT; y++)
            {
                for (var x = 0; x < MAP_WIDTH; x++)
                {
                    groundTileMap.Set(x, y, "Grass");
                }
            }

            this.Add(groundTileMap);

            this.Add(new Tree().Move(300, 200));
            this.Add(new Rock().Move(500, 150));
            this.Add(new Player().Move(500, 250));

            // UI
            // TODO: probably backed by a PNG
            // TODO: show/hide label on mouse over/out
            this.Add(new EnergyBar());

            // Event handlers
            EventBus.LatestInstance.Subscribe(MapEvents.InteractedWithTree, (obj) => 
            {
                var tree = obj as Tree;
                if (GameWorld.Instance.PlayerEnergy > Player.EnergyCost(MapEvents.InteractedWithTree))
                {
                    EventBus.LatestInstance.Broadcast(MapEvents.ChoppedDownTree, tree);
                    this.Remove(tree);
                }
            });

            EventBus.LatestInstance.Subscribe(MapEvents.InteractedWithRock, (obj) => 
            {
                var rock = obj as Rock;
                if (GameWorld.Instance.PlayerEnergy > Player.EnergyCost(MapEvents.InteractedWithRock))
                {
                    // EventBus.LatestInstance.Broadcast(MapEvent.MinedRock, rock);
                    // this.Remove(rock);
                    HavenIslandGame.LatestInstance.ShowScene(new RockMiningScene());
                }
            });

            // Camera
            this.Add(new Entity().Camera(Constants.GAME_ZOOM));
        }
    }
}