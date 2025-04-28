namespace C__Grammer_Tester.DataStructures
{
    public class LinkedList<T>
    {
        public int Count => GetCount();

        public class Node
        {
            public T data;
            public Node? next;

            public Node(T data)
            {
                this.data = data;
                next = null;
            }
        }

        private Node? head = null;

        public LinkedList()
        {
            head = null;
        }

        public void PushBack(T data)
        {
            Node newNode = new Node(data);
            
            if(head == null)
            {
                head = newNode;
            }
            else
            {
                Node current = head;
                while(current.next != null)
                {
                    current = current.next;
                }
                current.next = newNode;
            }
        }

        public void PushFront(T data)
        {
            Node newNode = new Node(data);
            newNode.next = head;
            head = newNode;
        }

        public void InsertAt(T data, int index)
        {
            if (index == 0)
            {
                PushFront(data);
                return;
            }

            Node newNode = new Node(data);
            Node current = head;

            for(int i = 0; i < index - 1; i++)
            {
                if (current == null)
                    throw new Exception("Index out of range");

                current = current.next;
            }

            newNode.next = current.next;
            current.next = newNode;
        }

        public void Remove(T data)
        {
            if (head == null)
                return;

            if(EqualityComparer<T>.Default.Equals(head.data, data))
            {
                head = head.next;
                return;
            }

            Node current = head;
            while(current.next != null && EqualityComparer<T>.Default.Equals(current.next.data, data))
            {
                current = current.next;
            }

            if(current.next != null)
            {
                current.next = current.next.next;
            }
        }

        public int GetCount()
        {
            int length = 0;
            Node current = head;
            while(current != null)
            {
                length++;
                current = current.next;
            }
            return length;
        }
    }
}
