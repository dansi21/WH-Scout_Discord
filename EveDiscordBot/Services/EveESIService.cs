using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ESI.NET;
using ESI.NET.Enumerations;
using Microsoft.Extensions.Options;
using EveDiscordBot.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace EveDiscordBot.Services
{
    public class EveESIService
    {
        private EsiClient client;
        private static readonly HttpClient HTTPclient = new HttpClient();

        public EveESIService() {

            //Grab secrets
            EveCredentials input = JsonConvert.DeserializeObject<EveCredentials>(File.ReadAllText("EveCredentials.json"));

            IOptions<EsiConfig> config = Options.Create(new EsiConfig()
            {
                EsiUrl = "https://esi.evetech.net/",
                DataSource = DataSource.Tranquility,
                ClientId = input.ClientId,
                SecretKey = input.SecretKey,
                CallbackUrl = input.CallbackUrl,
                UserAgent = input.UserAgent,
                AuthVersion = AuthVersion.v2
            });

            client = new EsiClient(config);
        }

        public async Task<string> SearchSystem(string system) {
            EsiResponse<ESI.NET.Models.Universe.IDLookup> searchResults = await client.Universe.IDs(new List<string>() { system });
            if (searchResults.Data.Systems.Count == 0)
                return null;
            EsiResponse<ESI.NET.Models.Universe.SolarSystem> SystemData = await client.Universe.System(searchResults.Data.Systems[0].Id);

            return SystemData.Data.Name + " : " + SystemData.Data.SecurityStatus;
        }

        public async Task<string> zKillQuery(string systemID) {
            HTTPclient.DefaultRequestHeaders.Add("User-Agent", "github.com/dansi21");
            string value = await HTTPclient.GetStringAsync();

        }

    }
}
