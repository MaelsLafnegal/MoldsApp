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

        #region Список всех прессформ
        private ObservableCollection<Molds> moldscollection()
        {           
            return new ObservableCollection<Molds>(ApplicationContext.GetContext().Molds.ToList());
        }

        private ObservableCollection<Molds> allMolds { get; set; } = new ObservableCollection<Molds>();
        public ObservableCollection<Molds> AllMolds
        {
            get => allMolds;
            set
            {
                allMolds = moldscollection();
                //OnPropertyChanged();
            }
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

        #endregion

        public MainWindowViewModel()
        {
            #region Экземпляры команд

            CloseApplicationCommand = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion

            AllMolds = moldscollection();
        }
    }
}