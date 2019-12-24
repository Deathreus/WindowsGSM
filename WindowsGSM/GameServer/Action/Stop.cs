﻿using System.Threading.Tasks;
using System.Diagnostics;

namespace WindowsGSM.GameServer.Action
{
    class Stop
    {
        private readonly Function.ServerTable server;
        public string Error = "";
        public string Notice = "";

        public Stop(Function.ServerTable server)
        {
            this.server = server;
        }

        public async Task<bool> Run(Process process)
        {
            switch (server.Game)
            {
                case (GameServer.CSGO.FullName): await GameServer.CSGO.Stop(process); break;
                case (GameServer.GMOD.FullName): await GameServer.GMOD.Stop(process); break;
                case (GameServer.TF2.FullName): await GameServer.TF2.Stop(process); break;
                case (GameServer.MCPE.FullName): await GameServer.MCPE.Stop(process); break;
                case (GameServer.RUST.FullName): await GameServer.RUST.Stop(process); break;
                case (GameServer.CS.FullName): await GameServer.CS.Stop(process); break;
                case (GameServer.CSCZ.FullName): await GameServer.CSCZ.Stop(process); break;
                case (GameServer.HL2DM.FullName): await GameServer.HL2DM.Stop(process); break;
                case (GameServer.L4D2.FullName): await GameServer.L4D2.Stop(process); break;
                case (GameServer.MC.FullName): await GameServer.MC.Stop(process); break;
                case (GameServer.GTA5.FullName): await GameServer.GTA5.Stop(process); break;
                default: return true;
            }

            if (!process.HasExited)
            {
                process.Kill();
            }

            return true;
        }
    }
}