public class Heap<T>
{

    private List<T> _items;
    private IComparer<T> _comparer;


    public int Capacity
    {
        get => _items.Capacity;
        set => _items.Capacity = value;
    }
    public int Count => _items.Count;


    public Heap(IComparer<T> comparer)
    {

        _items = new List<T>();
        _comparer = comparer;
    }


    public void Add(T input)
    {
        _items.Add(input);
        HeapifyUp(_items.Count - 1);
    }


    public T Remove()
    {
        if (_items.Count == 0)
        {
            // Changed the Exception type and added a message
            throw new InvalidOperationException("The heap is empty.");
        }

        var result = _items[0];

        var temp = _items[0];
        _items[0] = _items[_items.Count - 1];
        _items[_items.Count - 1] = temp;

        _items.RemoveAt(_items.Count - 1);
        HeapifyDown(0);
        return result;
    }

    private void HeapifyUp(int index)
    {
        var parent = (index - 1) / 2;
        if (index > 0 && _comparer.Compare(_items[parent], _items[index]) < 0)
        {


            var temp = _items[parent];
            _items[parent] = _items[index];
            _items[index] = temp;


            HeapifyUp(parent);
        }
    }

    private void HeapifyDown(int index)
    {
        var left = index * 2 + 1;
        var right = index * 2 + 2;
        var highest = index;


        if (left < _items.Count && _comparer.Compare(_items[left], _items[highest]) > 0)
        {
            highest = left;
        }

        if (right < _items.Count && _comparer.Compare(_items[right], _items[highest]) > 0)
        {
            highest = right;
        }

        if (highest != index)
        {
            var temp = _items[highest];
            _items[highest] = _items[index];
            _items[index] = temp;

            HeapifyDown(highest);
        }
    }

}