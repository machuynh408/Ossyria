using System;
using System.Collections.Generic;

namespace OssyriaDEV
{
    public class Factory
    {
        public static Item createItem(int id)
        {
            return createItem(id, 0, 1);
        }
        public static Item createItem(int id, short quantity)
        {
            return createItem(id, 0, quantity);
        }
        public static Item createItem(int id, byte position)
        {
            return createItem(id, position, 1);
        }
        public static Item createItem(int id, byte position, short quantity, bool randomize = false)
        {
            Item i = new Item(id, position, quantity);
            if (id >= 0 && id < 2000000)
            {
                CharacterWz c = Library.getEquip(id);
                if (c == null)
                    return null;
                i.insert("type", "inventory\\equip");

                if (id == 1932000) // This is the mount is for Battleship, but it's not an actual item
                    return null;

                if (c.contains("reqLevel"))
                {
                    int value = c.getInt("reqLevel");
                    i.insert("reqLevel", value);
                }

                if (id == 1902000 || id == 1902001 || id == 1902002 || id == 1902008 || id == 1902009)
                {
                    i.insert("mount", 1);
                    i.insert("mountLevel", 1);
                    i.insert("mountExp", 0);
                    return i;
                }
                else if(id == 1912000) // Saddle
                {
                    return i;
                }
                if (c.contains("incSTR"))
                {
                    int incSTR = c.getIncSTR();
                    int value = Tools.getInt(incSTR - 2, incSTR + 3);
                    i.insert("incSTR", randomize == true ? value : incSTR);
                }
                if (c.contains("incDEX"))
                {
                    int incDEX = c.getIncDEX();
                    int value = Tools.getInt(incDEX - 2, incDEX + 3);
                    i.insert("incDEX", randomize == true ? value : incDEX);
                }
                if (c.contains("incINT"))
