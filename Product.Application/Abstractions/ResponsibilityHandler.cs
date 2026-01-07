namespace Product.Application.Abstractions
{
    public abstract class ResponsibilityHandler<T> : IResponsibilityHandler<T>
    {
        private IResponsibilityHandler<T>? _nextHandler;
        public abstract Task Handle(T input);
        public IResponsibilityHandler<T> SetNextHandler(IResponsibilityHandler<T> nextHandler)
        {
            _nextHandler = nextHandler;
            return _nextHandler;
        }
        protected async Task CallNext(T input)
        {
            if (_nextHandler != null) await _nextHandler.Handle(input);
        }
    }
}