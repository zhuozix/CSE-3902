using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint0.ObjectManager;
using Sprint0.Sprites;
using System.Collections;
using Sprint0.NPC.Blocks;
using Sprint0.MarioPlayer;
using Sprint0.Content;
using Sprint0.Factory;
using Sprint0.NPC.Enemy;
using Sprint0.NPC.StateChange;
using Sprint0.Command.PlayerCMD;
using Sprint0.NPC.Item;
using Sprint0.MarioPlayer.State.PowerupState;
using Sprint0.MarioPlayer.State.ActionState;
using static System.Formats.Asn1.AsnWriter;
using Sprint0.Collision.Logic;

namespace Sprint0.Collision
{
    public class Collide
    {

        private GameObjectManager gobj;
        //logic
        private ICollisionLogic enemyVSenemy;
        private ICollisionLogic enemyVSblock;
        private ICollisionLogic itemVSblock;
        private ICollisionLogic player;
        private ICollisionLogic fireball;
        private ICollisionLogic activateEnemy;
        //private ICollisionLogic bossVSmario;
        private List<ICollisionLogic> logicList;

        public bool fall { get; set; }
        public Game1 game;

        public Collide(Game1 gameInstance)
        {
            this.gobj = gameInstance.gameObjectManager;
            game = gameInstance;
            logicList = new List<ICollisionLogic>();
            loadLogic();
        }

        public void loadLogic()
        {
            enemyVSenemy = new EnemyAndEnemy(gobj.enemies,gobj.enemies,this);
            enemyVSblock = new EnemyAndBlock(gobj.enemies, gobj.blocks, this);
            itemVSblock = new ItemAndBlock(gobj.items, gobj.blocks, this);
            player = new MarioAndNPC(gobj.players, gobj.blocks, gobj.items,gobj.enemies, gobj.spawners, gobj.superFireballList, this);
            fireball = new FireballAndOthers(gobj.fireBallList, gobj.players, gobj.enemies, gobj.blocks, this);
            activateEnemy = new ActivateEnemy(gobj.players,gobj.enemies, this);
            //bossVSmario = new BossVSMario(gobj.players,gobj.fireBallList, gobj.bossList, this);
            logicList.Add(enemyVSblock);
            logicList.Add(enemyVSenemy);
            logicList.Add(itemVSblock);
            logicList.Add(player);
            logicList.Add(fireball);
            logicList.Add(activateEnemy);
        }

        public void Update(GameTime gameTime)
        {

            foreach(ICollisionLogic a in logicList)
            {
                a.update(gameTime);
            }

        }

    }
}

