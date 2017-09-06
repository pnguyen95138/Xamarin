using Cirrious.MvvmCross.Interfaces.Commands;

namespace QuickSilver.Core.ViewModels
{
    public class ItemWithCommand<T>
    {
        public ItemWithCommand(T item, IMvxCommand command)
        {
            Item = item;
            Command = command;
        }

        public T Item { get; private set; }
        public IMvxCommand Command { get; private set; }
    }
}