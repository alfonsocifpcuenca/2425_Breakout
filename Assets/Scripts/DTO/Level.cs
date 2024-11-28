using System;
using System.Collections.Generic;

[Serializable]
public class Level
{
    public int nivel;
    public int potenciadores;
    public List<string> bloques; // "bloques" debe coincidir con el JSON
}

