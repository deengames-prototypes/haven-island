using DeenGames.HavenIsland.Entities.Map;
using Puffin.Core;
using Puffin.Core.Ecs;
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
            this.Add(new Player().Move(300, 300));

            // Camera
            this.Add(new Entity().Camera(Constants.GAME_ZOOM));
        }
    }
}