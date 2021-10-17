﻿using UnityEngine;

namespace Monster_Rancher.MouseEventSystem
{
    public abstract class MouseEventBase : MonoBehaviour
    {
        public abstract void OnMouseOverReaction();
        public abstract void OnMouseClickReaction();
        public abstract void OnMouseEnterReaction();
        public abstract void OnMouseExitReaction();
    }
}