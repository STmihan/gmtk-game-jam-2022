using Gameplay.Components.Player;
using Gameplay.Components.Share;
using Gameplay.Components.Tile;
using Gameplay.Configs;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Systems.Tile
{
    public class TileSetterSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsWorld _world;
        private TilesConfig _config;
        private EcsFilter<PlayerTag, CharacterViewComponent> _filter;
        private EcsFilter<TileComponent> _tileFilter;

        public void Init()
        {
            for (int x = -50; x < 51; x++)
            {
                for (int y = -50; y < 51; y++)
                {
                    ref var tile = ref _world.NewEntity().Get<TileComponent>();
                    tile.Position = new Vector3(x * 100, 0, y * 100);
                }
            }
        }

        public void Run()
        {
            foreach (var i in _filter)
            foreach (var t in _tileFilter)
            {
                var playerPos = _filter.Get2(i).View.transform.position;
                ref var tile = ref _tileFilter.Get1(t);
                if (Vector3.Distance(playerPos, tile.Position) < 100)
                {
                    if (tile.View != null) continue;
                    var tileView = Object.Instantiate(_config.Tiles[Random.Range(0, _config.Tiles.Length)],
                        tile.Position, Quaternion.identity);
                    tile.View = tileView;
                }
                else
                {
                    if (tile.View == null) continue;
                    Object.Destroy(tile.View.gameObject);
                    tile.View = null;
                }
            }
        }
    }
}