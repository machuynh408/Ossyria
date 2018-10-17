using System;
using System.Collections.Generic;
using System.Text;

namespace OssyriaDEV
{
    public class Info
    {
        private string name = null;
        private Dictionary<string, object> data = null;
        public Info(string name = "info")
        {
            this.name = name;
            data = new Dictionary<string, object>();
        }
        public Dictionary<string, object> getData()
        {
            return data;
        }

        public string getName()
        {
            return name;
        }

        public bool contains(string name)
        {
            return data.ContainsKey(name);
        }
