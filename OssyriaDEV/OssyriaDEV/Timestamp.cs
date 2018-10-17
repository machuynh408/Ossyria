using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssyriaDEV
{
    public class Timestamp
    {
        private static readonly DateTime x = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long createTime(int day = 0)
