namespace OssyriaDEV
{
    public class ItemWz : Info
    {
        private int id = -1;
        private Info spec = null;
        public ItemWz(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }

        public void setSpec(Info spec)
        {
            this.spec = spec;
        }

        public Info getSpec()
        {
            return spec;
        }

        public bool isCash()
        {
            return getBool("cash");
        }

