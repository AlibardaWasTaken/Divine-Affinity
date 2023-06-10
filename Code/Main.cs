using System;
using NCMS;
using UnityEngine;



namespace DivineAffinity
{
    [ModEntry]
    class Main : MonoBehaviour
    {
        void Awake()
        {
            DivineAffinityGroup.init();
            Traits.init();
        }
        
    }
}