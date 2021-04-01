using InventarizationWPF.Infrastructure.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace InventarizationWPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Выбранная вью-модель

        private ViewModel _currentViewModel;

        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        }

        #endregion

        #region Список доступных вью-моделей

        private List<ViewModel> _viewModelsList;
        public List<ViewModel> ViewModelsList
        {
            get => _viewModelsList;
            set => Set(ref _viewModelsList, value);
        }

        #endregion

        #region Команды

        public ICommand SelectViewModelCommand { get; set; }

        private void OnSelectViewModelCommandExecute(object parameter)
        {
            ViewModel selectedViewModel = ViewModelsList.FirstOrDefault(vm => nameof(vm) == parameter.ToString());
            CurrentViewModel = selectedViewModel;
        }

        private bool CanSelectViewModelCommandExecuted(object parameter) => true;

        #endregion


        public MainWindowViewModel()
        {
            SelectViewModelCommand = new RelayCommand(OnSelectViewModelCommandExecute, CanSelectViewModelCommandExecuted);
        }

    }
}
