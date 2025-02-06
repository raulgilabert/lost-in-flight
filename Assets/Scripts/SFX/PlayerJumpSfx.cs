using Player;

namespace SFX
{
    public class PlayerJumpSfx : SfxTrigger
    {
        private new void Awake()
        {
            base.Awake();
            
            GetComponentInParent<PlayerMovement>().onJump.AddListener(Play);
        }
    }
}