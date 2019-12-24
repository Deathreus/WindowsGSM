﻿using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace WindowsGSM.GameServer
{
    class CSGO
    {
        private readonly string _serverId;

        private string _param;
        public string Error;
        public string Notice;

        public const string FullName = "Counter-Strike: Global Offensive Dedicated Server";

        public string port = "27015";
        public string defaultmap = "de_dust2";
        public string maxplayers = "24";
        public string additional = "-tickrate 64 -usercon +game_type 0 +game_mode 0 +mapgroup mg_active";

        public CSGO(string serverid)
        {
            _serverId = serverid;
        }

        public void CreateServerCFG(string hostname, string rcon_password)
        {
            string configPath = Functions.Path.GetServerFiles(_serverId, @"csgo\cfg\server.cfg");

            File.Create(configPath).Dispose();

            using (TextWriter textwriter = new StreamWriter(configPath))
            {
                textwriter.WriteLine($"hostname \"{hostname}\"");
                textwriter.WriteLine($"rcon_password \"{rcon_password}\"");
                textwriter.WriteLine("sv_password \"\"");
                textwriter.WriteLine("sv_lan \"0\"");
                textwriter.WriteLine("net_maxfilesize \"64\"");
                textwriter.WriteLine("sv_downloadurl \"\"");
                textwriter.WriteLine("exec banned_user.cfg");
                textwriter.WriteLine("exec banned_ip.cfg");
                textwriter.WriteLine("writeid");
                textwriter.WriteLine("writeip");
            }
        }

        public void SetParameter(string ip, string port, string map, string maxplayers, string gslt, string additional)
        {
            _param = "-console -game csgo";
            _param += String.Format("{0}", String.IsNullOrEmpty(ip) ? "" : $" -ip {ip}");
            _param += String.Format("{0}", String.IsNullOrEmpty(port) ? "" : $" -port {port}");
            _param += String.Format("{0}", String.IsNullOrEmpty(maxplayers) ? "" : $" -maxplayers_override {maxplayers}");
            _param += String.Format("{0}", String.IsNullOrEmpty(gslt) ? "" : $" +sv_setsteamaccount {gslt}");
            _param += String.Format("{0}", String.IsNullOrEmpty(additional) ? "" : $" {additional}");
            _param += String.Format("{0}", String.IsNullOrEmpty(map) ? "" : $" +map {map}");
        }

        public async Task<Process> Start()
        {
            string configPath = Functions.Path.GetServerFiles(_serverId, @"csgo\cfg\server.cfg");
            if (!File.Exists(configPath))
            {
                Notice = $"server.cfg not found ({configPath})";
            }

            Steam.SRCDS srcds = new Steam.SRCDS(_serverId);
            Process p = await srcds.Start(_param);
            Error = srcds.Error;

            return p;
        }

        public static async Task<bool> Stop(Process p)
        {
            return await Steam.SRCDS.Stop(p);
        }

        public async Task<Process> Install()
        {
            Steam.SRCDS srcds = new Steam.SRCDS(_serverId);
            Process p = await srcds.Install("740");
            Error = srcds.Error;

            return p;
        }

        public async Task<bool> Update()
        {
            Steam.SRCDS srcds = new Steam.SRCDS(_serverId);
            bool success = await srcds.Update("740");
            Error = srcds.Error;

            return success;
        }
    }
}