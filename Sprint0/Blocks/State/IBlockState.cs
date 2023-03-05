using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint0.Blocks.State
{
    public interface IBlockState
    {
        void Enter(IBlockState previousState);
        void Exit();

        void DefaultTransition();
        void BumpTransition();
        void ShatterTransition();
        void UsedTransition();

        void Update(GameTime gameTime);
    }
}
