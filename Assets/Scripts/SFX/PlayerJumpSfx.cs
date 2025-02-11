using Player;

namespace SFX
{
    public class PlayerJumpSfx : SfxTrigger
    {
        private new void Start()
        {
            base.Start();
            
            GetComponentInParent<PlayerMovement>().onJump.AddListener(Play);
        }
    }
}