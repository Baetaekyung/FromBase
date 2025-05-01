namespace C__Grammer_Tester.ObserverPattern
{
    public class Product : ISubject
    {
        private List<IObserver> _observers = new();

        private string _productName;
        private string _productAvailability;

        public Product(string productName)
        {
            _productName = productName;
        }

        public string ProductAvailability
        {
            get { return _productAvailability; }
            set
            {
                _productAvailability = value;

                // _productAvailability의 내용이 변경될 때 옵저버들에게 변경 사실을 알림.
                NotifyObservers();
            }
        }

        public void RegisterObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_productAvailability);
            }
        }
    }
}
