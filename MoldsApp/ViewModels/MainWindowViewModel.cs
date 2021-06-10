using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MoldsApp.Data;
using MoldsApp.Infrastructure.Commands;
using MoldsApp.Models;
using MoldsApp.ViewModels.Base;

namespace MoldsApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
      
        #region Переменные

        #region Текстблок поиска по типу

        private Molds newMolds { get; set; } = new Molds();
        public string TxtType
        {
            get => newMolds.Type;
            set => newMolds.Type = value;
        }

        #endregion
       
        #region Список всех прессформ

        private ObservableCollection<Molds> allMolds = new ObservableCollection<Molds>(ApplicationContext.GetContext().Molds.ToList());
        public ObservableCollection<Molds> AllMolds
        {
            get => allMolds; 
            set => allMolds = value;           
        }

        #endregion

        #region Отфильтрованный список прессформ

        private ObservableCollection<Molds> filteredMolds = new ObservableCollection<Molds>(ApplicationContext.GetContext().Molds.ToList());
        public ObservableCollection<Molds> FilteredMolds
        {
            get
            {
                filteredMolds = AllMolds;
                if (string.IsNullOrEmpty(TxtType)) return AllMolds;
                return new ObservableCollection<Molds>(filteredMolds.Where(d => d.Name.ToLower().Contains(TxtType.ToLower())).ToList());
            }

            set => filteredMolds = value;
        }

        #endregion


        #endregion

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region SearchCommand

        public ICommand SearchCommand { get; }

        private bool CanSearchCommandExecute(object p) => true;

        private void OnSearchCommandExecuted(object p)
        {           
            OnPropertyChanged("FilteredMolds");
        }

        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Экземпляры команд

            CloseApplicationCommand = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SearchCommand = new LamdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);

            #endregion
           
        }
    }
}