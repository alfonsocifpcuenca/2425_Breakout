using System.Collections.Generic;

public static class BlockConfigFactory
{
    // Diccionario con las configuraciones de los bloques
    private static readonly Dictionary<BlockType, BlockConfig> Configurations = new Dictionary<BlockType, BlockConfig>
    {
        { BlockType.Yellow, new BlockConfig { hitsToDestroy = 1, pointsPerHit = 50 } },
        { BlockType.Blue, new BlockConfig { hitsToDestroy = 2, pointsPerHit = 100 } },
        { BlockType.Cian, new BlockConfig { hitsToDestroy = 2, pointsPerHit = 125 } },
        { BlockType.Green, new BlockConfig { hitsToDestroy = 3, pointsPerHit = 150 } },
        { BlockType.Red, new BlockConfig { hitsToDestroy = 3, pointsPerHit = 200 } },
        { BlockType.Orange, new BlockConfig { hitsToDestroy = 4, pointsPerHit = 250 } }
    };

    public static BlockConfig GetConfig(BlockType type)
    {
        if (Configurations.TryGetValue(type, out var config))
        {
            return config;
        }

        return new BlockConfig { hitsToDestroy = 1, pointsPerHit = 50 };
    }
}