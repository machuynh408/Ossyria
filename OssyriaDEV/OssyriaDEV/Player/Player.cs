using System;
using System.Collections.Generic;

namespace OssyriaDEV
{
    public class Player : Spawn
    {
        private IConnection c;
        private string username = "";
        private CashInventoryManager cashInventoryManager = null;
        private DiseasesManager diseasesManager = null;
        private EquipmentManager equipmentManager = null;
        private InventoryManager inventoryManager = null;
        private KeyboardManager keyboardManager = null;
        private QuestsManager questsManager = null;
        private SkillsManager skillsManager = null;
        private StatsManager statsManager = null;

        private Interprocess interprocess = null;

        private int chair = 0;
        private object npc = null;
        private Party party = null;
        private Trade trade = null;
        public Player(IConnection c)
        {
            this.c = c;
            this.cashInventoryManager = new CashInventoryManager(this);
            this.diseasesManager = new DiseasesManager(this);
            this.equipmentManager = new EquipmentManager(this);
            this.inventoryManager = new InventoryManager(this);
            this.keyboardManager = new KeyboardManager(this);
            this.questsManager = new QuestsManager(this);
            this.skillsManager = new SkillsManager(this);
            this.statsManager = new StatsManager(this);
            this.interprocess = new Interprocess(this);
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public string getUsername()
        {
            return username;
        }

        public CashInventoryManager getCashInventoryManager()
        {
            return cashInventoryManager;
        }
        public StatsManager getStatsManager()
        {
            return this.statsManager;
        }
        public EquipmentManager getEquipmentManager()
        {
            return this.equipmentManager;
        }
        public InventoryManager getInventoryManager()
        {
            return this.inventoryManager;
        }
        public KeyboardManager getKeyboardManager()
        {
            return keyboardManager;
        }
        public QuestsManager getQuestsManager()
        {
            return this.questsManager;
        }
        public SkillsManager getSkillsManager()
        {
            return this.skillsManager;
        }
        public Interprocess getInterprocess()
        {
            return interprocess;
        }
        public int getId()
        {
            return statsManager.getInt("id");
        }
        public string getName()
        {
            return statsManager.getString("name");
        }
        public int getGender()
        {
            return statsManager.getInt("gender");
        }
        public int getSkin()
        {
            return statsManager.getInt("skin");
        }
        public int getHair()
        {
            return statsManager.getInt("hair");
        }
        public int getFace()
        {
            return statsManager.getInt("face");
        }
        public int getFame()
        {
            return statsManager.getInt("fame");
        }
        public int getLevel()
        {
            return statsManager.getInt("level");
        }
        public int getExp()
        {
            return statsManager.getInt("exp");
        }
        public int getClass()
        {
            return statsManager.getInt("class");
        }
        public int getStr()
        {
            return statsManager.getStr();
        }
        public int getDex()
        {
            return statsManager.getDex();
        }
        public int getInt()
        {
            return statsManager.getInt();
        }
        public int getLuk()
        {
            return statsManager.getLuk();
        }
        public int getAp()
        {
            return statsManager.getAp();
        }
        public int getSp()
        {
            return statsManager.getSp();
        }
        public bool connected()
        {
            return true;
        }

        public void save()
        {
            Database d = new Database();
            List<object> data = new List<object>();
            data.Add(statsManager);
            data.Add(equipmentManager);
            data.Add(inventoryManager);
            data.Add(skillsManager);
            data.Add(keyboardManager);
            d.save(Settings.DATABASE_PATH + @"\accounts\" + getUsername() + @"\players\" + getName() + ".xml", data);
        }
        
        public Equipment getEquipment()
        {
            return equipmentManager.getEquipment();
        }

        public Inventory getInventory(string name)
        {
            switch(name)
            {
                case "equip": return inventoryManager.getEquip();
                case "use": return inventoryManager.getUse();
                case "set-up": return inventoryManager.getSetup();
                case "etc": return inventoryManager.getEtc();
                case "cash": return inventoryManager.getCash();
            }
            return null;
        }
  
        public void teleport(Map destination, int spawn = 0)
        {
            if (destination == null)
            {
