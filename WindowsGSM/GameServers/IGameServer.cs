﻿using WindowsGSM.GameServers.Components;
using WindowsGSM.GameServers.Configs;
using WindowsGSM.GameServers.Mods;
using WindowsGSM.Services;
using WindowsGSM.Utilities;

namespace WindowsGSM.GameServers
{
    /// <summary>
    /// Game Server Interface
    /// </summary>
    public interface IGameServer
    {
        /// <summary>
        /// Game Server Name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Game Server Image Source
        /// </summary>
        public string ImageSource { get; }

        /// <summary>
        /// Game Server Configuration
        /// </summary>
        public IConfig Config { get; set; }

        /// <summary>
        /// Game Server Status
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Game Server Process
        /// </summary>
        public ProcessEx Process { get; set; }

        public Task Backup();

        public Task Restore();

        public Task Create();

        public Task Update();

        public Task Start();

        public Task Stop();

        public Task<string> GetLocalVersion();

        public Task<string> GetLatestVersion();

        public void UpdateStatus(Status status)
        {
            Status = status;
            GameServerService.InvokeGameServersHasChanged();
        }
    }
}