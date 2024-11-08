namespace Collections
{
    public class ArrayCollection : Collection
    {
        private int SIZE;
        private int cap;
        private object[] data;

        public ArrayCollection(int cap) 
        {
            data = new object[cap];//สร้าง Constructor
            this.cap = cap;

        }

        public void add(object e)//ต้องมีตัวเช็ก Error
        {
            ensureCapacity();
            data[SIZE++] = e;
        }

        private void ensureCapacity()// เช็ก Error ของ add
        {
            if (SIZE + 1 > data.Length)
            {
                object[] tempdata = new object[2 * SIZE];
                for (int i = 0; i < SIZE; i++)
                    tempdata[i] = data[i];
                data = tempdata;
            }
        }

        public bool contains(object e)
        {
            return IndexOf(e) != -1;
        }

        private int IndexOf(object e)
        {

            for (int i = 0; i < SIZE; i++)
            {

                if (data[i].Equals(e))
                    return i;
            }
            return -1;
        }

        public bool isEmpty()
        {
            return SIZE == 0;
        }

        public void remove(object e)
        {
            int i = IndexOf(e);
            if (i < 0)
                return;
            data[i] = data[--SIZE];
            data[SIZE] = null; //จะไม่มีก็ได้ถ้ามั่นใจว่าไม่มาถึงนี่
        }

        public int size()
        {
            return SIZE;
        }

        public object get(int index)
        {
            if (index < 0 || index >= SIZE)
                throw new IndexOutOfRangeException("Index out of bounds.");
            return data[index];
        }

        public void set(int index, object e)
        {
            if (index < 0 || index >= SIZE)
                throw new IndexOutOfRangeException("Index out of bounds.");
            data[index] = e;
        }

    }
}

