using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Discord;
using Discord.Commands;
using EveDiscordBot.Services;

namespace EveDiscordBot.Modules
{
    public class CommandsModule : ModuleBase<SocketCommandContext>
    {
        public EveESIService EveClient { get; set; }

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync(){
            return ReplyAsync("pong!"); 
        }

        [Command("help")]
        [Alias("commands", "Help")]
        public Task HelpAsync()
        {
            return ReplyAsync("Commands! \n-ping *Check if the bot is running\n-Scout *Quickly get info on a system");
        }

        // Get info on a user, or the user who invoked the command if one is not specified
        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;

            await ReplyAsync(user.ToString());
        }

        [Command("scout")]
        public async Task SearchSystem([Remainder] string reason = null)
        {
            string SystemResponse = EveClient.SearchSystem(reason).Result;
            await ReplyAsync(SystemResponse);
        }
    }
}
