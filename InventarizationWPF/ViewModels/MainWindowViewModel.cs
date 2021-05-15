using InventarizationWPF.Infrastructure.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace InventarizationWPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Выбранная вью-модель

        /// <summary>Выбранная вью-модель</summary>
        private ViewModel _currentViewModel = new ViewModel();

        /// <summary>Выбранная вью-модель</summary>
        public ViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set => Set(ref _currentViewModel, value);
        }

        #endregion

        #region Список доступных вью-моделей

        /// <summary>Список доступных вью-моделей</summary>
        private List<ViewModel> _viewModelsList = new List<ViewModel>();

        /// <summary>Список доступных вью-моделей</summary>
        public List<ViewModel> ViewModelsList
        {
            get => _viewModelsList;
            set => Set(ref _viewModelsList, value);
        }

        #endregion

        #region Команды

        /// <summary>Изменяет текущую вью-модель</summary>
        public ICommand SelectViewModelCommand { get; set; }

        /// <summary>Изменяет текущую вью-модель</summary>
        private void OnSelectViewModelCommandExecute(object parameter)
        {
            CurrentViewModel = ViewModelsList.Where(vm => vm.GetType().Name.Contains(parameter.ToString())).First();
        }

        /// <summary>Проверяет можно ли изменить текущую вью-модель</summary>
        private bool CanSelectViewModelCommandExecuted(object parameter) => true;

        #endregion

        /// <summary>Инициализирует доступные вью-модели</summary>
        public void InitViewModels()
        {
            ViewModelsList.Add(new OfficeViewModel());
            ViewModelsList.Add(new InventoryListViewModel());
            ViewModelsList.Add(new InventoryViewModel());
            CurrentViewModel = ViewModelsList.Where(vm => vm.GetType().Name.Contains("OfficeViewModel")).First();
        }

        /// <summary>Инициализирует вью-модель главного окна</summary>
        public MainWindowViewModel()
        {
            InitViewModels();
            SelectViewModelCommand = new RelayCommand(OnSelectViewModelCommandExecute, CanSelectViewModelCommandExecuted);
        }

    }
}
