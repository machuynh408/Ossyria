using OssyriaDEV;
using System;
using System.Collections.Generic;

namespace Channel
{
    public class _0x2E : IHandler
    {
        public void Invoke(User u, Reader r)
        {
            Player p = u.getPlayer();
            Map m = p.getMap();

            Action action = new Action(() => { handle(p, r); });
            m.processAction(action);
        }

        private void handle(Player p, Reader r)
        {
            string message = r.readMapleAsciiString();
            string[] s = null;
            if (message.StartsWith("!"))
            {
                switch (message)
                {
                    case "!help":
                        {
                            string heading = "Welcome to the Admin panel, " + p.getName() + "!";
                            p.send(Packets.print(heading));
                            p.send(Packets.notice("-------------------------------------------------------------"));
                            p.send(Packets.notice("                         Ossyria v1.0                        "));
                            p.send(Packets.notice("-------------------------------------------------------------"));
                            p.send(Packets.print("!str    - (value) - Sets the player's strength"));
                            p.send(Packets.notice("!dex    - (value) - Sets the player's dexterity"));
                            p.send(Packets.print("!int    - (value) - Sets the player's intelligence"));
                            p.send(Packets.notice("!luk    - (value) - Sets the player's luk"));
                            p.send(Packets.print("!level  - (value) - Sets the player's level"));
                            p.send(Packets.notice("!class  - (value) - Sets the player's class"));
                            p.send(Packets.print("!ap     - (amount) - Sets the player's ap"));
                            p.send(Packets.notice("!sp     - (amount) - Sets the player's sp"));
                            p.send(Packets.print("!item   - (itemId)"));
                            p.send(Packets.notice("!map    - (mapId) (spawnPoint, 0 by default)"));
                            p.send(Packets.print("!spawn  - (monsterId) (amount | max = 50)"));
                            p.send(Packets.notice("!info   - (player | mob | npc | skill) (name or id)"));
                            p.send(Packets.print("!search - (name) - A very fast search that searches in all directories"));
                            p.send(Packets.notice("!search - (directory) (name) - A very precise search that searches in the specified directory"));

                            int count = 0;
                            List<string> data = new List<string>();
                            string dir = "\t\t\t  [-] ";
                            Library.getStringWzNames().ForEach(x =>
                            {
                                dir += x + " | ";
                                count++;
                                if (count == 5)
                                {
                                    data.Add(dir);
                                    dir = "\t\t\t  [-] ";
                                    count = 0;
                                }
                            });
                            data.Add(dir);
                            data.ForEach(y => { p.send(Packets.print(y)); });


                            p.send(Packets.notice("!tp - (mapName) - Teleports you to common used maps."));


                            count = 0;
                            data.Clear();
                            dir = "\t\t\t  [-] ";
                            Tools.getMaps().ForEach(x =>
                            {
                                dir += x + " | ";
                                count++;
                                if (count == 4)
                                {
                                    data.Add(dir);
                                    dir = "\t\t\t  [-] ";
                                    count = 0;
                                }
                            });
                            data.Add(dir);
                            data.ForEach(y => { p.send(Packets.print(y)); });
                            p.send(Packets.notice("!warp - (playerName) - Teleports you to the specified player."));
                            p.send(Packets.print("!pos - Displays the current position you are in."));
                            p.send(Packets.notice("!meso - Gives you the specified amount of mesos."));
                            p.send(Packets.print("!dmp - Dumps you the information on the current map."));
                            p.send(Packets.notice("!wipe - Clears all the monsters in the map."));
                            p.send(Packets.print("!rate (e = exp, l = loot, m = meso) (value)- Sets the specified rate for the server."));
                            p.send(Packets.notice("!dispose - Hopefully lets you perform actions."));
                            p.send(Packets.print("-------------------------------------------------------------"));
                            break;
                        }
                    case "!pos":
                        {
                            p.send(Packets.print("x: " + p.getPosition().getInt("x") + " y: " + p.getPosition().getInt("y") + " fh: " + p.getPosition().getFh()));
                            break;
                        }
                    case "!control":
                        {
                            List<Spawn> controlled = new List<Spawn>();
                            p.getSpawns().ForEach(x =>
                            {
                                if (x is Monster)
                                {
                                    Monster monster = x as Monster;
                                    if (monster.getMaster() == p)
                                        controlled.Add(monster);
                                }
                            });
                            p.send(Packets.print("Count: " + controlled.Count));
                            controlled.ForEach(y => { p.send(Packets.print("Key: " + y.getKey())); });
                            break;
                        }
                    case "!dmp":
                        {
                            p.send(Packets.notice("[Map = " + p.getMap().getId() + "] " + "(" + Library.getString("map", p.getMap().getId()) + ")"));
                            p.send(Packets.print("      [Players = " + p.getMap().getPlayers().Count + "] [Monsters = " + p.getMap().getMonsters().Count + "] [Npcs = " + p.getMap().getNpcs().Count + "]"));


                            p.send(Packets.notice("[Players]"));
                            foreach (Player x in p.getMap().getPlayers())
                                p.send(Packets.print("      [Id = " + x.getId() + "] [Name = " + x.getName() + "] [Hp = " + "(" + x.getHp() + "/" + x.getMaxHP() + ")" + "]"));

                            List<int> monsters = new List<int>();
                            foreach (Monster m in p.getMap().getMonsters())
                                if (!monsters.Contains(m.getId()))
