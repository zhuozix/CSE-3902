using Microsoft.Xna.Framework;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.Sounds;
using System;

namespace Sprint0.MarioPlayer.State.ActionState
{
    public class MarioPipeState : MarioActionState
    {
        public MarioPipeState(Mario marioEntity, MarioFactory marioFactory, Vector2 dest, string direction, string destDir) :
            base(marioEntity, marioFactory)
        {
            destination = dest;
            yPos = marioEntity.Position.Y;
            xPos = marioEntity.Position.X;
            dir = direction;
            destDirection = destDir;

        }

        private Vector2 destination;
        private string dir;
        private string destDirection;
        private float yPos;
        private float xPos;

        public override void Enter(IMarioActionState previousState)
        {
            base.Enter(previousState);
            if (dir.Equals("down")) marioEntity.velocity = new Vector2(0, 40);
            if (dir.Equals("right")) marioEntity.velocity = new Vector2(40, 0);
            if (dir.Equals("left")) marioEntity.velocity = new Vector2(-40, 0);



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
            return MarioActionStateType.Piping;
        }

        public void ExitPipe(string destDir)
        {
            if(destDir != null)
            {
                marioEntity.Position = destination;
            }
            if (destDir == null || destDir.Equals("none"))
            {
                CurrentState = new MarioFallState(marioEntity, marioFactory);
                CurrentState.Enter(this);
            } else
            {
                CurrentState = new MarioPipeState(marioEntity, marioFactory, new Vector2(0,0), destDir, null);
                CurrentState.Enter(this);
            }
            
        }
        public override void Update(GameTime gameTime)
        {
            //marioEntity.Position = new Vector2(marioEntity.Position.X, marioEntity.Position.Y - 1);
            if (dir.Equals("down") && marioEntity.Position.Y > yPos+32)
            {
                ExitPipe(destDirection);
            }
            else if (dir.Equals("up"))
            {
                ExitPipe(destDirection);
            }
            else if (dir.Equals("right") && marioEntity.Position.X > xPos + 16)
            {
                ExitPipe(destDirection);
            }
            else if (dir.Equals("left") && marioEntity.Position.X < xPos - 16)
            {
                ExitPipe(destDirection);
            }


        }
    }
}
