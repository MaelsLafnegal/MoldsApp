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
using MoldsApp.Views.Windows;

namespace MoldsApp.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
      
        #region Переменные
       
        #region Текстблоки для поиска

        private Molds newMolds { get; set; } = new Molds();
        public string TxtType
        {
            get => newMolds.Type;
            set => newMolds.Type = value;
        }

        public string TxtName
        {
            get => newMolds.Name;
            set => newMolds.Name = value;
        }

        public string TxtKus
        {
            get => newMolds.Kus;
            set => newMolds.Kus = value;
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
                var someshit = new List<Molds>(filteredMolds);
                if (TxtName != null)
                    someshit = someshit.Where(p => p.Name.ToLower().Contains(TxtName.ToLower())).ToList();
                if (TxtType != null)
                    someshit = someshit.Where(p => p.Type.ToLower().Contains(TxtType.ToLower())).ToList();
                if (TxtKus != null)
                    someshit = someshit.Where(p => p.Kus.ToLower().Contains(TxtKus.ToLower())).ToList();
                return new ObservableCollection<Molds>(someshit);
            }
            
            set => filteredMolds = value;
        }

        #endregion

        #region Для EditCommand

        private readonly Func<Molds, bool> editDialog;

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

        #region AddMoldCommand
        public ICommand AddMoldCommand { get; }

        private bool CanAddMoldCommandExecute(object SelectedRow) => true;

        private void OnAddMoldCommandExecuted(object SelectedRow)
        {
            AddEditWindow window = new AddEditWindow()
            {
                DataContext = new AddEditWindowViewModel()
            };
            window.Show();
            //var result = SomeDialogService.ShowDialog(AddEditWindow);
        }
        #endregion

        #region Open AddEditWindowCommand
        public ICommand AddEditWindowCommand { get; }

        private bool CanAddEditWindowCommandExecute(object SelectedRow) => true;

        private void OnAddEditWindowCommandExecuted(object SelectedRow)
        {
            AddEditWindow window = new AddEditWindow()
            {
                DataContext = new AddEditWindowViewModel((Molds)SelectedRow)
            };
            window.Show();
        }
        #endregion

        #region EditCommand
        public ICommand EditCommand { get; }

        private bool CanEditCommandExecute(object p) => p is Molds;

        private void OnEditCommandExecuted(object p)
        {
            Molds molds = (Molds)p;

            // At this point, there should be element preprocessing code.
            // Let's say copying for the possibility of canceling the result of editing.
            Molds moldsCopy = molds.Copy();

            // Calling a dialog and getting its result
            var result = editDialog(moldsCopy);

            if (result)
            {
                // Saving the result.
                // And changing the item to match the edited copy.

                Save(moldsCopy);
                molds.CopyValuesFrom(moldsCopy);
            }
        }
        #endregion

        #region DeleteMoldCommand

        public ICommand DeleteMoldCommand { get; }

        private bool CanDeleteMoldCommandExecute(object SelectedItems)
        {
            if (SelectedItems != null) return true; else return false;           
        }
       
        private void OnDeleteMoldCommandExecuted(object SelectedItems)
        {
            System.Collections.IList items = (System.Collections.IList)SelectedItems;
            var moldsforRemoving = items?.Cast<Molds>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {moldsforRemoving.Count()} пресс-деталей?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ApplicationContext.GetContext().Molds.RemoveRange(moldsforRemoving);
                    ApplicationContext.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены.", "Удаление данных",
         MessageBoxButton.OK, MessageBoxImage.Information);
                    AllMolds = new ObservableCollection<Molds>(ApplicationContext.GetContext().Molds.ToList());
                    OnPropertyChanged("FilteredMolds");
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка",
         MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #region DragMoveCommand

        public ICommand DragMoveCommand { get; }

        private bool CanDragMoveCommandExecute(object p) => true;

        private void OnDragMoveCommandExecuted(object p)
        {
            Application.Current.MainWindow.DragMove();
        }

        #endregion

        #endregion

        #region Методы

        #region Save()
        private void Save(Molds moldsCopy)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Copy()
        private void Copy(Molds)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        public MainWindowViewModel(Func<Molds, bool> editDialog)
        {
            this.editDialog = editDialog;
        }
        public MainWindowViewModel()
        {
            #region Экземпляры команд

            CloseApplicationCommand = new LamdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SearchCommand = new LamdaCommand(OnSearchCommandExecuted, CanSearchCommandExecute);
            AddEditWindowCommand = new LamdaCommand(OnAddEditWindowCommandExecuted, CanAddEditWindowCommandExecute);
            DeleteMoldCommand = new LamdaCommand(OnDeleteMoldCommandExecuted, CanDeleteMoldCommandExecute);
            DragMoveCommand = new LamdaCommand(OnDragMoveCommandExecuted, CanDragMoveCommandExecute);
            AddMoldCommand = new LamdaCommand(OnAddMoldCommandExecuted, CanAddMoldCommandExecute);
            EditCommand = new LamdaCommand(OnEditCommandExecuted, CanEditCommandExecute);


            #endregion

            #region Экземпляры переменных для поиска

            TxtName = null;
            TxtKus = null;
            TxtType = null;

            #endregion
          
        }
    }
}