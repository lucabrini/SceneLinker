﻿using UnityEngine;

namespace SceneLinker.Runtime
{
    public class Linkable : MonoBehaviour
    {
        [SerializeField] private bool justTeleported = false;

        public void SetJustTeleported(bool teleported)
        {
            justTeleported = teleported;
        }

        public bool HasJustBeenTeleported()
        {
            return justTeleported;
        }
    }
}