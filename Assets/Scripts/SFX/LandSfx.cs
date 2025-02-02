using Physics;

namespace SFX
{
    public class LandSfx : SfxTrigger
    {
        private new void Start()
        {
            base.Start();
            
            GetComponentInParent<GroundDetector>().onGroundedStateChange.AddListener(OnGroundedStateChange);
        }

        private void OnGroundedStateChange(bool grounded)
        {
            if (grounded) Play();
        }
    }
}