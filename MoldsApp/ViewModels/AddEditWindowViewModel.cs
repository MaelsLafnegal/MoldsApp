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

        #region Контекст данных
        public Molds CurrentMold { get; }

        public AddEditWindowViewModel()
            : this(new Molds())
        {
        }

        #endregion

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

        #region DragMoveAddEditWindowCommand

        public ICommand DragMoveAddEditWindowCommand { get; }

        private bool CanDragMoveAddEditWindowCommandExecute(object p) => true;

        private void OnDragMoveAddEditWindowCommandExecuted(object p)
        {
            Application.Current.Windows[1].DragMove();
        }

        #endregion

        #region SaveMoldCommand

        public ICommand SaveMoldCommand { get; }

        private bool CanSaveMoldCommandExecute(object p) => true;

        private void OnSaveMoldCommandExecuted(object p)
        {
            if (CurrentMold.Id == 0)
            {
                ApplicationContext.GetContext().Molds.Add(CurrentMold);
            }

            try
            {
                //_currentGame.ImagePreview = File.ReadAllBytes(txtImage.Text);
                ApplicationContext.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена.", "Успешно",
         MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Windows[1].Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка",
        MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #endregion

        public AddEditWindowViewModel(Molds currentMold)
        {
            #region Экземпляры переменных

            CurrentMold = currentMold;

            #endregion

            #region Экземпляры команд

            CloseWindowCommand = new LamdaCommand(OnCloseWindowCommandExecuted, CanCloseWindowCommandExecute);
            DragMoveAddEditWindowCommand = new LamdaCommand(OnDragMoveAddEditWindowCommandExecuted, CanDragMoveAddEditWindowCommandExecute);
            SaveMoldCommand = new LamdaCommand(OnSaveMoldCommandExecuted, CanSaveMoldCommandExecute);
            #endregion
        }
    }
}
