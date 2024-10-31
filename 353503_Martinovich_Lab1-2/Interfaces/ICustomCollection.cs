namespace _353503_Martinovich_Lab1.Interfaces
{
    internal interface ICustomCollection<T>
    {

        T this[int index] { get; set; }

        // Метод, устанавливающий курсор в начало коллекции
        void Reset();

        // Метод, перемещающий курсор на следующий элемент коллекции
        void Next();

        // Метод, возвращающий элемент текущего положения курсора
        T Current();

        // Свойство, возвращающее количество элементов в коллекции
        int Count { get; }

        // Метод, добавляющий объект item в конец коллекции
        void Add(T item);

        // Метод, удаляющий объект item из коллекции
        void Remove(T item);

        // Метод, удаляющий элемент текущего положения курсора
        T RemoveCurrent();
        
    }
}
