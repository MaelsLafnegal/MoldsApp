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

        private List<Molds> _MoldsList;       
        public List<Molds> MoldsList
        {
            get => _MoldsList;
            set => Set(ref _MoldsList, ApplicationContext.GetContext().Molds.ToList());
        }

        

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
        }
    }
}