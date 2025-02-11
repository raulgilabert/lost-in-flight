using System;
using UnityEngine;

namespace SFX
{
    public class DamagedSfx : SfxTrigger
    {
        [Serializable]
        public enum Mode
        {
            Always,
            ExceptOnDeath,
            OnlyOnDeath,
        }

        [SerializeField] private Mode mode;
        
        private new void Start()
        {
            base.Start();
            
            GetComponentInParent<Health.Health>().onDamaged.AddListener(OnDamaged);
        }

        private void OnDamaged(float damage, bool isDead)
        {
            if (mode.ShouldPlay(isDead)) Play();
        }
    }
    
    static class ModeMethods
    {
        public static bool ShouldPlay(this DamagedSfx.Mode mode, bool isDead) => 
            mode == DamagedSfx.Mode.Always
            || (mode == DamagedSfx.Mode.ExceptOnDeath && !isDead)
            || (mode == DamagedSfx.Mode.OnlyOnDeath && isDead);
    }
}