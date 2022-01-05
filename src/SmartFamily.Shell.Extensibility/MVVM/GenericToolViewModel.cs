﻿using ReactiveUI;

namespace SmartFamily.MVVM
{
    public abstract class ToolViewModel<T> : ToolViewModel
    {
        private T _model;

        protected ToolViewModel(T model)
            : this(null, model)
        {
        }

        protected ToolViewModel(string title, T model)
            : base(title)
        {
            _model = model;
        }

        public new T Model
        {
            get { return _model; }
            set { this.RaiseAndSetIfChanged(ref _model, value); }
        }
    }
}