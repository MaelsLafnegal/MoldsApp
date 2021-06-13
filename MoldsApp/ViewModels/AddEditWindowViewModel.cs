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

        #region Команды

        #region CloseWindowCommand
        public ICommand CloseWindowCommand { get; }

        private bool CanCloseWindowCommandExecute(object p) => true;

        private void OnCloseWindowCommandExecuted(object p)
        {
            Application.Current.Windows[1].Close();           
        }
        #endregion

        
        #endregion

        public AddEditWindowViewModel()
        {
            #region Экземпляры команд

            CloseWindowCommand = new LamdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            
            #endregion
          
        }
    }
}
