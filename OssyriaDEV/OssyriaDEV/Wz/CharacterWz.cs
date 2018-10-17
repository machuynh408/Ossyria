namespace OssyriaDEV
{
    public class CharacterWz : Info
    {
        private int id;
        public CharacterWz(int id)
        {
            this.id = id;
        }

        public int getId()
        {
            return id;
        }
        public int getPrice()
        {
            return getInt("price");
        }

        public bool isCash()
        {
            return getInt("cash") == 1;
        }
