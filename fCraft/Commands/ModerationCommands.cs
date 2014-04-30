﻿// Part of fCraft | Copyright 2009-2013 Matvei Stefarov <me@matvei.org> | BSD-3 | See LICENSE.txt //Copyright (c) 2011-2013 Jon Baker, Glenn Marien and Lao Tszy <Jonty800@gmail.com> //Copyright (c) <2012-2014> <LeChosenOne, DingusBungus>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using JetBrains.Annotations;

namespace fCraft {
    /// <summary> Most commands for server moderation - kick, ban, rank change, etc - are here. </summary>
    static class ModerationCommands {
        const string BanCommonHelp = "Ban information can be viewed with &H/BanInfo";

        internal static void Init() {
            CdBan.Help += BanCommonHelp;
            CdBanIP.Help += BanCommonHelp;
            CdBanAll.Help += BanCommonHelp;
            CdUnban.Help += BanCommonHelp;
            CdUnbanIP.Help += BanCommonHelp;
            CdUnbanAll.Help += BanCommonHelp;

            CommandManager.RegisterCommand( CdBan );
            CommandManager.RegisterCommand( CdBanIP );
            CommandManager.RegisterCommand( CdBanAll );
            CommandManager.RegisterCommand( CdUnban );
            CommandManager.RegisterCommand( CdUnbanIP );
            CommandManager.RegisterCommand( CdUnbanAll );
            CommandManager.RegisterCommand( CdBot );
            CommandManager.RegisterCommand( CdCalculator );
            CommandManager.RegisterCommand( CdKick );
            CommandManager.RegisterCommand( CdRank );
            CommandManager.RegisterCommand( CdHide );
            CommandManager.RegisterCommand( CdUnhide );
            CommandManager.RegisterCommand( CdSetSpawn );
            CommandManager.RegisterCommand( CdFreeze );
            CommandManager.RegisterCommand( CdUnfreeze );
            CommandManager.RegisterCommand( CdTeleport );
            CommandManager.RegisterCommand( CdTeleportP );
            CommandManager.RegisterCommand( CdBring );
            CommandManager.RegisterCommand( CdWorldBring );
            CommandManager.RegisterCommand( CdBringAll );
            CommandManager.RegisterCommand( CdPatrol );
            CommandManager.RegisterCommand( CdSpecPatrol );
            CommandManager.RegisterCommand( CdMute );
            CommandManager.RegisterCommand( CdUnmute );
            CommandManager.RegisterCommand( CdSpectate );
            CommandManager.RegisterCommand( CdUnspectate );
            CommandManager.RegisterCommand( CdChangeModel );
            CommandManager.RegisterCommand( CdSudo );
            CommandManager.RegisterCommand( CdTPDeny );
            CommandManager.RegisterCommand( CdJORW );
            CommandManager.RegisterCommand( CdMaxCaps );
            CommandManager.RegisterCommand( CdHackControl );

        }
        #region Calculator

        static readonly CommandDescriptor CdCalculator = new CommandDescriptor
        {
            Name = "Calculator",
            Aliases = new[] { "Calc" },
            Category = CommandCategory.New,
            Permissions = new Permission[] { Permission.Chat },
            IsConsoleSafe = true,
            Usage = "/Calculator [number] [+, -, *, /, sqrt, sqr] [(for +,-,*, or /)number]",
            Help = "Lets you use a simple calculator in minecraft. Valid options are [ + , - , * ,  / , sqrt, and sqr].",
            NotRepeatable = false,
            Handler = CalcHandler,
        };

        static void CalcHandler(Player player, CommandReader cmd)
        {
            String numberone = cmd.Next();
            String op = cmd.Next();
            String numbertwo = cmd.Next();
            double no1 = 1;
            double no2 = 1;

            if (numberone == null || op == null)
            {
                CdCalculator.PrintUsage(player);
                return;
            }

            if (!double.TryParse(numberone, out no1))
            {
                player.Message("Please choose from a whole number.");
                return;
            }
            if (numbertwo != null)
            {
                if (!double.TryParse(numbertwo, out no2))
                {
                    player.Message("Please choose from a whole number.");
                    return;
                }
            }


            if (player.Can(Permission.Chat))
            {

                if (numberone != null || op != null)
                {
                    if (op == "+" | op == "-" | op == "*" | op == "/" | op == "sqrt" | op == "sqr")
                    {

                        if (op == "+")
                        {
                            if (numbertwo == null)
                            {
                                player.Message("You must select a second number!");
                                return;
                            }
                            double add = no1 + no2;
                            if (add < 0 | no1 < 0 | no2 < 0)
                            {
                                player.Message("Negative Number Detected, please choose from a whole number.");
                                return;
                            }
                            else
                            {
                                player.Message("&0Calculator&f: {0} + {1} = {2}", no1, no2, add);
                            }
                        }
                        if (op == "-")
                        {
                            if (numbertwo == null)
                            {
                                player.Message("You must select a second number!");
                                return;
                            }
                            double subtr = no1 - no2;
                            if (subtr < 0 | no1 < 0 | no2 < 0)
                            {
                                player.Message("Negative Number Detected, please choose from a whole number.");
                                return;
                            }
                            else
                            {
                                player.Message("&0Calculator&f: {0} - {1} = {2}", no1, no2, subtr);
                            }
                        }
                        if (op == "*")
                        {
                            if (numbertwo == null)
                            {
                                player.Message("You must select a second number!");
                                return;
                            }
                            double mult = no1 * no2;
                            if (mult < 0 | no1 < 0 | no2 < 0)
                            {
                                player.Message("Negative Number Detected, please choose from a whole number.");
                                return;
                            }
                            else
                            {
                                player.Message("&0Calculator&f: {0} * {1} = {2}", no1, no2, mult);
                            }
                        }
                        if (op == "/")
                        {
                            if (numbertwo == null)
                            {
                                player.Message("You must select a second number!");
                                return;
                            }
                            double div = no1 / no2;
                            if (div < 0 | no1 < 0 | no2 < 0)
                            {
                                player.Message("Negative Number Detected, please choose from a whole number.");
                                return;
                            }
                            else
                            {
                                player.Message("&0Calculator&f: {0} / {1} = {2}", no1, no2, div);
                                return;
                            }
                        }
                        if (op == "sqrt")
                        {
                            double sqrt = Math.Round(Math.Sqrt(no1), 2);
                            if (no1 < 0)
                            {
                                player.Message("Negative Number Detected, please choose from a whole number.");
                                return;
                            }
                            else
                            {
                                player.Message("&0Calculator&f: Square Root of {0} = {1}", no1, sqrt);
                                return;
                            }
                        }
                        if (op == "sqr")
                        {
                            double sqr = no1 * no1;
                            if (no1 < 0)
                            {
                                player.Message("Negative Number Detected, please choose from a whole number.");
                                return;
                            }
                            else
                            {
                                player.Message("&0Calculator&f: Square of {0} = {1}", no1, sqr);
                                return;
                            }
                        }
                    }
                    else
                    {
                        player.Message("&cInvalid Operator. Please choose from '+' , '-' , '*' , '/' , 'sqrt' , or 'sqr'");
                        return;
                    }
                }
                else
                {
                    CdCalculator.PrintUsage(player);
                }
            }

        }
        #endregion
        #region ChangeModel
        static readonly CommandDescriptor CdChangeModel = new CommandDescriptor
        {
            Name = "Model",
            Category = CommandCategory.New,
            Permissions = new[] { Permission.EditPlayerDB },
            Usage = "/Model [Player] [Model]",
            IsConsoleSafe = true,
            Help = "Change the Model of [Player]!\n" +
            "Valid models: &e [Any Block Name or ID#], Chicken, Creeper, Croc, Humanoid, Pig, Printer, Sheep, Skeleton, Spider, Zombie!",
            Handler = ModelHandler
        };

        static void ModelHandler(Player player, CommandReader cmd)
        {
            if (!cmd.HasNext)
            {
                CdChangeModel.PrintUsage(player);
                return;
            }
            string ply = cmd.Next();            
            if (!cmd.HasNext)
            {
                CdChangeModel.PrintUsage(player);
                return;
            }
            string nick = cmd.NextAll();            
            PlayerInfo p = PlayerDB.FindPlayerInfoOrPrintMatches(player, ply, SearchOptions.IncludeSelf);
            if (p == null)
            {
                return;
            }
            int fail;
            Block block;
            if (!int.TryParse(nick, out fail)
                && !nick.ToLower().Equals("chicken")
                && !nick.ToLower().Equals("creeper")
                && !nick.ToLower().Equals("croc")
                && !nick.ToLower().Equals("crocodile")
                && !nick.ToLower().Equals("humanoid")
                && !nick.ToLower().Equals("pig")
                && !nick.ToLower().Equals("printer")
                && !nick.ToLower().Equals("sheep")
                && !nick.ToLower().Equals("skeleton")
                && !nick.ToLower().Equals("spider")
                && !nick.ToLower().Equals("nope")
                && !nick.ToLower().Equals("zombie"))
            {
                if (Map.GetBlockByName(nick.ToLower(), false, out block))
                {
                    nick = block.GetHashCode().ToString();
                }
                else
                {
                    player.Confirm(cmd, "Model not valid, see &h/Help Model&s. Or use default \"Humanoid\" instead?");
                }
            }
            if (nick.ToLower().Equals("crocodile"))
            {
                nick = "croc";
            }
            if (nick.ToLower().Equals("nope"))
            {
                nick = "spider";
            }
            if (p.Mob.ToLower() == nick.ToLower())
            {
                player.Message("{0}'s model is already set to {1}", p.Name, nick);
                return;
            }
            if (p.IsOnline)
            {
                p.PlayerObject.Message("&e{0} &echanged your model from {1} &eto {2}", player.Name, p.Mob, nick);
            }
            player.Message("&eChanged model of {0} &efrom {1} &eto {2}", p.Name, p.Mob, nick);
            p.Mob = nick;
        }
        #endregion
        #region Sudo
        static readonly CommandDescriptor CdSudo = new CommandDescriptor
        {
            Name = "Sudo",
            Category = CommandCategory.New,
            Permissions = new[] { Permission.EditPlayerDB },
            Usage = "/Sudo [Player] [What to type in]",
            IsConsoleSafe = true,
            Help = "Forces the player to execute chat/commands",
            Handler = SudoHandler
        };

        static void SudoHandler(Player player, CommandReader cmd)
        {
            string ply = cmd.Next();
            if (!cmd.HasNext)
            {
                CdSudo.PrintUsage(player);
                return;
            }
            string sudocmd = cmd.NextAll();
            PlayerInfo p = PlayerDB.FindPlayerInfoOrPrintMatches(player, ply, SearchOptions.IncludeSelf);
            if (p == null)
            {
                return;
            }
            if (p.PlayerObject == null)
            {
                player.Message("This player is offline!");
                return;
            }
            if (sudocmd.StartsWith("/j ", StringComparison.OrdinalIgnoreCase) || sudocmd.StartsWith("/join ", StringComparison.OrdinalIgnoreCase) || sudocmd.StartsWith("/map ", StringComparison.OrdinalIgnoreCase))
            {
                player.Message("You cannot use world joining commands with /sudo");
                return;
            }
            p.PlayerObject.ParseMessage(sudocmd, false);
            player.Message("Forced {0} to type in \"{1}\"", p.Name, sudocmd);
            
        }
        #endregion
        #region Bot
        static readonly CommandDescriptor CdBot = new CommandDescriptor
        {
            Name = "Bot",
            Category = CommandCategory.New,
            Permissions = new Permission[] { Permission.Chat },
            Usage = "Bot [Option]",
            Help = "Bot options are &hGo&s, &hServer&s, &hJoke&s, &hTime&s, &hClock&s, &hPromos&s, &hBans&s, &hKicks&s, &hBlocks&s, &hProtip&s, and &hFunfact&s.\n" +
                   "Type in &h/help bot [option] &sfor more information.\n" +
                   "&6Bot&s is our Automated response system, so please don't abuse it.",
            NotRepeatable = true,
            HelpSections = new Dictionary<string, string>{
                { "go",             "&sType: &f!Bot Go\n&S" +
                                    "Sets a 5 second timer to be used for games." +
                                    "Most useful for when there are no staff on." },
                { "server",         "&sType: &f!Bot Server\n&S" +
                                    "Displays the server name."},
                { "joke",           "&sType: &f!Bot Joke\n&S" +
                                    "Displays a joke."},
                { "time",           "&sType: &f!Bot Time\n&S" +
                                    "Displays the time you spent this game session." +
                                    "&sType: &f!Bot Time Total\n&S" +
                                    "Displays your Total Time spent on the server." },
                { "clock",          "&sType: &f!Bot Clock\n&S" +
                                    "Displays the date and time."},
                { "promos",         "&sType: &f!Bot Promos\n&S" +
                                    "Displays the amount of players you have promoted."},
                { "bans",           "&sType: &f!Bot Bans\n&S" +
                                    "Displays the amount of players you have banned."},
                { "kicks",          "&sType: &f!Bot Kicks\n&S" +
                                    "Displays the amount of players you have kicked."},
                { "blocks",         "&sType: &f!Bot Blocks\n&S" +
                                    "Displays the amount of blocks you have modified this session." +
                                    "&sType: &f!Bot Blocks Total\n&S" +
                                    "Displays the amount of blocks you have modified in Total." },
                { "protip",         "&sType: &f!Bot Protip\n&S" +
                                    "Displays a life changing tip.*" +
                                    "*May or may not change your life" },
                { "funfact",        "&sType: &f!Bot Funfact\n&S" +
                                    "Displays a funfact."}
                
                
            },
            Handler = LeBotHandler,
        };

