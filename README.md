# Ossyria

A C# project based on the original source written by Odin. The project files are truncated as I'm only displaying a snippet of the source. If you are a recuriter and are interested in looking at the full source, please message me privately. This was a solo project done completely by me to help me learn the C# language and improve my skills in the areas of Multi-threading and Socket programming. I felt that the original java sources wasn't coded as best as it could, so I took the opportunity to recreate my own version of it, but in a different programming language. The goals of this project was:

- Functionality
- Performance
- Higher Uptimes
- Stability

# Features

- 90% Working Skills (Actives, Passives, Buffs)
- Auth Server, Channel Server, World Server
- Maps, Portals, Monsters, NPCs, Movement
- Items, Skills, Chat, Scrolling, Item Effects
- User and Admin commands
- NPC Scripting
- Semi Cash Shop
- Multiplayer
- Party System
- Wz Parsing and Loading
- Custom Database (The port to MySQL or SQL Server will happen in the near future)

# Structure

The Auth, Channel, and World servers each have a reference to the OssyriaDEV which is the base and heart of the servers. The servers each have their own handlers to process client requests. I took this approach because it reduced redundancy. It gave each server a link to the OssyriaDEV rather than having each server have the same files. It would have been very messy in terms of updating and overall development. Thus, each server has a .DLL reference to OssyriaDEV. 

The server also loads all necessary .WZ data during the startup of the server. I took this approach because it would avoid the need to load them later on invidually by each user. The database is custom as working with MySQL caused a lot of problems such as implementing tables and having correct the correct query strings for requests. It was annoying and slowed down development. However, I do plan on porting my custom database to MySQL or SQL Server.  


# Credits
- Mackenzie Huynh (Project Owner)
- MapleLib by Snow (https://github.com/haha01haha01/MapleLib)
