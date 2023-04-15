using Microsoft.Xna.Framework;
using Sprint0.Sounds;
using Sprint0.UI.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.MarioPlayer.State.ActionState
{
    internal class MarioLevel1WinState : MarioActionState
    {
        public MarioLevel1WinState(Mario marioEntity, MarioFactory marioFactory) :
            base(marioEntity, marioFactory)
        { }
        private float timeSpent = 0f;
        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);

            marioEntity.velocity = new Vector2(50, 0);

        }

        public override void JumpingTransition()
        {

        }

        public override void IdleTransition()
        {


        }

        public override void RunningTransition()
        {
        }

        public override void FallingTransition()
        {

        }
        public override void PoleSlidingTransition()
        {
           
        }
        public override void TurnLeft()
        {




        }
        public override void TurnRight()
        {



        }

        public override void Attack()
        {

        }

        public override MarioActionStateType GetEnumValue()
        {
            return MarioActionStateType.Win;
        }

        public override void Update(GameTime gameTime)
        {
            // transition to boss fight
            timeSpent += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(timeSpent >= 4.3f)
            {
                marioEntity.game.ChangeState(new bossfightState(marioEntity.game));
            }

        }
    }
}
