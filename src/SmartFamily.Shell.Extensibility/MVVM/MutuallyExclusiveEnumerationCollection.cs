﻿using ReactiveUI;

using System.Collections.ObjectModel;
using System.Reactive;

namespace SmartFamily.MVVM
{
    public class MutuallyExclusiveEnumerationCollection<T> : ObservableCollection<MutuallyExclusiveEnumeration<T>> where T : struct, IComparable
    {
        public MutuallyExclusiveEnumerationCollection(T defaultValue, Action<T> setter)
        {
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                var enumClass = new MutuallyExclusiveEnumeration<T>();
                enumClass.Enumeration = value;
                enumClass.IsChecked = value.Equals(defaultValue) ? true : false;

                Add(enumClass);
            }

            Command = ReactiveCommand.Create<object, Unit>(o =>
            {
                T myEnum = (T)o;

                var collection = this;

                var theClass = collection.First(t => t.Enumeration.Equals(myEnum));

                // ok, they want to check this one, let them and uncheck all else
                foreach (var iter in collection)
                {
                    iter.IsChecked = false;
                }

                theClass.IsChecked = true;

                setter(myEnum);

                return Unit.Default;
            });
        }

        public ReactiveCommand<object, Unit> Command { get; private set; }
    }
}