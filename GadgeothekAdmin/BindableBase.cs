using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GadgeothekAdmin
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// updates the backing field's value of the given property and
        /// informs all observers if the value changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage">a reference to the backing field of the property</param>
        /// <param name="value">the new value of the property</param>
        /// <param name="name">the name of the property as a string (is automatically added by the compiler if omitted)</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string name = null)
        {
            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(name);
            return true;
        }

        /// <summary>
        /// informs all observers about a change in the property with the given name
        /// </summary>
        /// <param name="name">the name of the changed property as a string</param>
        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
