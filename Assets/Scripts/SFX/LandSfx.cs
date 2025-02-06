using Physics;

namespace SFX
{
    public class LandSfx : SfxTrigger
    {
        private new void Awake()
        {
            base.Awake();
            
            GetComponentInParent<GroundDetector>().onGroundedStateChange.AddListener(OnGroundedStateChange);
        }

        private void OnGroundedStateChange(bool grounded)
        {
            if (grounded) Play();
        }
    }
}