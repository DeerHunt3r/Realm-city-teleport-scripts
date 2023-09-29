using System;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using DOL.Events;
using DOL.Database;
using DOL.Database.Attributes;
using DOL.AI.Brain;
using DOL.GS.SkillHandler;
using DOL.GS;
using DOL.GS.Scripts;
using DOL.GS.PacketHandler;
using DOL.GS.Spells;
using DOL.GS.Effects;

using log4net;

namespace DOL.GS.Scripts
{
    public class HIBPvEPorter : GameNPC
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override bool Interact(GamePlayer player)
        {
            if (!base.Interact(player)) return false;
            TurnTo(player.X, player.Y);
		player.Out.SendMessage("Hello " + player.Name + ", I can transport you to the other capitols in our world.\n" +
                                "Would you like to visit the bustling capitol of Albion, [Camelot]? \n" +
                                "Or visit the frozen capitol of Midgard [Jordheim]?", eChatType.CT_Say, eChatLoc.CL_PopupWindow);
            player.Bind(true);

            return true;
        }



        public override bool WhisperReceive(GameLiving source, string str)
        {
            if (!base.WhisperReceive(source, str)) return false;
            if (!(source is GamePlayer)) return false;
            GamePlayer t = (GamePlayer)source;
            TurnTo(t.X, t.Y);

            switch (str)
            {
				
				case "Camelot":
				
				if (!t.InCombat)
				{	
                    SendReply(t, "I'm now translocating you to Camelot!");
                    foreach (GamePlayer player in this.GetPlayersInRadius(WorldMgr.VISIBILITY_DISTANCE))
                                    player.Out.SendSpellCastAnimation(this, 4953, 6);
                    t.MoveTo(10, 36213, 29881, 7969, 47);
				}
					else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }

                    break;
				
				case "Jordheim":
				
				if (!t.InCombat)
				{
				
					SendReply(t, "I'm now translocating you to Jordheim!");
                    foreach (GamePlayer player in this.GetPlayersInRadius(WorldMgr.VISIBILITY_DISTANCE))
                                    player.Out.SendSpellCastAnimation(this, 4953, 6);
                    t.MoveTo(101, 31928, 27447, 8800, 2218);
					
				}
					else { t.Client.Out.SendMessage("You can't port while in combat.", eChatType.CT_Say, eChatLoc.CL_PopupWindow); }

                    break;
					
				

                default: break;
            }
            return true;

        }
        private void SendReply(GamePlayer target, string msg)
        {
            target.Client.Out.SendMessage(
                msg,
                eChatType.CT_Say, eChatLoc.CL_PopupWindow);
        }
        [ScriptLoadedEvent]
        public static void OnScriptCompiled(DOLEvent e, object sender, EventArgs args)
        {
            log.Info("\tTeleporter initialized: true");
        }
    }

}