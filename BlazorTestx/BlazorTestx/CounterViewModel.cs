using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlazorTestx
{
    public class CounterViewModel : ReactiveObject
    {

        private readonly IDisposable disposables;

        [Reactive]
        public int Counter { get; set; }

        public CounterViewModel()
        {
            disposables = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2))
                .Do(x =>
                {
                    Counter++;
                }).Subscribe();
        }
    }
}
