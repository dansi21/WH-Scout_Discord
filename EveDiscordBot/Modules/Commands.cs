using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using EveDiscordBot.Services;

namespace EveDiscordBot.Modules
{
    public class CommandsModule : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync(){
            await 
            return ReplyAsync("pong!"); 
        }
    }
}