        static void LeBotHandler(Player player, CommandReader cmd)
        {
            String cmdchat = cmd.Next();
            String option = cmd.Next();
            String helper = cmd.Next();
            double BotTime = (DateTime.Now - player.Info.LastTimeUsedBot).TotalSeconds;
            if (cmdchat != "IgnoreMeThisIsJustAFailSafe!@#")
            {                
                return;
            }
            if (BotTime < 5)
            {
                double LeftOverTime = Math.Round(5 - BotTime);
                if (LeftOverTime == 1)
                {
                    player.Message("&WYou can use /Bot again in 1 second.");
                    return;
                }
                else
                {
                    player.Message("&WYou can use /Bot again in " + LeftOverTime + " seconds");
                    return;
                }
            }
            if (option == null)
            {
                player.Message(CdBot.Help);
                return;
            }
            else if (option == "go")
            {
                if (player.Info.TimesUsedBot == 0)
                {
                    player.Message("&6Bot&f: This is your first time using &6Bot&f, I suggest you use \"&h/Help Bot&f\" to further understand how I work.");

                }
                Scheduler.NewTask(t => Server.Players.Message("&6Bot&f: 5")).RunManual(TimeSpan.FromSeconds(0));
                Scheduler.NewTask(t => IRC.SendChannelMessage("\u212C&6Bot\u211C: 5")).RunManual(TimeSpan.FromSeconds(0));
                Scheduler.NewTask(t => Server.Players.Message("&6Bot&f: 4")).RunOnce(TimeSpan.FromSeconds(1));
                Scheduler.NewTask(t => IRC.SendChannelMessage("\u212C&6Bot\u211C: 4")).RunManual(TimeSpan.FromSeconds(1));
                Scheduler.NewTask(t => Server.Players.Message("&6Bot&f: 3")).RunOnce(TimeSpan.FromSeconds(2));
                Scheduler.NewTask(t => IRC.SendChannelMessage("\u212C&6Bot\u211C: 3")).RunManual(TimeSpan.FromSeconds(2));
                Scheduler.NewTask(t => Server.Players.Message("&6Bot&f: 2")).RunOnce(TimeSpan.FromSeconds(3));
                Scheduler.NewTask(t => IRC.SendChannelMessage("\u212C&6Bot\u211C: 2")).RunManual(TimeSpan.FromSeconds(3));
                Scheduler.NewTask(t => Server.Players.Message("&6Bot&f: 1")).RunOnce(TimeSpan.FromSeconds(4));
                Scheduler.NewTask(t => IRC.SendChannelMessage("\u212C&6Bot\u211C: 1")).RunManual(TimeSpan.FromSeconds(4));
                Scheduler.NewTask(t => Server.Players.Message("&6Bot&f: Go!")).RunOnce(TimeSpan.FromSeconds(5));
                Scheduler.NewTask(t => IRC.SendChannelMessage("\u212C&6Bot\u211C: Go!")).RunManual(TimeSpan.FromSeconds(5));
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "server")
            {
                if (player.Info.TimesUsedBot == 0)
                {
                    player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                }
                Server.Players.Message("&6Bot&f: The name of this server is " + ConfigKey.ServerName.GetString() + ".");
                IRC.SendChannelMessage("\u212C&6Bot\u211C: The name of this server is " + ConfigKey.ServerName.GetString() + ".");
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "joke")
            {
                string[] jokeStrings = { "&fWhat do you call a fat psychic? A four chin teller.",
                                      "&fIf con is the opposite of pro, it must mean Congress is the opposite of progress?",
                                      "&fWhat did the fish say when he swam into the wall? -- Damn",
                                      "&fWhat do you call a sheep with no legs? A cloud.",
                                      "&fHow do you make holy water? You boil the hell out of it.",
                                      "&fFat people are harder to kidnap.",
                                      "&fWhat's the best thing about being 100 y/o? -- No peer pressure",
                                      "&fFailure is not an option -- it comes bundled with Windows.",
                                      "&fYo moma is like HTML: Tiny head, huge body.",
                                      "&f1f u c4n r34d th1s u r34lly n33d t0 g37 l41d",
                                      "&fAs long as there are tests, there will be prayer in schools.",
                                      "&fFor Sale: Parachute. Only used once, never opened.",
                                      "&fWhen everything's coming your way, you're in the wrong lane.",
                                      "&fIf at first you don't succeed, destroy all evidence that you tried.",
                                      "&fThe last thing I want to do is hurt you. But it's still on the list.",
                                      "&fIf I agreed with you we'd both be wrong.",
                                      "&fEnergizer Bunny was arrested, charged with battery.",
                                      "&fI usually take steps to avoid elevators.",
                                      "&fSchrodinger's Cat: Wanted dead and alive.",
                                      "&fIf at first you don't succeed; call it version 1.0",
                                      "&fCONGRESS.SYS Corrupted: Re-boot Washington D.C (Y/n)?",
                                      "&fWe live in a society where pizza gets to your house before the police.",
                                      "&fLight travels faster than sound. This is why some people appear bright until you hear them speak.",
                                      "&fEvening news is where they begin with 'Good evening', and then proceed to tell you why it isn't.",
                                      "&fYou do not need a parachute to skydive. You only need a parachute to skydive twice.",
                                      "&fWhen in doubt, mumble.",
                                      "&fWar does not determine who is right – only who is left...",
                                      "&fI wondered why the frisbee was getting bigger - then it hit me.",
                                      "&fNever argue with a fool, they will lower you to their level, and then beat you with experience.",
                                      "&fThere are 3 kinds of people in the world: Those who can count and those who can't.",
                                      "&fNever hit a man with glasses. Hit him with a baseball bat instead.",
                                      "&fNostalgia isn't what it used to be."};
                Random RandjokeString = new Random();
                if (player.Info.TimesUsedBot == 0)
                {
                    player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                }
                string joker = jokeStrings[RandjokeString.Next(0, jokeStrings.Length)];
                Server.Players.Message("&6Bot&f: " + joker);
                IRC.SendChannelMessage("\u212C&6Bot\u211C: " + joker.Remove(0, 2));
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "protip")
            {
                string[] tipStrings = { "&fStore cheese curls bag in freezer, the colder they are the better they taste.",
                                      "&fTake a screen-shot on the payment confirmation screen of every purchase you make online.",
                                      "&fUse an underscore in front of folder names and other sortable items on your computer to keep those at the top of the list.",
                                      "&fDon't use \"lol\" as a filler word.",
                                      "&fPress F2 to immediately rename a file, no more slow double clicks.",
                                      "&fTake one minute to record your phone's serial number. This may be the one identifying factor if your phone is ever stolen and reset.",
                                      "&fCreate a life binder and keep in it copies of things like your: medical records, SSN, birth photos, etc.",
                                      "&fWhen applying for a job online, save the job description in an email/pdf. You'll be able to prepare even if the post is removed.",
                                      "&fTry the microwave challenge: when microwaving see how much of the kitchen you can clean up.",
                                      "&fGive your shower walls a quick wipe-down after each shower and you'll never fight mildew and hard water stains again.",
                                      "&fOpen your dishwasher as soon as it has finished to allow additional water to evaporate (and get rid of water spots)",
                                      "&fHave a separate account on your laptop for performing presentations.",
                                      "&fWhen going on long roadtrips and don't want to pay for a motel? All Wal-Marts allow people to park over night without being kicked out.",
                                      "&fSprinkle cinnamon on the carpet. When you vacuum, the room will smell like cinnamon instead of your nasty vacuum.",
                                      "&fUse a window squeegee to remove pet hair from carpet.",
                                      "&fIf you see someone griefing, don't try to fix it, tell an admin we have commands that can fix it instantly with no work.",
                                      "&fWant a higher rank? Be nice, don't argue, try your best to get a better impression toward the admins.",
                                      "&fUse travel delay as opportunity to stop rather than get stressed. When the world stands still, let it.",
                                      "&fStop clinging and embrace change as a constant.",
                                      "&fTry and give people the benefit of the doubt if they snap at you. Might be something going on you don't know about.",
                                      "&fFriendship is a gift, not a possession.",
                                      "&fBefore you go to bed, write down only 3 things that you want to do the following day. This is how to prioritize.",
                                      "&fDo something relaxing before going to bed. No electronics.",
                                      "&fWhen in doubt, take a deep breath.",
                                      "&fDefine what's necessary; say no to the rest.",
                                      "&fExpect nothing. Welcome everything.",
                                      "&fGood things come to those who wait… greater things come to those who get off their ass and do anything to make it happen.",
                                      "&fEnds are not bad things, they just mean that something else is about to begin.",
                                      "&fBeat your alarm to wake up? Don't go back to bed, you will feel worse.",
                                      "&fOrgasms cure hiccups.",
                                      "&fTelemarketers aren't allowed to hang up first. The possibilities are endless.",
                                      "&fApples are 10 times more effective at keeping people awake than coffee."};
                Random RandtipString = new Random();
                if (player.Info.TimesUsedBot == 0)
                {
                    player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                }
                string tipper = tipStrings[RandtipString.Next(0, tipStrings.Length)];
                Server.Players.Message("&6Bot&f: " + tipper);
                IRC.SendChannelMessage("\u212C&6Bot\u211C: " + tipper.Remove(0, 2));
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "time")
            {
                TimeSpan time = player.Info.TotalTime;
                TimeSpan timenow = player.Info.TimeSinceLastLogin;
                if (helper == "total")
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + "&f has spent a total of {0:F2}&f hours on this server.", time.TotalHours);
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + "\u211C has spent a total of {0:F2}\u211C hours on this server.", time.TotalHours );
                }
                else
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + "&f has played a total of {0:F2}&f minutes this session.", timenow.TotalMinutes);
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + "\u211C has played a total of {0:F2}\u211C minutes this session.", timenow.TotalMinutes );
                }

                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "promos")
            {
                if (player.Info.Rank.Permissions[(int)Permission.Promote] == true)
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + " &fhas promoted " + player.Info.PromoCount + " players.");
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + " \u211Chas promoted " + player.Info.PromoCount + " players." );
                }
                else
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + " &fcannot promote players yet");
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + " \u211Ccannot promote players yet" );
                }
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "bans")
            {
                if (player.Info.Rank.Permissions[(int)Permission.Ban] == true)
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + " &fhas banned " + player.Info.TimesBannedOthers + " players.");
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + " \u211Chas banned " + player.Info.TimesBannedOthers + " players." );
                }
                else
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + " &fcannot ban yet");
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + " \u211Ccannot ban yet" );
                }

                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "kicks")
            {
                if (player.Info.Rank.Permissions[(int)Permission.Kick] == true)
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + " &fhas kicked " + player.Info.TimesKickedOthers + " players.");
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + " \u211Chas kicked " + player.Info.TimesKickedOthers + " players." );
                }
                else
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + " &fcannot kick yet");
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + " \u211Ccannot kick yet" );
                }
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "clock")
            {
                if (player.Info.TimesUsedBot == 0)
                {
                    player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");
                }
                Server.Players.Message("&6Bot&f: It is " + DateTime.Now.ToLongTimeString());
                Server.Players.Message("&6Bot&f: On a " + DateTime.Now.ToLongDateString());
                IRC.SendChannelMessage("\u212C&6Bot\u211C: It is " + DateTime.Now.ToLongTimeString());
                IRC.SendChannelMessage("\u212C&6Bot\u211C: On a " + DateTime.Now.ToLongDateString());
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "blocks")
            {
                if (helper == "total")
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");
                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + " &fhas built " + player.Info.BlocksBuilt + " blocks, deleted " + player.Info.BlocksDeleted + " and drew " + player.Info.BlocksDrawn + ".");
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + " \u211Chas built " + player.Info.BlocksBuilt + " blocks, deleted " + player.Info.BlocksDeleted + " and drew " + player.Info.BlocksDrawn + "." );
                }
                else
                {
                    if (player.Info.TimesUsedBot == 0)
                    {
                        player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");
                    }
                    Server.Players.Message("&6Bot&f: " + player.ClassyName + " &fhas built " + player.Info.BlocksBuiltThisGame + " blocks and deleted " + player.Info.BlocksDeletedThisGame + " blocks this session.");
                    IRC.SendChannelMessage( "\u212C&6Bot\u211C: " + player.ClassyName + " \u211Chas built " + player.Info.BlocksBuiltThisGame + " blocks and deleted " + player.Info.BlocksDeletedThisGame + " blocks this session." );
                }
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else if (option == "funfact")
            {
                string[] factStrings = { "&fEvery time you lick a stamp, you're consuming 1/10 of a calorie.",
                                      "&fBanging your head against a wall uses 150 calories an hour",
                                      "&fThe average person falls asleep in seven minutes.",
                                      "&fYour stomach has to produce a new layer of mucus every two weeks otherwise it will digest itself.",
                                      "&fPolar bears are left handed.",
                                      "&fAn ostrich's eye is bigger than it's brain.",
                                      "&fBullet proof vests, fire escapes, windshield wipers, and laser printers were all invented by women.",
                                      "&fPearls melt in vinegar.",
                                      "&fCoca Cola was originally green.",
                                      "&fThe youngest Pope was 11 years old.",
                                      "&fA 'jiffy' is an actual unit of time: 1/100th of a second.",
                                      "&fVenus is the only planet that rotates clockwise.",
                                      "&fMost lipstick contains fish scales.",
                                      "&fWomen blink nearly twice as much as men.",
                                      "&fKetchup was sold in the 1830s as medicine.",
                                      "&fStewardesses' is the longest word that is typed with only the left hand.",
                                      "&fThe only 15 letter word that can be spelled without repeating a letter is \"uncopyrightable\".",
                                      "&fMaine is the only state in the USA that is one syllable.",
                                      "&fThe adult human brain weighs about 3 pounds (1,300-1,400 g).",
                                      "&fPirates of old spoke just like everyone else. The 'pirate accent' was invented for the 1950 Disney movie, Treasure Island.",
                                      "&fAlmonds are a member of the peach family.",
                                      "&fParaguay's flag is the only national flag where the front and the back are different.",
                                      "&fIn 1999, Furbies were banned by the Pentagon, based on the fear that the dolls would mimic top-secret discussions.",
                                      "&fA dime has 118 ridges around the edge.",
                                      "&fPeanuts are one of the ingredients of dynamite.",
                                      "&fTwo thirds of the world's eggplant is grown in New Jersey.",
                                      "&fNo piece of paper can be folded in half more than 7 times.",
                                      "&fAmerican Airlines saved $40,000 in 1987 by eliminating 1 olive from each salad served in first-class.",
                                      "&fIn 1998, more fast-food employees were murdered on the job than police officers.",
                                      "&fFortune cookies were actually invented in America, in 1918, by Charles Jung.",
                                      "&fTYPEWRITER is the longest word that can be made using the letters only on one row of the keyboard."};
                Random RandfactString = new Random();
                if (player.Info.TimesUsedBot == 0)
                {
                    player.Message("&6Bot&f: This is your first time using &6Bot&e, I suggest you use \"/Help Bot\" to further understand how I work.");

                }
                string facter = factStrings[RandfactString.Next(0, factStrings.Length)];
                Server.Players.Message("&6Bot&f: " + facter);
                IRC.SendChannelMessage("\u212C&6Bot\u211C: " + facter.Remove(0, 2));
                player.Info.LastTimeUsedBot = DateTime.Now;
                player.Info.TimesUsedBot = (player.Info.TimesUsedBot + 1);
            }
            else
            {
                player.Message(CdBot.Help);
                return;
            }


        }
        #endregion        
        #region Ban / Unban

        static readonly CommandDescriptor CdBan = new CommandDescriptor
        {
            Name = "Ban",
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Ban },
            Usage = "/Ban PlayerName [Reason]",
            Help = "Bans a specified player by name. Note: Does NOT ban IP. " +
                   "Any text after the player name will be saved as a memo. ",
            Handler = BanHandler
        };

        static void BanHandler(Player player, CommandReader cmd)
        {
            string targetName = cmd.Next();
            if (targetName == null)
            {
                CdBan.PrintUsage(player);
                return;
            }
            PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                       targetName,
                                                                       SearchOptions.ReturnSelfIfOnlyMatch);
            if (target == null) return;
            if (target == player.Info)
            {
                player.Message("You cannot &H/Ban&S yourself.");
                return;
            }
            string reason = cmd.NextAll();
            try
            {
                Player targetPlayer = target.PlayerObject;
                target.Ban(player, reason, true, true);
                WarnIfOtherPlayersOnIP(player, target, targetPlayer);
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
                if (ex.ErrorCode == PlayerOpExceptionCode.ReasonRequired)
                {
                    FreezeIfAllowed(player, target);
                }
            }
        }



        static readonly CommandDescriptor CdBanIP = new CommandDescriptor
        {
            Name = "BanIP",
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Ban, Permission.BanIP },
            Usage = "/BanIP PlayerName|IPAddress [Reason]",
            Help = "Bans the player's name and IP. If player is not online, last known IP associated with the name is used. " +
                   "You can also type in the IP address directly. " +
                   "Any text after PlayerName/IP will be saved as a memo. ",
            Handler = BanIPHandler
        };

        static void BanIPHandler(Player player, CommandReader cmd)
        {
            string targetNameOrIP = cmd.Next();
            if (targetNameOrIP == null)
            {
                CdBanIP.PrintUsage(player);
                return;
            }
            string reason = cmd.NextAll();

            IPAddress targetAddress;
            if (IPAddressUtil.IsIP(targetNameOrIP) && IPAddress.TryParse(targetNameOrIP, out targetAddress))
            {
                try
                {
                    targetAddress.BanIP(player, reason, true, true);
                }
                catch (PlayerOpException ex)
                {
                    player.Message(ex.MessageColored);
                }
            }
            else
            {
                PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                           targetNameOrIP,
                                                                           SearchOptions.ReturnSelfIfOnlyMatch);
                if (target == null) return;
                if (target == player.Info)
                {
                    player.Message("You cannot &H/BanIP&S yourself.");
                    return;
                }
                try
                {
                    if (target.LastIP.Equals(IPAddress.Any) || target.LastIP.Equals(IPAddress.None))
                    {
                        target.Ban(player, reason, true, true);
                    }
                    else
                    {
                        target.BanIP(player, reason, true, true);
                    }
                }
                catch (PlayerOpException ex)
                {
                    player.Message(ex.MessageColored);
                    if (ex.ErrorCode == PlayerOpExceptionCode.ReasonRequired)
                    {
                        FreezeIfAllowed(player, target);
                    }
                }
            }
        }



        static readonly CommandDescriptor CdBanAll = new CommandDescriptor
        {
            Name = "BanAll",
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Ban, Permission.BanIP, Permission.BanAll },
            Usage = "/BanAll PlayerName|IPAddress [Reason]",
            Help = "Bans the player's name, IP, and all other names associated with the IP. " +
                   "If player is not online, last known IP associated with the name is used. " +
                   "You can also type in the IP address directly. " +
                   "Any text after PlayerName/IP will be saved as a memo. ",
            Handler = BanAllHandler
        };

        static void BanAllHandler(Player player, CommandReader cmd)
        {
            string targetNameOrIP = cmd.Next();
            if (targetNameOrIP == null)
            {
                CdBanAll.PrintUsage(player);
                return;
            }
            string reason = cmd.NextAll();

            IPAddress targetAddress;
            if (IPAddressUtil.IsIP(targetNameOrIP) && IPAddress.TryParse(targetNameOrIP, out targetAddress))
            {
                try
                {
                    targetAddress.BanAll(player, reason, true, true);
                }
                catch (PlayerOpException ex)
                {
                    player.Message(ex.MessageColored);
                }
            }
            else
            {
                PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                           targetNameOrIP,
                                                                           SearchOptions.ReturnSelfIfOnlyMatch);
                if (target == null) return;
                if (target == player.Info)
                {
                    player.Message("You cannot &H/BanAll&S yourself.");
                    return;
                }
                try
                {
                    if (target.LastIP.Equals(IPAddress.Any) || target.LastIP.Equals(IPAddress.None))
                    {
                        target.Ban(player, reason, true, true);
                    }
                    else
                    {
                        target.BanAll(player, reason, true, true);
                    }
                }
                catch (PlayerOpException ex)
                {
                    player.Message(ex.MessageColored);
                    if (ex.ErrorCode == PlayerOpExceptionCode.ReasonRequired)
                    {
                        FreezeIfAllowed(player, target);
                    }
                }
            }
        }



        static readonly CommandDescriptor CdUnban = new CommandDescriptor
        {
            Name = "Unban",
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Ban },
            Usage = "/Unban PlayerName [Reason]",
            Help = "Removes ban for a specified player. Does NOT remove associated IP bans. " +
                   "Any text after the player name will be saved as a memo. ",
            Handler = UnbanHandler
        };

        static void UnbanHandler(Player player, CommandReader cmd)
        {
            string targetName = cmd.Next();
            if (targetName == null)
            {
                CdUnban.PrintUsage(player);
                return;
            }
            PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                       targetName,
                                                                       SearchOptions.ReturnSelfIfOnlyMatch);
            if (target == null) return;
            if (target == player.Info)
            {
                player.Message("You cannot &H/Unban&S yourself.");
                return;
            }
            string reason = cmd.NextAll();
            try
            {
                target.Unban(player, reason, true, true);
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
            }
        }



        static readonly CommandDescriptor CdUnbanIP = new CommandDescriptor
        {
            Name = "UnbanIP",
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Ban, Permission.BanIP },
            Usage = "/UnbanIP PlayerName|IPaddress [Reason]",
            Help = "Removes ban for a specified player's name and last known IP. " +
                   "You can also type in the IP address directly. " +
                   "Any text after the player name will be saved as a memo. ",
            Handler = UnbanIPHandler
        };

        static void UnbanIPHandler(Player player, CommandReader cmd)
        {
            string targetNameOrIP = cmd.Next();
            if (targetNameOrIP == null)
            {
                CdUnbanIP.PrintUsage(player);
                return;
            }
            string reason = cmd.NextAll();

            try
            {
                IPAddress targetAddress;
                if (IPAddressUtil.IsIP(targetNameOrIP) && IPAddress.TryParse(targetNameOrIP, out targetAddress))
                {
                    targetAddress.UnbanIP(player, reason, true, true);
                }
                else
                {
                    PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                               targetNameOrIP,
                                                                               SearchOptions.ReturnSelfIfOnlyMatch);
                    if (target == null) return;
                    if (target == player.Info)
                    {
                        player.Message("You cannot &H/UnbanIP&S yourself.");
                        return;
                    }
                    if (target.LastIP.Equals(IPAddress.Any) || target.LastIP.Equals(IPAddress.None))
                    {
                        target.Unban(player, reason, true, true);
                    }
                    else
                    {
                        target.UnbanIP(player, reason, true, true);
                    }
                }
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
            }
        }



        static readonly CommandDescriptor CdUnbanAll = new CommandDescriptor
        {
            Name = "UnbanAll",
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Ban, Permission.BanIP, Permission.BanAll },
            Usage = "/UnbanAll PlayerName|IPaddress [Reason]",
            Help = "Removes ban for a specified player's name, last known IP, and all other names associated with the IP. " +
                   "You can also type in the IP address directly. " +
                   "Any text after the player name will be saved as a memo. ",
            Handler = UnbanAllHandler
        };

        static void UnbanAllHandler(Player player, CommandReader cmd)
        {
            string targetNameOrIP = cmd.Next();
            if (targetNameOrIP == null)
            {
                CdUnbanAll.PrintUsage(player);
                return;
            }
            string reason = cmd.NextAll();

            try
            {
                IPAddress targetAddress;
                if (IPAddressUtil.IsIP(targetNameOrIP) && IPAddress.TryParse(targetNameOrIP, out targetAddress))
                {
                    targetAddress.UnbanAll(player, reason, true, true);
                }
                else
                {
                    PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                               targetNameOrIP,
                                                                               SearchOptions.ReturnSelfIfOnlyMatch);
                    if (target == null) return;
                    if (target == player.Info)
                    {
                        player.Message("You cannot &H/UnbanAll&S yourself.");
                        return;
                    }
                    if (target.LastIP.Equals(IPAddress.Any) || target.LastIP.Equals(IPAddress.None))
                    {
                        target.Unban(player, reason, true, true);
                    }
                    else
                    {
                        target.UnbanAll(player, reason, true, true);
                    }
                }
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
            }
        }


        static readonly CommandDescriptor CdBanEx = new CommandDescriptor
        {
            Name = "BanEx",
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Ban, Permission.BanIP },
            Usage = "/BanEx +PlayerName&S or &H/BanEx -PlayerName",
            Help = "Adds or removes an IP-ban exemption for an account. " +
                   "Exempt accounts can log in from any IP, including banned ones.",
            Handler = BanExHandler
        };

        static void BanExHandler(Player player, CommandReader cmd)
        {
            string playerName = cmd.Next();
            if (playerName == null || playerName.Length < 2 || (playerName[0] != '-' && playerName[0] != '+'))
            {
                CdBanEx.PrintUsage(player);
                return;
            }
            bool addExemption = (playerName[0] == '+');
            string targetName = playerName.Substring(1);
            PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player, targetName, SearchOptions.Default);
            if (target == null) return;

            switch (target.BanStatus)
            {
                case BanStatus.Banned:
                    if (addExemption)
                    {
                        player.Message("Player {0}&S is currently banned. Unban before adding an exemption.",
                                        target.ClassyName);
                    }
                    else
                    {
                        player.Message("Player {0}&S is already banned. There is no exemption to remove.",
                                        target.ClassyName);
                    }
                    break;
                case BanStatus.IPBanExempt:
                    if (addExemption)
                    {
                        player.Message("IP-Ban exemption already exists for player {0}", target.ClassyName);
                    }
                    else
                    {
                        player.Message("IP-Ban exemption removed for player {0}",
                                        target.ClassyName);
                        target.BanStatus = BanStatus.NotBanned;
                        Logger.Log(LogType.UserActivity,
                                    "{0} removed a ban exemption for {1}", player.Name, target.Name);

                    }
                    break;
                case BanStatus.NotBanned:
                    if (addExemption)
                    {
                        player.Message("IP-Ban exemption added for player {0}",
                                        target.ClassyName);
                        target.BanStatus = BanStatus.IPBanExempt;
                        Logger.Log(LogType.UserActivity,
                                    "{0} added a ban exemption for {1}", player.Name, target.Name);
                    }
                    else
                    {
                        player.Message("No IP-Ban exemption exists for player {0}",
                                        target.ClassyName);
                    }
                    break;
            }
        }

        #endregion
        #region Kick

        static readonly CommandDescriptor CdKick = new CommandDescriptor {
            Name = "Kick",
            Aliases = new[] { "k" },
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Kick },
            Usage = "/Kick PlayerName [Reason]",
            Help = "Kicks the specified player from the server. " +
                   "Optional kick reason/message is shown to the kicked player and logged." +
                   "Kicking will also make them spawn in &fTutorial &sworld next time they log in." +
                   "(In the mean time it will show up as them not having read the rules in their info)",
            Handler = KickHandler
        };

        static void KickHandler( Player player, CommandReader cmd ) {
            string name = cmd.Next();
            if( name == null ) {
                player.Message( "Usage: &H/Kick PlayerName [Message]" );
                return;
            }
            // find the target
            Player target = Server.FindPlayerOrPrintMatches(player, name, SearchOptions.Default);
            if( target == null ) return;

            string reason = cmd.NextAll();
            DateTime previousKickDate = target.Info.LastKickDate;
            string previousKickedBy = target.Info.LastKickByClassy;
            string previousKickReason = target.Info.LastKickReason;

            // do the kick
            try {
                Player targetPlayer = target;
                target.Kick( player, reason, LeaveReason.Kick, true, true, true );
                target.Info.HasRTR = false;
                WarnIfOtherPlayersOnIP( player, target.Info, targetPlayer );

            } catch( PlayerOpException ex ) {
                player.Message( ex.MessageColored );
                if( ex.ErrorCode == PlayerOpExceptionCode.ReasonRequired ) {
                    FreezeIfAllowed( player, target.Info );
                }
                return;
            }

            // warn player if target has been kicked before
            if( target.Info.TimesKicked > 1 ) {
                player.Message( "Warning: {0}&S has been kicked {1} times before.",
                                target.ClassyName, target.Info.TimesKicked - 1 );
                if( previousKickDate != DateTime.MinValue ) {
                    player.Message( "Most recent kick was {0} ago, by {1}",
                                    DateTime.UtcNow.Subtract( previousKickDate ).ToMiniString(),
                                    previousKickedBy );
                }
                if( !String.IsNullOrEmpty( previousKickReason ) ) {
                    player.Message( "Most recent kick reason was: {0}",
                                    previousKickReason );
                }
            }
        }

        #endregion
        #region Changing Rank (Promotion / Demotion)

        static readonly CommandDescriptor CdRank = new CommandDescriptor
        {
            Name = "Rank",
            Aliases = new[] { "user", "promote", "demote" },
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Promote, Permission.Demote },
            AnyPermission = true,
            IsConsoleSafe = true,
            Usage = "/Rank PlayerName RankName [Reason]",
            Help = "Changes the rank of a player to a specified rank. " +
                   "Any text specified after the RankName will be saved as a memo.",
            Handler = RankHandler
        };

        static void RankHandler([NotNull] Player player, [NotNull] CommandReader cmd)
        {
            string name = cmd.Next();
            string newRankName = cmd.Next();

            // Check arguments
            if (name == null || newRankName == null)
            {
                CdRank.PrintUsage(player);
                player.Message("See &H/Ranks&S for list of ranks.");
                return;
            }

            // Parse rank name
            Rank newRank = RankManager.FindRank(newRankName);
            if (newRank == null)
            {
                player.MessageNoRank(newRankName);
                return;
            }

            if (name == "-")
            {
                if (player.LastUsedPlayerName != null)
                {
                    name = player.LastUsedPlayerName;
                }
                else
                {
                    player.Message("Cannot repeat player name: you haven't used any names yet.");
                    return;
                }
            }
            PlayerInfo targetInfo;

            // Find player by name
            if (name.StartsWith("!"))
            {
                name = name.Substring(1);
                Player target = Server.FindPlayerExact(player, name, SearchOptions.IncludeSelf);
                if (target == null)
                {
                    player.MessageNoPlayer(name);
                    return;
                }
                targetInfo = target.Info;
            }
            else
            {
                targetInfo = PlayerDB.FindPlayerInfoExact(name);
            }

            // Handle non-existent players
            if (targetInfo == null)
            {
                if (!player.Can(Permission.EditPlayerDB))
                {
                    player.MessageNoPlayer(name);
                    return;
                }
                if (!Player.IsValidPlayerName(name))
                {
                    player.MessageInvalidPlayerName(name);
                    CdRank.PrintUsage(player);
                    return;
                }
                if (cmd.IsConfirmed)
                {
                    if (newRank > RankManager.DefaultRank)
                    {
                        targetInfo = PlayerDB.AddFakeEntry(name, RankChangeType.Promoted);
                    }
                    else
                    {
                        targetInfo = PlayerDB.AddFakeEntry(name, RankChangeType.Demoted);
                    }
                }
                else
                {
                    Logger.Log(LogType.UserActivity,
                                "Rank: Asked {0} to confirm adding unrecognized name \"{1}\" to the database.",
                                player.Name,
                                name);
                    player.Confirm(cmd,
                                    "Warning: Player \"{0}\" is not in the database (possible typo). Type the full name or",
                                    name);
                    return;
                }
            }
            try
            {
                player.LastUsedPlayerName = targetInfo.Name;
                Rank oldrank = targetInfo.Rank;
                targetInfo.ChangeRank(player, newRank, cmd.NextAll(), true, true, false);
                if (oldrank < targetInfo.Rank)
                {
                    if (targetInfo.IsOnline && !targetInfo.IsHidden)
                    {
                        Server.Players.Message("&6Bot&f: Hey " + targetInfo.ClassyName + "&f!\n" +
                                               "&fCongrats on getting " + newRank.ClassyName + "&f!");
                        IRC.SendChannelMessage( "\u212C&6Bot\u211C: Hey " + targetInfo.ClassyName + "\u211C! Congrats on getting " + newRank.ClassyName + "\u211C!" );
                    }
                    else if (!targetInfo.IsOnline || targetInfo.IsHidden)
                    {
                        Server.Players.Message("&6Bot&f: Hey guys! Remember to congrat " + targetInfo.ClassyName + " &fon getting promoted to " + newRank.ClassyName + "&f!");
                        IRC.SendChannelMessage( "\u212C&6Bot\u211C: Hey guys! Remember to congrat " + targetInfo.ClassyName + " \u211Con getting promoted to " + newRank.ClassyName + "\u211C!" );
                    }
                }
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
            }            
        }

        #endregion
        #region Hide

        static readonly CommandDescriptor CdHide = new CommandDescriptor
        {
            Name = "Hide",
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Hide },
            Usage = "/Hide [silent]",
            Help = "Enables invisible mode. It looks to other players like you left the server, " +
                   "but you can still do anything - chat, build, delete, type commands - as usual. " +
                   "Great way to spy on griefers and scare newbies. " +
                   "Call &H/Unhide&S to reveal yourself.",
            Handler = HideHandler
        };

        static void HideHandler([NotNull] Player player, [NotNull] CommandReader cmd)
        {
            if (player.Info.IsHidden)
            {
                player.Message("You are already hidden.");
                return;
            }

            string silentString = cmd.Next();
            bool silent = false;
            if (silentString != null)
            {
                silent = silentString.Equals("silent", StringComparison.OrdinalIgnoreCase);
            }

            player.Info.IsHidden = true;
            if (silent)
            {
                player.Message("&8You are now hidden (silent).");
            }
            else
            {
                player.Message("&8You are now hidden.");
            }

            // to make it look like player just logged out in /Info
            player.Info.LastSeen = DateTime.UtcNow;

            if (!silent && ConfigKey.ShowConnectionMessages.Enabled())
            {
                Server.Players.CantSee(player).Message("{0}&s left the server.", player.ClassyName);
            }

            // for aware players: notify
            Server.Players.CanSee(player).Message("&SPlayer {0}&S is now hidden.", player.ClassyName);

            Player.RaisePlayerHideChangedEvent(player, true, silent);
            foreach (Player p1 in Server.Players)
            {
                if (p1.SupportsExtPlayerList)
                {
                    p1.Send(Packet.MakeExtRemovePlayerName(player.NameID));
                }
            }
            Server.UpdateTabList();
        }


        static readonly CommandDescriptor CdUnhide = new CommandDescriptor
        {
            Name = "Unhide",
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Hide },
            Usage = "/Unhide [silent]",
            Help = "Disables the &H/Hide&S invisible mode. " +
                   "It looks to other players like you just joined the server.",
            Handler = UnhideHandler
        };

        static void UnhideHandler([NotNull] Player player, [NotNull] CommandReader cmd)
        {
            World playerWorld = player.World;
            if (playerWorld == null) PlayerOpException.ThrowNoWorld(player);

            if (!player.Info.IsHidden)
            {
                player.Message("You are not currently hidden.");
                return;
            }
            bool silent = cmd.HasNext;

            // for aware players: notify
            Server.Players
                  .CanSee(player)
                  .Message("&SPlayer {0}&S is no longer hidden.",
                            player.ClassyName);

            // for unaware players: fake a join message
            if (!silent)
            {
                if (ConfigKey.ShowConnectionMessages.Enabled())
                {
                    string msg = Server.MakePlayerConnectedMessage(player, false, playerWorld);
                    Server.Players.CantSee(player).Message(msg);
                }
            }
            player.Info.IsHidden = false;
            if (silent)
            {
                player.Message("&8You are no longer hidden (silent).");
            }
            else
            {
                player.Message("&8You are no longer hidden.");
            }

            Player.RaisePlayerHideChangedEvent(player, false, silent);
            foreach (Player p1 in Server.Players)
            {
                if (p1.SupportsExtPlayerList)
                {
                    p1.Send(Packet.MakeExtRemovePlayerName(player.NameID));
                }
            }
            Server.UpdateTabList();
        }

        #endregion
        #region Set Spawn

        static readonly CommandDescriptor CdSetSpawn = new CommandDescriptor {
            Name = "SetSpawn",
            Category = CommandCategory.Moderation | CommandCategory.World,
            Permissions = new[] { Permission.SetSpawn },
            Help = "Assigns your current location to be the spawn point of the map/world. " +
                   "If an optional PlayerName param is given, the spawn point of only that player is changed instead.",
            Usage = "/SetSpawn [PlayerName]",
            Handler = SetSpawnHandler
        };

        static void SetSpawnHandler( Player player, CommandReader cmd ) {
            World playerWorld = player.World;
            if( playerWorld == null ) PlayerOpException.ThrowNoWorld( player );


            string playerName = cmd.Next();
            if( playerName == null ) {
                Map map = player.WorldMap;
                map.Spawn = player.Position;
                player.TeleportTo( map.Spawn );
                player.Send( Packet.MakeAddEntity( Packet.SelfId, player.ListName, player.Position ) );
                player.Message( "New spawn point saved." );
                Logger.Log( LogType.UserActivity,
                            "{0} changed the spawned point.",
                            player.Name );

            } else if( player.Can( Permission.Bring ) ) {
                Player[] infos = playerWorld.FindPlayers( player, playerName );
                if( infos.Length == 1 ) {
                    Player target = infos[0];
                    player.LastUsedPlayerName = target.Name;
                    if( player.Can( Permission.Bring, target.Info.Rank ) ) {
                        target.Send( Packet.MakeAddEntity( Packet.SelfId, target.ListName, player.Position ) );
                    } else {
                        player.Message( "You may only set spawn of players ranked {0}&S or lower.",
                                        player.Info.Rank.GetLimit( Permission.Bring ).ClassyName );
                        player.Message( "{0}&S is ranked {1}", target.ClassyName, target.Info.Rank.ClassyName );
                    }

                } else if( infos.Length > 0 ) {
                    player.MessageManyMatches( "player", infos );

                } else {
                    infos = Server.FindPlayers(playerName, SearchOptions.IncludeSelf);
                    if( infos.Length > 0 ) {
                        player.Message( "You may only set spawn of players on the same world as you." );
                    } else {
                        player.MessageNoPlayer( playerName );
                    }
                }
            } else {
                player.MessageNoAccess( CdSetSpawn );
            }
        }

        #endregion
        #region Freeze

        static readonly CommandDescriptor CdFreeze = new CommandDescriptor
        {
            Name = "Freeze",
            Aliases = new[] { "f" },
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Freeze },
            Usage = "/Freeze PlayerName",
            Help = "Freezes the specified player in place. " +
                   "This is usually effective, but not hacking-proof. " +
                   "To release the player, use &H/unfreeze PlayerName",
            Handler = FreezeHandler
        };

        static void FreezeHandler(Player player, CommandReader cmd)
        {
            string targetName = cmd.Next();
            if (targetName == null)
            {
                CdFreeze.PrintUsage(player);
                return;
            }

            PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                       targetName,
                                                                       SearchOptions.ReturnSelfIfOnlyMatch);
            if (target == null) return;
            if (target == player.Info)
            {
                player.Message("You cannot &H/Freeze&S yourself.");
                return;
            }

            try
            {
                target.Freeze(player, true, true);
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
            }
        }


        static readonly CommandDescriptor CdUnfreeze = new CommandDescriptor
        {
            Name = "Unfreeze",
            Aliases = new[] { "uf" },
            Category = CommandCategory.Moderation,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Freeze },
            Usage = "/Unfreeze PlayerName",
            Help = "Releases the player from a frozen state. See &H/Help Freeze&S for more information.",
            Handler = UnfreezeHandler
        };

        static void UnfreezeHandler(Player player, CommandReader cmd)
        {
            string targetName = cmd.Next();
            if (targetName == null)
            {
                CdFreeze.PrintUsage(player);
                return;
            }

            PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                       targetName,
                                                                       SearchOptions.ReturnSelfIfOnlyMatch);
            if (target == null) return;
            if (target == player.Info)
            {
                player.Message("You cannot &H/Unfreeze&S yourself.");
                return;
            }

            try
            {
                target.Unfreeze(player, true, true);
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
            }
        }

        #endregion
        #region TPDeny
        static readonly CommandDescriptor CdTPDeny = new CommandDescriptor
        {
            Name = "TPDeny",
            Category = CommandCategory.New,
            Aliases = new[] { "tpd", "teleportdeny", "tptoggle" },
            Permissions = new[] { Permission.ReadStaffChat },
            Usage = "/TPDeny On/Off",
            Help = "Determines if lower ranks can teleport to you or not.",
            Handler = TPDenyHandler
        };

        static void TPDenyHandler(Player player, CommandReader cmd)
        {
            if (cmd.HasNext)
            {
                string state = cmd.Next();
                if (state.ToLower() == "on" || state.ToLower() == "yes")
                {
                    player.Info.TPDeny = true;
                    player.Message("TPDeny: &2On");
                    player.Message("Lower ranks can no longer teleport to you.");
                    return;
                }
                if (state.ToLower() == "off" || state.ToLower() == "no")
                {
                    player.Info.TPDeny = false;
                    player.Message("TPDeny: &4Off");
                    player.Message("Lower ranks can now teleport to you.");
                    return;
                }
                if (state.ToLower() == "state" || state.ToLower() == "what" || state.ToLower() == "current")
                {
                    if (player.Info.TPDeny == false)
                    {
                        player.Message("TPDeny: &4Off");
                    }
                    if (player.Info.TPDeny == true)
                    {
                        player.Message("TPDeny: &2On");
                    }
                    return;
                }
                else
                {
                    CdTPDeny.PrintUsage(player);
                    return;
                }
            }
            if (player.Info.TPDeny == true)
            {
                player.Info.TPDeny = false;
                player.Message("TPDeny: &4Off");
                player.Message("Lower ranks can now teleport to you.");
                return;
            }
            else
            {
                player.Info.TPDeny = true;
                player.Message("TPDeny: &2On");
                player.Message("Lower ranks can no longer teleport to you.");
                return;
            }
        }
        #endregion
        #region TP

        static readonly CommandDescriptor CdTeleport = new CommandDescriptor {
            Name = "TP",
            Aliases = new[] { "teleport", "to" },
            Category = CommandCategory.New,
            Permissions = new[] { Permission.Teleport },
            Usage = "/TP PlayerName&S or &H/TP X Y Z [R L]&s or &h/TP Random",
            Help = "Teleports you to a specified player's location. " +
                   "If coordinates are given, teleports to that location." +
                   "Or teleports you to a random location at your own height level.",
            Handler = TeleportHandler
        };

        static void TeleportHandler( Player player, CommandReader cmd ) {
            string name = cmd.Next();
            if( name == null ) {
                CdTeleport.PrintUsage( player );
                return;
            }
            if (player.World.Name.ToLower() == "maze")
            {
                player.Message("Hey no cheating!");
                return; 
            }
            if (name == "zone")
            {
                string zoneName = cmd.Next();
                if (zoneName == null)
                {
                    player.Message("No zone name specified. See &H/Help tpzone");
                    return;
                }
                else
                {
                    Zone zone = player.World.Map.Zones.Find(zoneName);
                    if (zone == null)
                    {
                        player.MessageNoZone(zoneName);
                        return;
                    }
                    int zoneX = (zone.Bounds.XMin + zone.Bounds.XMax) / 2;
                    int zoneY = (zone.Bounds.YMin + zone.Bounds.YMax) / 2;
                    int zoneZ = (zone.Bounds.ZMin + zone.Bounds.ZMax) / 2;
                retry2:
                    if (player.World.map.GetBlock(zoneX, zoneY, zoneZ - 1) == Block.Air)
                    {
                        zoneZ = zoneZ - 1;
                        goto retry2;
                    }
                retry:
                    if (player.World.map.GetBlock(zoneX, zoneY, zoneZ) != Block.Air || player.World.map.GetBlock(zoneX, zoneY, zoneZ + 1) != Block.Air)
                    {
                        zoneZ = zoneZ + 1;
                        goto retry;
                    }
                    Position zPos = new Position((zoneX) * 32 + 16, (zoneY) * 32 + 16, (zoneZ) * 32 + 64);
                    player.TeleportTo((zPos));
                    player.Message("&sTeleporting you to zone " + zone.ClassyName);
                    return;
                }
            }
            if (name == "random" || name == "rand")
            {
                Random rand = new Random();
                int x = rand.Next(0, player.WorldMap.Width);
                int y = rand.Next(0, player.WorldMap.Length);
                int z = player.Position.Z / 32 + 1;
            retry2:
                if (player.World.map.GetBlock(x, y, z - 3) == Block.Air)
                {
                    z = z - 1;
                    goto retry2;
                }
            retry:
                if (player.World.map.GetBlock(x, y, z - 2) != Block.Air || player.World.map.GetBlock(x, y, z - 1) != Block.Air)
                {
                    z = z + 1;
                    goto retry;
                }

                player.TeleportTo(new Position
                {
                    X = (short)(x * 32 + 16),
                    Y = (short)(y * 32 + 16),
                    Z = (short)(z * 32 + 16),
                    R = player.Position.R,
                    L = player.Position.L
                });
                player.Message("Teleported to: ({0}, {1}, {2})", x, y, z);
                return;
            }

            if( cmd.Next() != null ) {
                cmd.Rewind();
                int x, y, z, rot, lot;
                rot = player.Position.R;
                lot = player.Position.L;
                if( cmd.NextInt( out x ) && cmd.NextInt( out y ) && cmd.NextInt( out z ) ) {
                    if (cmd.HasNext)
                    {
                        if (cmd.HasNext)
                        {
                            if (cmd.NextInt(out rot) && cmd.NextInt(out lot))
                            {
                                if (rot > 255 || rot < 0)
                                {
                                    player.Message("R must be inbetween 0 and 255. Set to player R");
                                }
                                if (lot > 255 || lot < 0)
                                {
                                    player.Message("L must be inbetween 0 and 255. Set to player L");
                                }
                            }
                        }
                    }
                    if( x <= -1024 || x >= 1024 || y <= -1024 || y >= 1024 || z <= -1024 || z >= 1024 ) {
                        player.Message( "Coordinates are outside the valid range!" );

                    } else {
                        player.TeleportTo( new Position {
                            X = (short)(x * 32 + 16),
                            Y = (short)(y * 32 + 16),
                            Z = (short)(z * 32 + 48),
                            R = (byte)rot,
                            L = (byte)lot
                        } );
                    }
                } else {
                    CdTeleport.PrintUsage( player );
                }

            } else {
                if( name == "-" ) {
                    if( player.LastUsedPlayerName != null ) {
                        name = player.LastUsedPlayerName;
                    } else {
                        player.Message( "Cannot repeat player name: you haven't used any names yet." );
                        return;
                    }
                }
                Player[] matches = Server.FindPlayers(player, name, SearchOptions.Default);
                if( matches.Length == 1 ) {
                    Player target = matches[0];
                    World targetWorld = target.World;
                    if( targetWorld == null ) PlayerOpException.ThrowNoWorld( target );
                    if (target.Info.TPDeny && target.Info.Rank >= player.Info.Rank)
                    {
                        player.Message("&CThis player does not want people teleporting to them");
                        player.Message("Cannot teleport to {0}&S.",
                                    target.ClassyName,
                                    targetWorld.ClassyName,
                                    targetWorld.AccessSecurity.MinRank.ClassyName);
                        return;
                    }

                    if( targetWorld == player.World ) {
                        player.TeleportTo( target.Position );

                    } else {
                        switch( targetWorld.AccessSecurity.CheckDetailed( player.Info ) ) {
                            case SecurityCheckResult.Allowed:
                            case SecurityCheckResult.WhiteListed:
                                if (player.Info.Rank.Name == "Banned")
                                {
                                    player.Message("&CYou can not change worlds while banned.");
                                    player.Message("Cannot teleport to {0}&S.",
                                                target.ClassyName,
                                                targetWorld.ClassyName,
                                                targetWorld.AccessSecurity.MinRank.ClassyName);
                                    break;
                                }  
                                if( targetWorld.IsFull ) {
                                    player.Message( "Cannot teleport to {0}&S because world {1}&S is full.",
                                                    target.ClassyName,
                                                    targetWorld.ClassyName );
                                    player.Message("Cannot teleport to {0}&S.",
                                                target.ClassyName,
                                                targetWorld.ClassyName,
                                                targetWorld.AccessSecurity.MinRank.ClassyName);
                                    break;
                                }
                                player.StopSpectating();
                                player.JoinWorld( targetWorld, WorldChangeReason.Tp, target.Position );
                                break;
                            case SecurityCheckResult.BlackListed:
                                player.Message( "Cannot teleport to {0}&S because you are blacklisted on world {1}",
                                                target.ClassyName,
                                                targetWorld.ClassyName );
                                break;
                            case SecurityCheckResult.RankTooLow:
                                if (player.Info.Rank.Name == "Banned")
                                {
                                    player.Message("&CYou can not change worlds while banned.");
                                    player.Message("Cannot teleport to {0}&S.",
                                                target.ClassyName,
                                                targetWorld.ClassyName,
                                                targetWorld.AccessSecurity.MinRank.ClassyName);
                                    break;
                                }
                                
                                if (targetWorld.IsFull)
                                {
                                    if (targetWorld.IsFull)
                                    {
                                        player.Message("Cannot teleport to {0}&S because world {1}&S is full.",
                                                        target.ClassyName,
                                                        targetWorld.ClassyName);
                                        player.Message("Cannot teleport to {0}&S.",
                                                    target.ClassyName,
                                                    targetWorld.ClassyName,
                                                    targetWorld.AccessSecurity.MinRank.ClassyName);
                                        break;
                                    }
                                    player.StopSpectating();
                                    player.JoinWorld(targetWorld, WorldChangeReason.Tp, target.Position);
                                    break;
                                }                                
                                player.Message( "Cannot teleport to {0}&S because world {1}&S requires {2}+&S to join.",
                                                target.ClassyName,
                                                targetWorld.ClassyName,
                                                targetWorld.AccessSecurity.MinRank.ClassyName );
                                break;
                        }
                    }

                } else if( matches.Length > 1 ) {
                    player.MessageManyMatches( "player", matches );

                } 
            }
        }

        #endregion
        #region TPP

        static readonly CommandDescriptor CdTeleportP = new CommandDescriptor
        {
            Name = "TPP",
            Aliases = new[] { "teleportprecise"},
            Category = CommandCategory.New,
            Permissions = new[] { Permission.Teleport },
            IsHidden = true,
            Usage = "/TPP X Y Z [R L]",
            Help = "Teleports to precise location, one block is 32 units.",
            Handler = TeleportPHandler
        };

        static void TeleportPHandler(Player player, CommandReader cmd)
        {
            string name = cmd.Next();
            if (name == null)
            {
                CdTeleportP.PrintUsage(player);
                return;
            }
            if (cmd.Next() != null)
            {
                cmd.Rewind();
                int x, y, z, rot, lot;
                rot = player.Position.R;
                lot = player.Position.L;
                if (cmd.NextInt(out x) && cmd.NextInt(out y) && cmd.NextInt(out z))
                {
                    if (cmd.HasNext)
                    {
                        if (cmd.HasNext)
                        {
                            if (cmd.NextInt(out rot) && cmd.NextInt(out lot))
                            {
                                if (rot > 255 || rot < 0)
                                {
                                    player.Message("R must be inbetween 0 and 255. Set to player R");
                                }
                                if (lot > 255 || lot < 0)
                                {
                                    player.Message("L must be inbetween 0 and 255. Set to player L");
                                }
                            }
                        }
                    }
                    if (x <= -32768 || x >= 32768 || y <= -32768 || y >= 32768 || z <= -32768 || z >= 32768)
                    {
                        player.Message("Coordinates are outside the valid range!");

                    }
                    else
                    {
                        player.TeleportTo(new Position
                        {
                            X = (short)x,
                            Y = (short)y,
                            Z = (short)z,
                            R = (byte)rot,
                            L = (byte)lot
                        });
                    }
                }
                else
                {
                    CdTeleportP.PrintUsage(player);
                }
            }
        }

        #endregion
        #region JoinOnRankWorld
        static readonly CommandDescriptor CdJORW = new CommandDescriptor
        {
            Name = "JoinOnRankWorld",
            Category = CommandCategory.New,
            Aliases = new[] { "jorw", "joinonrank", "jor"},
            Usage = "/JoinOnRank On/Off ",
            Help = "Determines if you spawn on your designated rank world or not.",
            Handler = SORWHandler
        };

        static void SORWHandler(Player player, CommandReader cmd)
        {
            if (!WorldManager.Worlds.Contains(player.Info.Rank.MainWorld))
            {
                player.Message("Sorry there is no main world for your rank at the moment.");
                return;                    
            }
            if (cmd.HasNext)
            {
                string state = cmd.Next();
                if (state.ToLower() == "on" || state.ToLower() == "yes")
                {
                    player.Info.JoinOnRankWorld = true;
                    player.Message("JoinOnRankWorld: &2On");
                    player.Message("You will now spawn on world {0}&s when you log onto the server.", player.Info.Rank.MainWorld.ClassyName);
                    return;
                }
                if (state.ToLower() == "off" || state.ToLower() == "no")
                {
                    player.Info.JoinOnRankWorld = false;
                    player.Message("JoinOnRankWorld: &4Off");
                    player.Message("You will now spawn on world {0}&s when you log onto the server.", WorldManager.MainWorld.ClassyName);
                    return;
                }
                if (state.ToLower() == "state" || state.ToLower() == "what" || state.ToLower() == "current")
                {
                    if (player.Info.JoinOnRankWorld == false)
                    {
                        player.Message("JoinOnRankWorld: &4Off");
                    }
                    if (player.Info.JoinOnRankWorld == true)
                    {
                        player.Message("JoinOnRankWorld: &2On");
                    }
                    return;
                }
                else
                {
                    CdJORW.PrintUsage(player);
                    return;
                }
            }
            if (player.Info.JoinOnRankWorld == true)
            {
                player.Info.JoinOnRankWorld = false;
                player.Message("JoinOnRankWorld: &4Off");
                player.Message("You will now spawn on world {0}&s when you log onto the server.", WorldManager.MainWorld.ClassyName);
                return;
            }
            else
            {
                player.Info.JoinOnRankWorld = true;
                player.Message("JoinOnRankWorld: &2On");
                player.Message("You will now spawn on world {0}&s when you log onto the server.", player.Info.Rank.MainWorld.ClassyName);
                return;
            }
        }
        #endregion
        #region MaxCaps

        static readonly CommandDescriptor CdMaxCaps = new CommandDescriptor
        {
            Name = "MaxCaps",
            Aliases = new[] { "caps" },
            Permissions = new[] { Permission.Chat },
            Category = CommandCategory.New,
            Help = "Changes/Displays the max amount of uppercase letters a rank can use in a message.",
            Usage = "/MaxCaps <Rank> <Amount>",
            Handler = MaxCapsHandler
        };

        static void MaxCapsHandler(Player player, CommandReader cmd)
        {
            string rname = cmd.Next();
            string rmax = cmd.Next();
            Rank prank = player.Info.Rank;
            if (player.Info.Rank.Can(Permission.ShutdownServer))
            {
                if (rname == null)
                {
                    if (prank.MaxCaps != 0 && prank.MaxCaps != 1)
                    {
                        player.Message("Rank ({0}&s) has a max of {1} uppercase letters/message.", prank.ClassyName, prank.MaxCaps);
                        return;
                    }
                    else if (prank.MaxCaps == 0)
                    {
                        player.Message("Rank ({0}&s) has no max.", prank.ClassyName);
                        return;
                    }
                    else if (prank.MaxCaps == 1)
                    {
                        player.Message("Rank ({0}&s) has a max of (RawMessage.Length / 2) uppercase letters/message.", prank.ClassyName);
                        return;
                    }
                }
                Rank rank = RankManager.FindRank(rname);
                if (rank == null)
                {
                    player.MessageNoRank(rname);
                    return;
                }
                if (rmax == null)
                {
                    if (rank.MaxCaps != 0 && rank.MaxCaps != 1)
                    {
                        player.Message("Rank ({0}&s) has a max of {1} uppercase letters/message.", rank.ClassyName, rank.MaxCaps);
                        return;
                    }
                    else if (rank.MaxCaps == 0)
                    {
                        player.Message("Rank ({0}&s) has no max.", rank.ClassyName);
                        return;
                    }
                    else if (rank.MaxCaps == 1)
                    {
                        player.Message("Rank ({0}&s) has a max of (RawMessage.Length / 2) uppercase letters/message.", rank.ClassyName);
                        return;
                    }
                }
                int mcaps;
                if (!int.TryParse(rmax, out mcaps))
                {
                    player.Message(CdMaxCaps.Usage);
                    return;
                }
                if (rank != null && mcaps != null)
                {
                    rank.MaxCaps = mcaps;
                    player.Message("Set MaxCaps for rank ({0}&s) to {1} uppercase letters/message.", rank.ClassyName, rank.MaxCaps);
                    return;
                }
            }
            else
            {
                if (prank.MaxCaps != 0 && prank.MaxCaps != 1)
                {
                    player.Message("Rank ({0}&s) has a max of {1} uppercase letters/message.", prank.ClassyName, prank.MaxCaps);
                    return;
                }
                else if (prank.MaxCaps == 0)
                {
                    player.Message("Rank ({0}&s) has no max.", prank.ClassyName);
                    return;
                }
                else if (prank.MaxCaps == 1)
                {
                    player.Message("Rank ({0}&s) has a max of (RawMessage.Length / 2) uppercase letters/message.", prank.ClassyName);
                    return;
                }
                return;
            }
        }

        #endregion
        #region Bring / WorldBring / BringAll

        static readonly CommandDescriptor CdBring = new CommandDescriptor {
            Name = "Bring",
            IsConsoleSafe = true,
            Aliases = new[] { "summon", "fetch" },
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Bring },
            Usage = "/Bring PlayerName [ToPlayer]",
            Help = "Teleports another player to your location. " +
                   "If the optional second parameter is given, teleports player to another player.",
            Handler = BringHandler
        };

        static void BringHandler( Player player, CommandReader cmd ) {
            string name = cmd.Next();
            if( name == null ) {
                CdBring.PrintUsage( player );
                return;
            }

            // bringing someone to another player (instead of to self)
            string toName = cmd.Next();
            Player toPlayer = player;
            if( toName != null ) {
                toPlayer = Server.FindPlayerOrPrintMatches(player, toName, SearchOptions.IncludeSelf);
                if( toPlayer == null ) return;
            } else if( toPlayer.World == null ) {
                player.Message( "When used from console, /Bring requires both names to be given." );
                return;
            }

            World world = toPlayer.World;
            if( world == null ) PlayerOpException.ThrowNoWorld( toPlayer );

            Player target = Server.FindPlayerOrPrintMatches(player, name, SearchOptions.Default);
            if( target == null ) return;

            if( !player.Can( Permission.Bring, target.Info.Rank ) ) {
                player.Message( "You may only bring players ranked {0}&S or lower.",
                                player.Info.Rank.GetLimit( Permission.Bring ).ClassyName );
                player.Message( "{0}&S is ranked {1}",
                                target.ClassyName, target.Info.Rank.ClassyName );
                return;
            }

            if( target.World == world ) {
                // teleport within the same world
                target.TeleportTo( toPlayer.Position );

            } else {
                // teleport to a different world
                SecurityCheckResult check = world.AccessSecurity.CheckDetailed( target.Info );
                if( check == SecurityCheckResult.RankTooLow ) {
                    if( player.CanJoin( world ) ) {
                        if( cmd.IsConfirmed ) {
                            BringPlayerToWorld( player, target, world, true, true );
                        } else {
                            //Allow banned players to be moved about...
                            if (target.Info.Rank.Name == "Banned")
                            {
                                player.Message("&EYou CAN move banned players about... It is considered bad form though...");
                            }
                            Logger.Log( LogType.UserActivity,
                                        "Bring: Asked {0} to confirm overriding world permissions to bring player {1} to world {2}",
                                        player.Name, target.Name, world.Name );
                            player.Confirm( cmd,
                                            "{0} {1}&S is ranked too low to join {2}&S. Override world permissions?",
                                            target.Info.Rank.ClassyName,
                                            target.ClassyName,
                                            world.ClassyName );
                        }
                    } else {
                        player.Message( "Neither you nor {0}&S are allowed to join world {1}",
                                        target.ClassyName, world.ClassyName );
                    }
                } else {
                    BringPlayerToWorld( player, target, world, false, true );
                }
            }
        }


        static readonly CommandDescriptor CdWorldBring = new CommandDescriptor {
            Name = "WBring",
            IsConsoleSafe = true,
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Bring },
            Usage = "/WBring PlayerName WorldName",
            Help = "Teleports a player to the given world's spawn.",
            Handler = WorldBringHandler
        };

        static void WorldBringHandler( Player player, CommandReader cmd ) {
            string playerName = cmd.Next();
            string worldName = cmd.Next();
            if( playerName == null || worldName == null ) {
                CdWorldBring.PrintUsage( player );
                return;
            }

            Player target = Server.FindPlayerOrPrintMatches(player, playerName, SearchOptions.Default);
            World world = WorldManager.FindWorldOrPrintMatches( player, worldName );

            if( target == null || world == null ) return;

            if (target == player)
            {
                player.Message( "&WYou cannot &H/WBring&W yourself." );
                return;
            }

            if( !player.Can( Permission.Bring, target.Info.Rank ) ) {
                player.Message( "You may only bring players ranked {0}&S or lower.",
                                player.Info.Rank.GetLimit( Permission.Bring ).ClassyName );
                player.Message( "{0}&S is ranked {1}",
                                target.ClassyName, target.Info.Rank.ClassyName );
                return;
            }

            if( world == target.World ) {
                player.Message( "{0}&S is already in world {1}&S. They were brought to spawn.",
                                target.ClassyName, world.ClassyName );
                target.TeleportTo( target.WorldMap.Spawn );
                return;
            }

            SecurityCheckResult check = world.AccessSecurity.CheckDetailed( target.Info );
            if( check == SecurityCheckResult.RankTooLow ) {
                if( player.CanJoin( world ) ) {
                    if( cmd.IsConfirmed ) {
                        BringPlayerToWorld( player, target, world, true, false );
                    } else {
                        //Allow banned players to be moved about...
                        if (target.Info.Rank.Name == "Banned")
                        {
                            player.Message("&EYou CAN move banned players about... It is considered bad form though...");
                        }
                        Logger.Log( LogType.UserActivity,
                                    "WBring: Asked {0} to confirm overriding world permissions to bring player {1} to world {2}",
                                    player.Name, target.Name, world.Name );
                        player.Confirm( cmd,
                                        "{0} {1}&S is ranked too low to join {2}&S. Override world permissions?",
                                        target.Info.Rank.ClassyName,
                                        target.ClassyName,
                                        world.ClassyName );
                    }
                } else {
                    player.Message( "Neither you nor {0}&S are allowed to join world {1}",
                                    target.ClassyName, world.ClassyName );
                }
            } else {
                //Allow banned players to be moved about...
                if (target.Info.Rank.Name == "Banned")
                {
                    player.Message("&EYou CAN move banned players about... It is considered bad form though...");
                }
                BringPlayerToWorld( player, target, world, false, false );
            }
        }


        static readonly CommandDescriptor CdBringAll = new CommandDescriptor {
            Name = "BringAll",
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Bring, Permission.BringAll },
            Usage = "/BringAll [@Rank [@AnotherRank]] [*|World [AnotherWorld]]",
            Help = "Teleports all players from your world to you. " +
                   "If any world names are given, only teleports players from those worlds. " +
                   "If any rank names are given, only teleports players of those ranks.",
            Handler = BringAllHandler
        };

        static void BringAllHandler( Player player, CommandReader cmd ) {
            if( player.World == null ) PlayerOpException.ThrowNoWorld( player );

            List<World> targetWorlds = new List<World>();
            List<Rank> targetRanks = new List<Rank>();
            bool allWorlds = false;
            bool allRanks = true;

            // Parse the list of worlds and ranks
            string arg;
            while( (arg = cmd.Next()) != null ) {
                if( arg.StartsWith( "@" ) ) {
                    Rank rank = RankManager.FindRank( arg.Substring( 1 ) );
                    if( rank == null ) {
                        player.MessageNoRank( arg.Substring( 1 ) );
                        return;
                    } else {
                        if( player.Can( Permission.Bring, rank ) ) {
                            targetRanks.Add( rank );
                        } else {
                            player.Message( "&WYou are not allowed to bring players of rank {0}",
                                            rank.ClassyName );
                        }
                        allRanks = false;
                    }
                } else if( arg == "*" ) {
                    allWorlds = true;
                } else {
                    World world = WorldManager.FindWorldOrPrintMatches( player, arg );
                    if( world == null ) return;
                    targetWorlds.Add( world );
                }
            }

            // If no worlds were specified, use player's current world
            if( !allWorlds && targetWorlds.Count == 0 ) {
                targetWorlds.Add( player.World );
            }

            // Apply all the rank and world options
            HashSet<Player> targetPlayers;
            if( allRanks && allWorlds ) {
                targetPlayers = new HashSet<Player>( Server.Players );
            } else if( allWorlds ) {
                targetPlayers = new HashSet<Player>();
                foreach( Rank rank in targetRanks ) {
                    foreach( Player rankPlayer in Server.Players.Ranked( rank ) ) {
                        targetPlayers.Add( rankPlayer );
                    }
                }
            } else if( allRanks ) {
                targetPlayers = new HashSet<Player>();
                foreach( World world in targetWorlds ) {
                    foreach( Player worldPlayer in world.Players ) {
                        targetPlayers.Add( worldPlayer );
                    }
                }
            } else {
                targetPlayers = new HashSet<Player>();
                foreach( Rank rank in targetRanks ) {
                    foreach( World world in targetWorlds ) {
                        foreach( Player rankWorldPlayer in world.Players.Ranked( rank ) ) {
                            targetPlayers.Add( rankWorldPlayer );
                        }
                    }
                }
            }

            Rank bringLimit = player.Info.Rank.GetLimit( Permission.Bring );

            // Remove the player him/herself
            targetPlayers.Remove( player );

            int count = 0;


            // Actually bring all the players
            foreach( Player targetPlayer in targetPlayers.CanBeSeen( player )
                                                         .RankedAtMost( bringLimit ) ) {
                if( targetPlayer.World == player.World ) {
                    // teleport within the same world
                    targetPlayer.TeleportTo( player.Position );
                    targetPlayer.Position = player.Position;
                    if( targetPlayer.Info.IsFrozen ) {
                        targetPlayer.Position = player.Position;
                    }

                } else {
                    // teleport to a different world
                    BringPlayerToWorld( player, targetPlayer, player.World, false, true );
                }
                count++;
            }

            // Check if there's anyone to bring
            if( count == 0 ) {
                player.Message( "No players to bring!" );
            } else {
                player.Message( "Bringing {0} players...", count );
            }
        }



        static void BringPlayerToWorld( [NotNull] Player player, [NotNull] Player target, [NotNull] World world,
                                        bool overridePermissions, bool usePlayerPosition ) {
            if( player == null ) throw new ArgumentNullException( "player" );
            if( target == null ) throw new ArgumentNullException( "target" );
            if( world == null ) throw new ArgumentNullException( "world" );
            switch( world.AccessSecurity.CheckDetailed( target.Info ) ) {
                case SecurityCheckResult.Allowed:
                case SecurityCheckResult.WhiteListed:
                    //Allow banned players to be moved about...
                    if( world.IsFull ) {
                        player.Message( "Cannot bring {0}&S because world {1}&S is full.",
                                        target.ClassyName,
                                        world.ClassyName );
                        return;
                    }
                    target.StopSpectating();
                    if( usePlayerPosition ) {
                        target.JoinWorld( world, WorldChangeReason.Bring, player.Position );
                    } else {
                        target.JoinWorld( world, WorldChangeReason.Bring );
                    }
                    break;

                case SecurityCheckResult.BlackListed:
                    player.Message( "Cannot bring {0}&S because he/she is blacklisted on world {1}",
                                    target.ClassyName,
                                    world.ClassyName );
                    break;

                case SecurityCheckResult.RankTooLow:
                    if( overridePermissions ) {
                        //Allow banned players to be moved about...
                        if (target.Info.Rank.Name == "Banned")
                        {
                            player.Message("&EYou CAN move banned players about... It is considered bad form though...");
                        }
                        target.StopSpectating();
                        if( usePlayerPosition ) {
                            target.JoinWorld( world, WorldChangeReason.Bring, player.Position );
                        } else {
                            target.JoinWorld( world, WorldChangeReason.Bring );
                        }
                    } else {
                        player.Message( "Cannot bring {0}&S because world {1}&S requires {2}+&S to join.",
                                        target.ClassyName,
                                        world.ClassyName,
                                        world.AccessSecurity.MinRank.ClassyName );
                    }
                    break;
            }
        }

        #endregion
        #region Patrol & SpecPatrol

        static readonly CommandDescriptor CdPatrol = new CommandDescriptor {
            Name = "Patrol",
            Aliases = new[] { "pat" },
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Patrol },
            Help = "Teleports you to the next player in need of checking.",
            Handler = PatrolHandler
        };

        static void PatrolHandler( Player player, CommandReader cmd ) {
            World playerWorld = player.World;
            if( playerWorld == null ) PlayerOpException.ThrowNoWorld( player );

            Player target = playerWorld.GetNextPatrolTarget( player );
            if( target == null ) {
                player.Message( "Patrol: No one to patrol in this world." );
                return;
            }

            player.TeleportTo( target.Position );
            player.Message( "Patrol: Teleporting to {0}", target.ClassyName );
        }


        static readonly CommandDescriptor CdSpecPatrol = new CommandDescriptor {
            Name = "SpecPatrol",
            Aliases = new[] { "spat" },
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Patrol, Permission.Spectate },
            Help = "Teleports you to the next player in need of checking.",
            Handler = SpecPatrolHandler
        };

        static void SpecPatrolHandler( Player player, CommandReader cmd ) {
            World playerWorld = player.World;
            if( playerWorld == null ) PlayerOpException.ThrowNoWorld( player );

            Player target = playerWorld.GetNextPatrolTarget( player,
                                                             p => player.Can( Permission.Spectate, p.Info.Rank ),
                                                             true );
            if( target == null ) {
                player.Message( "Patrol: No one to spec-patrol in this world." );
                return;
            }

            target.LastPatrolTime = DateTime.UtcNow;
            player.Spectate( target );
        }

        #endregion
        #region Mute / Unmute

        static readonly TimeSpan MaxMuteDuration = TimeSpan.FromDays(700); // 100w0d

        static readonly CommandDescriptor CdMute = new CommandDescriptor
        {
            Name = "Mute",
            Category = CommandCategory.Moderation | CommandCategory.Chat,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Mute },
            Help = "Mutes a player for a specified length of time.",
            Usage = "/Mute PlayerName Duration",
            Handler = MuteHandler
        };

        static void MuteHandler(Player player, CommandReader cmd)
        {
            string targetName = cmd.Next();
            string timeString = cmd.Next();
            TimeSpan duration;
           
            // validate command parameters
            if (targetName == null || timeString == null ||
                !timeString.TryParseMiniTimeSpan(out duration) || duration <= TimeSpan.Zero)
            {
                CdMute.PrintUsage(player);
                return;
            }

            // check if given time exceeds maximum (700 days)
            if (duration > MaxMuteDuration)
            {
                player.Message("Maximum mute duration is {0}.", MaxMuteDuration.ToMiniString());
                duration = MaxMuteDuration;
            }

            // find the target
            PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                       targetName,
                                                                       SearchOptions.ReturnSelfIfOnlyMatch);
            if (target == null) return;
            if (target == player.Info)
            {
                player.Message("You cannot &H/Mute&S yourself.");
                return;
            }

            // actually mute
            try
            {
                target.Mute(player, duration, true, true);
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
            }
        }


        static readonly CommandDescriptor CdUnmute = new CommandDescriptor
        {
            Name = "Unmute",
            Category = CommandCategory.Moderation | CommandCategory.Chat,
            IsConsoleSafe = true,
            Permissions = new[] { Permission.Mute },
            Help = "Unmutes a player.",
            Usage = "/Unmute PlayerName",
            Handler = UnmuteHandler
        };

        static void UnmuteHandler(Player player, CommandReader cmd)
        {
            string targetName = cmd.Next();
            if (String.IsNullOrEmpty(targetName))
            {
                CdUnmute.PrintUsage(player);
                return;
            }

            // find target
            PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player,
                                                                       targetName,
                                                                       SearchOptions.ReturnSelfIfOnlyMatch);
            if (target == null) return;
            if (target == player.Info)
            {
                player.Message("You cannot &H/Unmute&S yourself.");
                return;
            }

            try
            {
                target.Unmute(player, true, true);
            }
            catch (PlayerOpException ex)
            {
                player.Message(ex.MessageColored);
            }
        }

        #endregion
        #region Spectate / Unspectate

        static readonly CommandDescriptor CdSpectate = new CommandDescriptor {
            Name = "Spectate",
            Aliases = new[] { "follow", "spec" },
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Spectate },
            Usage = "/Spectate PlayerName",
            Handler = SpectateHandler
        };

        static void SpectateHandler( Player player, CommandReader cmd ) {
            string targetName = cmd.Next();
            if( targetName == null ) {
                PlayerInfo lastSpec = player.LastSpectatedPlayer;
                if( lastSpec != null ) {
                    Player spec = player.SpectatedPlayer;
                    if( spec != null ) {
                        player.Message( "Now spectating {0}", spec.ClassyName );
                    } else {
                        player.Message( "Last spectated {0}", lastSpec.ClassyName );
                    }
                } else {
                    CdSpectate.PrintUsage( player );
                }
                return;
            }

            Player target = Server.FindPlayerOrPrintMatches(player, targetName, SearchOptions.Default);
            if( target == null ) return;

            if( target == player ) {
                player.Message( "You cannot spectate yourself." );
                return;
            }

            if( !player.Can( Permission.Spectate, target.Info.Rank ) ) {
                player.Message( "You may only spectate players ranked {0}&S or lower.",
                player.Info.Rank.GetLimit( Permission.Spectate ).ClassyName );
                player.Message( "{0}&S is ranked {1}",
                                target.ClassyName, target.Info.Rank.ClassyName );
                return;
            }

            if( !player.Spectate( target ) ) {
                player.Message( "Already spectating {0}", target.ClassyName );
            }
        }


        static readonly CommandDescriptor CdUnspectate = new CommandDescriptor {
            Name = "Unspectate",
            Aliases = new[] { "unfollow", "unspec" },
            Category = CommandCategory.Moderation,
            Permissions = new[] { Permission.Spectate },
            NotRepeatable = true,
            Handler = UnspectateHandler
        };

        static void UnspectateHandler( Player player, CommandReader cmd ) {
            if( !player.StopSpectating() ) {
                player.Message( "You are not currently spectating anyone." );
            }
        }

        #endregion
        #region HackControl
        static readonly CommandDescriptor CdHackControl = new CommandDescriptor
        {
            Name = "HackControl",
            Aliases = new[] { "hacks", "hax" },
            Category = CommandCategory.New,
            Permissions = new[] { Permission.Chat},
            Usage = "/Hacks [Player] [Hack] [jumpheight(if needed)]",
            IsConsoleSafe = true,
            Help = "Change the hacking abilities of [Player]\n" +
            "Valid hacks: &aFlying&e, &aNoclip&e, &aSpeedhack&e, &aRespawn&e, &aThirdPerson&e and &aJumpheight",
            Handler = HackControlHandler
        };

        static void HackControlHandler(Player player, CommandReader cmd)
        {
            string first = cmd.Next();
            if (first == null || player.Info.Rank != RankManager.HighestRank) {
                player.Message("&eCurrent Hacks for {0}", player.ClassyName);
                player.Message("    &eFlying: &a{0} &eNoclip: &a{1} &eSpeedhack: &a{2}",
                                player.Info.AllowFlying.ToString(),
                                player.Info.AllowNoClip.ToString(),
                                player.Info.AllowSpeedhack.ToString());
                player.Message("    &eRespawn: &a{0} &eThirdPerson: &a{1} &eJumpHeight: &a{2}",
                                player.Info.AllowRespawn.ToString(),
                                player.Info.AllowThirdPerson.ToString(),
                                player.Info.JumpHeight); return;
            }
            PlayerInfo target = PlayerDB.FindPlayerInfoOrPrintMatches(player, first, SearchOptions.IncludeSelf);
            if (target == null) { return; }
            string hack = cmd.Next();
            if (hack == null) {
                player.Message("&eCurrent Hacks for {0}", target.ClassyName);
                player.Message("    &eFlying: &a{0} &eNoclip: &a{1} &eSpeedhack: &a{2}",
                                player.Info.AllowFlying.ToString(),
                                player.Info.AllowNoClip.ToString(),
                                player.Info.AllowSpeedhack.ToString());
                player.Message("    &eRespawn: &a{0} &eThirdPerson: &a{1} &eJumpHeight: &a{2}",
                                player.Info.AllowRespawn.ToString(),
                                player.Info.AllowThirdPerson.ToString(),
                                player.Info.JumpHeight); return;
            } else { hack = hack.ToLower(); }
            if (hack.Equals("flying") || hack.Equals("fly") || hack.Equals("f"))
            {
                player.Message("Hacks for {0}", target.ClassyName);
                player.Message("    Flying: &a{0} &e--> &a{1}", target.AllowFlying.ToString(), (!target.AllowFlying).ToString());
                target.AllowFlying = !target.AllowFlying;
                if (target.IsOnline)
                {
                    if (target.PlayerObject.SupportsHackControl)
                    {
                        target.PlayerObject.Send(Packet.HackControl(
                            target.AllowFlying, target.AllowNoClip, target.AllowSpeedhack,
                            target.AllowRespawn, target.AllowThirdPerson, target.JumpHeight));
                    }
                }
            } 
            else if (hack.Equals("noclip") || hack.Equals("clip") || hack.Equals("nc")) 
            {
                player.Message("Hacks for {0}", target.ClassyName);
                player.Message("    NoClip: &a{0} &e--> &a{1}", target.AllowNoClip.ToString(), (!target.AllowNoClip).ToString());
                target.AllowNoClip = !target.AllowNoClip;
                if (target.IsOnline)
                {
                    if (target.PlayerObject.SupportsHackControl)
                    {
                        target.PlayerObject.Send(Packet.HackControl(
                            target.AllowFlying, target.AllowNoClip, target.AllowSpeedhack,
                            target.AllowRespawn, target.AllowThirdPerson, target.JumpHeight));
                    }
                }
            }
            else if (hack.Equals("speedhack") || hack.Equals("speed") || hack.Equals("sh"))
            {
                player.Message("Hacks for {0}", target.ClassyName);
                player.Message("    SpeedHack: &a{0} &e--> &a{1}", target.AllowSpeedhack.ToString(), (!target.AllowSpeedhack).ToString());
                target.AllowSpeedhack = !target.AllowSpeedhack;
                if (target.IsOnline)
                {
                    if (target.PlayerObject.SupportsHackControl)
                    {
                        target.PlayerObject.Send(Packet.HackControl(
                            target.AllowFlying, target.AllowNoClip, target.AllowSpeedhack,
                            target.AllowRespawn, target.AllowThirdPerson, target.JumpHeight));
                    }
                }
            }
            else if (hack.Equals("respawn") || hack.Equals("spawn") || hack.Equals("rs"))
            {
                player.Message("Hacks for {0}", target.ClassyName);
                player.Message("    Respawn: &a{0} &e--> &a{1}", target.AllowRespawn.ToString(), (!target.AllowRespawn).ToString());
                target.AllowRespawn = !target.AllowRespawn;
                if (target.IsOnline)
                {
                    if (target.PlayerObject.SupportsHackControl)
                    {
                        target.PlayerObject.Send(Packet.HackControl(
                            target.AllowFlying, target.AllowNoClip, target.AllowSpeedhack,
                            target.AllowRespawn, target.AllowThirdPerson, target.JumpHeight));
                    }
                }
            }
            else if (hack.Equals("thirdperson") || hack.Equals("third") || hack.Equals("tp"))
            {
                player.Message("Hacks for {0}", target.ClassyName);
                player.Message("    ThirdPerson: &a{0} &e--> &a{1}", target.AllowThirdPerson.ToString(), (!target.AllowThirdPerson).ToString());
                target.AllowThirdPerson = !target.AllowThirdPerson;
                if (target.IsOnline)
                {
                    if (target.PlayerObject.SupportsHackControl)
                    {
                        target.PlayerObject.Send(Packet.HackControl(
                            target.AllowFlying, target.AllowNoClip, target.AllowSpeedhack,
                            target.AllowRespawn, target.AllowThirdPerson, target.JumpHeight));
                    }
                }
            }
            else if (hack.Equals("jumpheight") || hack.Equals("jump") || hack.Equals("height") || hack.Equals("jh"))
            {
                short height;
                string third = cmd.Next();
                if (short.TryParse(third, out height))
                {
                    player.Message("Hacks for {0}", target.ClassyName);
                    player.Message("    JumpHeight: &a{0} &e--> &a{1}", target.JumpHeight, height);
                    target.JumpHeight = height;
                    if (target.IsOnline)
                    {
                        if (target.PlayerObject.SupportsHackControl)
                        {
                            target.PlayerObject.Send(Packet.HackControl(
                                target.AllowFlying, target.AllowNoClip, target.AllowSpeedhack,
                                target.AllowRespawn, target.AllowThirdPerson, target.JumpHeight));
                        }
                    }
                }
                else
                {
                    player.Message("Error: Could not parse \"&a{0}&e\" as a short. Try something between &a0&e and &a32767", third); 
                }
            }
            else { player.Message(CdHackControl.Usage); }
        }
        #endregion
        #region tempfreeze

        // freeze target if player is allowed to do so
        static void FreezeIfAllowed( Player player, PlayerInfo targetInfo ) {
            if( targetInfo.IsOnline && !targetInfo.IsFrozen && player.Can( Permission.Freeze, targetInfo.Rank ) ) {
                try {
                    targetInfo.Freeze( player, true, true );
                    player.Message( "{0}&S has been frozen while you retry.", targetInfo.ClassyName );
                } catch( PlayerOpException ) { }
            }
        }


        // warn player if others are still online from target's IP
        static void WarnIfOtherPlayersOnIP( Player player, PlayerInfo targetInfo, Player except ) {
            Player[] otherPlayers = Server.Players.FromIP( targetInfo.LastIP )
                                                  .Except( except )
                                                  .ToArray();
            if( otherPlayers.Length > 0 ) {
                player.Message( "&WWarning: Other player(s) share IP with {0}&W: {1}",
                                targetInfo.ClassyName,
                                otherPlayers.JoinToClassyString() );
            }
        }
        #endregion
    }
}