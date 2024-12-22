using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Factoria para la creación de potenciadores
/// </summary>
public static class PowerUpFactory
{
    // Lista con el peso (probabilidades) y ruta al prefab del porwerUp de cada powerUp
    private static readonly List<(int weight, string prefabPath)> powerUpData = new()
    {
        (1, "Prefabs/PowerUp_AddLive"),
        (2, "Prefabs/PowerUp_MultipleBalls"),
        (2, "Prefabs/PowerUp_SpeedPaddle"),
        (2, "Prefabs/PowerUp_ShrinkPaddle"),
        (2, "Prefabs/PowerUp_EnlargePaddle"),
        (2, "Prefabs/PowerUp_500"),
        (3, "Prefabs/PowerUp_250"),
        (4, "Prefabs/PowerUp_100"),
        (6, "Prefabs/PowerUp_50") 
    };

    private static int totalWeight;

    static PowerUpFactory()
    {
        // Calcular el peso total para las probabilidades
        totalWeight = 0;
        foreach (var powerUp in powerUpData)
        {
            totalWeight += powerUp.weight;
        }
    }

    public static GameObject CreatePowerUp()
    {
        try
        {
            // Generar un número aleatorio en el rango del peso total
            int randomValue = UnityEngine.Random.Range(0, totalWeight);

            // Seleccionar el powerUp según el rango
            int currentWeight = 0;
            foreach (var (weight, prefabPath) in powerUpData)
            {
                currentWeight += weight;
                if (randomValue < currentWeight)
                {
                    // Devolvemos el powerUp generado
                    return Resources.Load<GameObject>(prefabPath);
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            return null;
        }
    }
}