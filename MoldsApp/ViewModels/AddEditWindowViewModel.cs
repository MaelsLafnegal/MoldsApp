using MoldsApp.Data;
using MoldsApp.Infrastructure.Commands;
using MoldsApp.Models;
using MoldsApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MoldsApp.ViewModels
{
    internal class AddEditWindowViewModel : ViewModel
    {
        #region Переменные     



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

        public AddEditWindowViewModel()
        {
            #region Экземпляры команд

            CloseApplicationCommand = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SearchCommand = new LamdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);

            #endregion

            #region Экземпляры переменных для поиска

            TxtName = null;
            TxtKus = null;
            TxtType = null;

            #endregion

        }
    }
}
