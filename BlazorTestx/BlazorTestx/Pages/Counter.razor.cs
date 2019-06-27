using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlazorTestx.Pages
{
    public class ViewForModel<T> : ComponentBase where T : ReactiveObject
    {
        private IDisposable viewModelSubscriptions;
        private T viewModel;

        public T ViewModel
        {
            get => viewModel;
            set
            {
                viewModelSubscriptions?.Dispose();

                viewModel = value;

                viewModelSubscriptions = value.Changed.Do(_ => StateHasChanged()).Subscribe();
            }
        }
    }

    public class CounterViewModel : ReactiveObject
    {
        private readonly IDisposable disposables;

        [Reactive]
        public int Counter { get; set; }

        public CounterViewModel()
        {
            disposables = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)).ObserveOn(CurrentThreadScheduler.Instance).Do(x =>
            {
                Counter++;
            }).Subscribe();
        }
    }
}


//disposables = Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)).Do(x =>
//{
//Invoke(() =>
//{
//    currentCount++;
//    StateHasChanged();
//});
//}).Subscribe();