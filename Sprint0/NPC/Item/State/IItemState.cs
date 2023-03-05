﻿using Microsoft.Xna.Framework;

namespace Sprint0.NPC.Item.State
{
    public interface IItemState
    {
        void Enter(IItemState previousState);
        void Exit();

        void DispenseTransition();
        void IdleTransition();
        void MoveTransition();
        void BounceTransition();

        void Update(GameTime gameTime);
    }
}