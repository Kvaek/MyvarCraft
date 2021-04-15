﻿using Flux.Core.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flux.Core.Objects;

namespace Flux.Core.Services {
	public class ChunckProviderService : IService {
		public string Name { get; set; } = "ChunckProviderService";

		public void Start() { }

		public void Stop() { }

		public void Tick() {
			lock (MinecraftServer.Worlds) {
				foreach (World i in MinecraftServer.Worlds)
				foreach (Player p in i.Players)
					if (p.Spawned && !p.SpawnedCunckLoaded) {
						ChunkData cd = new ChunkData() { Owner = p.OwnerID };
						cd.Data = i.WorldGenerator.GetChunk(0, 0);

						NetworkService.EnqueuePacket(cd);

						p.SpawnedCunckLoaded = true;
					}
			}
		}
	}
}