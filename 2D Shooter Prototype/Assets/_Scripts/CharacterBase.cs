using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [System.Serializable]
    public struct CharacterProperties
    {
        public int health;
        public int baseDamage;
    }

    public CharacterProperties characterProperties;
}
