using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Security.Cryptography;

namespace Teploseti
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AdminWindow : MetroWindow
    {
        DBase context;
        Аккаунты account;
        Сотрудники user;
        int id_cur;
        public int id1, id2;
        string id_curr;

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tabControl.SelectedIndex == 0)
                {
                    dataGrid_tab1.ItemsSource = context.Сотрудники.Select(c => new
                    {
                        id = c.id_сотрудника,
                        Фамилия = c.Фамилия,
                        Имя = c.Имя,
                        Отчество = c.Отчество,
                        Должность = c.Должность,
                        Паспорт = c.Паспортные_данные,
                        idacc = c.Аккаунты.id_аккаунта,
                        Администратор = c.Аккаунты.Администратор,
                        Логин = c.Аккаунты.Логин
                    }).ToList();
                }
                if (tabControl.SelectedIndex == 1)
                {
                    if (tabControl1.SelectedIndex == 0)
                    {
                        dataGrid_tab21.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Select(c => new
                        {
                            id = c.id_коэфф_тепл_потери,
                            idm = c.id_материала,
                            СНиП = c.Номер_СНиП,
                            Материал = c.Материал.Наименование_материала,
                            Нач_диаметр = c.Нач_диаметр,
                            Кон_диаметр = c.Кон_диаметр,
                            Коэффициент = c.Коэффициент
                        }).ToList();
                    }
                    if (tabControl1.SelectedIndex == 1)
                    {
                        dataGrid_tab22.ItemsSource = context.Коэффициенты_теплоотдачи.Select(c => new
                        {
                            id = c.id_коэф_тепл_отдачи,
                            СНиП = c.Номер_СНиП,
                            t_до = c.t_до,
                            t_после = c.t_после,
                            Поверхность = c.Тип_поверхности,
                            КПМИ = c.Коэфф_помещения_с_малым_к_изл,
                            КПВИ = c.Коэфф_помещения_с_высоким_к_изл,
                            КОМИ = c.Коэфф_откр_с_малым_к_изл,
                            КОВИ = c.Коэфф_откр_с_высоким_к_изл
                        }).ToList();
                    }
                }
                if (tabControl.SelectedIndex == 2)
                {

                    dataGrid_tab32.ItemsSource = context.Материал.Select(c => new
                    {
                        id = c.id_материала,
                        Н_мат = c.Наименование_материала,
                        К_мат = c.Коэффициент_теплопроводности_материал,
                        Н_из = c.Наименование_изоляции,
                        К_из = c.Коэффициент_теплопроводности_изоляция,
                    }).ToList();

                }
                if (tabControl.SelectedIndex == 3)
                {
                    if (tabControl3.SelectedIndex == 0)
                    {
                        comboBox_tab41.ItemsSource = context.Участок.Select(c => c.Населенный_пункт).ToList();
                        dataGrid_tab41.ItemsSource = context.Участок.Select(c => new
                        {
                            id = c.id_участка,
                            Наименование = c.Наименование,
                            Нас_пункт = c.Населенный_пункт
                        }).ToList();
                    }
                    if (tabControl3.SelectedIndex == 1)
                    {
                        dataGrid_tab42.ItemsSource = context.Труба.Select(c => new
                        {
                            id = c.Код_трубы,
                            idm = c.id_материала,
                            idu = c.id_участка,
                            Длина = c.Длина,
                            ВТД = c.d_внутр_материал,
                            НТД = c.d_наруж_материал,
                            ВТИ = c.d_внутр_изол,
                            НТИ = c.d_наруж_изол,
                            Материал = c.Материал.Наименование_материала,
                            Участок = c.Участок.Наименование
                        }).ToList();
                    }
                }
                if (tabControl.SelectedIndex == 4)
                {
                    dataGrid_tab5.ItemsSource = context.Обслуживание.Select(c => new
                    {
                        id = c.id_обслуживания,
                        idt = c.Код_трубы,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Бригада = c.Номер_бригады,
                        Результат = c.Результат
                    }).ToList();
                }
                if (tabControl.SelectedIndex == 5)
                {
                    dataGrid_tab6.ItemsSource = context.Фактические_потери.Select(c => new
                    {
                        id = c.id_факт_потерь,
                        idt = c.Код_трубы,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Значение = c.Значение
                    }).ToList();
                }
                if (tabControl.SelectedIndex == 6)
                {
                    dataGrid_tab7.ItemsSource = context.Расчетные_потери.Select(c => new
                    {
                        id = c.id_расчетных_потерь,
                        idt = c.Код_трубы,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Значение = c.Результат,
                        Сотрудник = c.Сотрудники.Фамилия + " " + c.Сотрудники.Имя
                    }).ToList();
                }
                if (tabControl.SelectedIndex == 8)
                {
                    datePicker1_tab8.DisplayDateEnd = DateTime.Now;
                    datePicker2_tab8.DisplayDateEnd = DateTime.Now;
                }
            }
        }

        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    dataGrid_tab21.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Select(c => new
                    {
                        id = c.id_коэфф_тепл_потери,
                        idm = c.id_материала,
                        СНиП = c.Номер_СНиП,
                        Материал = c.Материал.Наименование_материала,
                        Нач_диаметр = c.Нач_диаметр,
                        Кон_диаметр = c.Кон_диаметр,
                        Коэффициент = c.Коэффициент
                    }).ToList();
                }
                if (tabControl1.SelectedIndex == 1)
                {
                    dataGrid_tab22.ItemsSource = context.Коэффициенты_теплоотдачи.Select(c => new
                    {
                        id = c.id_коэф_тепл_отдачи,
                        СНиП = c.Номер_СНиП,
                        t_до = c.t_до,
                        t_после = c.t_после,
                        Поверхность = c.Тип_поверхности,
                        КПМИ = c.Коэфф_помещения_с_малым_к_изл,
                        КПВИ = c.Коэфф_помещения_с_высоким_к_изл,
                        КОМИ = c.Коэфф_откр_с_малым_к_изл,
                        КОВИ = c.Коэфф_откр_с_высоким_к_изл
                    }).ToList();
                }
            }
        }

        private void tabControl3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tabControl3.SelectedIndex == 0)
                {
                    dataGrid_tab41.ItemsSource = context.Участок.Select(c => new
                    {
                        id = c.id_участка,
                        Наименование = c.Наименование,
                        Нас_пункт = c.Населенный_пункт
                    }).ToList();
                }
                if (tabControl3.SelectedIndex == 1)
                {
                    dataGrid_tab42.ItemsSource = context.Труба.Select(c => new
                    {
                        id = c.Код_трубы,
                        idm = c.id_материала,
                        idu = c.id_участка,
                        Длина = c.Длина,
                        ВТД = c.d_внутр_материал,
                        НТД = c.d_наруж_материал,
                        ВТИ = c.d_внутр_изол,
                        НТИ = c.d_наруж_изол,
                        Материал = c.Материал.Наименование_материала,
                        Участок = c.Участок.Наименование
                    }).ToList();
                }
            }
        }

        private void dataGrid_tab1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab1.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab1.Columns[0].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text);
            textBox_tab1_1.Text = (dataGrid_tab1.Columns[2].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text;
            textBox_tab1_2.Text = (dataGrid_tab1.Columns[3].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text;
            textBox_tab1_3.Text = (dataGrid_tab1.Columns[4].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text;
            textBox_tab1_4.Text = (dataGrid_tab1.Columns[5].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text;
            maskedTextBox.Text = (dataGrid_tab1.Columns[6].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text;
            id1 = Convert.ToInt32((dataGrid_tab1.Columns[1].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text);
            checkBox_tab1.IsChecked = Convert.ToBoolean((dataGrid_tab1.Columns[7].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text);
            textBox_tab1_6.Text = (dataGrid_tab1.Columns[8].GetCellContent(dataGrid_tab1.Items[dataGrid_tab1.SelectedIndex]) as TextBlock).Text;
        }

        private void button_tab1_add_Click(object sender, RoutedEventArgs e)
        {
            //if (!checkFields())
            //{
            //    MessageBox.Show("Не заполнены все поля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            if (passwordBox_tab1.Password != passwordBox_tab1_2.Password)
            {
                MessageBox.Show("The passwords don't match! Write in \"Password again\" matching password!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (passwordBox_tab1.Password.Length < 6)
            {
                MessageBox.Show("Password length must be at least 6 characters long!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (!maskedTextBox.IsMaskFull)
            {
                MessageBox.Show("Incorrect passport data is entered!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            Сотрудники row = new Сотрудники();

            Аккаунты roww = new Аккаунты();
            try
            {
                row.Фамилия = textBox_tab1_1.Text;
                row.Имя = textBox_tab1_2.Text;
                row.Отчество = textBox_tab1_3.Text;
                row.Должность = textBox_tab1_4.Text;
                row.Паспортные_данные = maskedTextBox.Text;
                roww.Логин = textBox_tab1_6.Text;
                roww.Хэш_пароль = getMD5String(passwordBox_tab1.Password);
                roww.Администратор = (checkBox_tab1.IsChecked == true) ? true : false;
                context.Аккаунты.Add(roww);
                context.SaveChanges();
                row.id_аккаунта = roww.id_аккаунта;
                context.Сотрудники.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab1.ItemsSource = context.Сотрудники.Select(c => new
                {
                    id = c.id_сотрудника,
                    Фамилия = c.Фамилия,
                    Имя = c.Имя,
                    Отчество = c.Отчество,
                    Должность = c.Должность,
                    Паспорт = c.Паспортные_данные,
                    idacc = c.Аккаунты.id_аккаунта,
                    Администратор = c.Аккаунты.Администратор,
                    Логин = c.Аккаунты.Логин
                }).ToList();
                textBox_tab1_1.Text = textBox_tab1_2.Text = textBox_tab1_3.Text = textBox_tab1_4.Text = maskedTextBox.Text = textBox_tab1_6.Text = passwordBox_tab1.Password = "";
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab1_edit_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox_tab1.Password != passwordBox_tab1_2.Password)
            {
                MessageBox.Show("The passwords don't match! Write in \"Password again\" matching password!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (passwordBox_tab1.Password.Length < 6)
            {
                MessageBox.Show("Password length must be at least 6 characters long!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            if (!maskedTextBox.IsMaskFull)
            {
                MessageBox.Show("Incorrect passport data is entered!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            Сотрудники row = context.Сотрудники.Where(c => c.id_сотрудника == id_cur).First();
            Аккаунты roww = context.Аккаунты.Where(c => c.id_аккаунта == id1).First();
            try
            {
                row.Фамилия = textBox_tab1_1.Text;
                row.Имя = textBox_tab1_2.Text;
                row.Отчество = textBox_tab1_3.Text;
                row.Должность = textBox_tab1_4.Text;
                row.Паспортные_данные = maskedTextBox.Text;
                roww.Логин = textBox_tab1_6.Text;
                roww.Администратор = (checkBox_tab1.IsChecked == true) ? true : false;
                if (passwordBox_tab1.Password.Trim() != String.Empty)
                {
                    roww.Хэш_пароль = getMD5String(passwordBox_tab1.Password);
                }
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab1.ItemsSource = context.Сотрудники.Select(c => new
                {
                    id = c.id_сотрудника,
                    Фамилия = c.Фамилия,
                    Имя = c.Имя,
                    Отчество = c.Отчество,
                    Должность = c.Должность,
                    Паспорт = c.Паспортные_данные,
                    idacc = c.Аккаунты.id_аккаунта,
                    Администратор = c.Аккаунты.Администратор,
                    Логин = c.Аккаунты.Логин
                }).ToList();
                textBox_tab1_1.Text = textBox_tab1_2.Text = textBox_tab1_3.Text = textBox_tab1_4.Text = maskedTextBox.Text = textBox_tab1_6.Text = passwordBox_tab1.Password = "";
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void button_tab1_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Сотрудники row = context.Сотрудники.Where(c => c.id_сотрудника == id_cur).First();
                Аккаунты roww = context.Аккаунты.Where(c => c.id_аккаунта == id1).First();
                try
                {
                    context.Аккаунты.Remove(roww);
                    context.Сотрудники.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    dataGrid_tab1.ItemsSource = context.Сотрудники.Select(c => new
                    {
                        id = c.id_сотрудника,
                        Фамилия = c.Фамилия,
                        Имя = c.Имя,
                        Отчество = c.Отчество,
                        Должность = c.Должность,
                        Паспорт = c.Паспортные_данные,
                        idacc = c.Аккаунты.id_аккаунта,
                        Администратор = c.Аккаунты.Администратор,
                        Логин = c.Аккаунты.Логин
                    }).ToList();
                    textBox_tab1_1.Text = textBox_tab1_2.Text = textBox_tab1_3.Text = textBox_tab1_4.Text = maskedTextBox.Text = textBox_tab1_6.Text = passwordBox_tab1.Password = "";
                    id_cur = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void button_tab1_clear_Click(object sender, RoutedEventArgs e)
        {
            id_cur = id1 = -1;
            textBox_tab1_1.Text = textBox_tab1_2.Text = textBox_tab1_3.Text = textBox_tab1_4.Text = maskedTextBox.Text = textBox_tab1_6.Text = passwordBox_tab1.Password = "";
        }
        private void dataGrid_tab21_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab21.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab21.Columns[0].GetCellContent(dataGrid_tab21.Items[dataGrid_tab21.SelectedIndex]) as TextBlock).Text);
            id1 = Convert.ToInt32((dataGrid_tab21.Columns[1].GetCellContent(dataGrid_tab21.Items[dataGrid_tab21.SelectedIndex]) as TextBlock).Text);
            textBox_tab21_1.Text = (dataGrid_tab21.Columns[2].GetCellContent(dataGrid_tab21.Items[dataGrid_tab21.SelectedIndex]) as TextBlock).Text;
            textBox_tab21_2.Text = (dataGrid_tab21.Columns[3].GetCellContent(dataGrid_tab21.Items[dataGrid_tab21.SelectedIndex]) as TextBlock).Text;
            textBox_tab21_3.Text = (dataGrid_tab21.Columns[4].GetCellContent(dataGrid_tab21.Items[dataGrid_tab21.SelectedIndex]) as TextBlock).Text;
            textBox_tab21_4.Text = (dataGrid_tab21.Columns[5].GetCellContent(dataGrid_tab21.Items[dataGrid_tab21.SelectedIndex]) as TextBlock).Text;
            textBox_tab21_5.Text = (dataGrid_tab21.Columns[6].GetCellContent(dataGrid_tab21.Items[dataGrid_tab21.SelectedIndex]) as TextBlock).Text;
        }
        private void button_tab21_choose_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 0, this, 0, textBox_tab21_2);
            form.ShowDialog();
        }

        private void button_tab2_add_Click(object sender, RoutedEventArgs e)
        {
            Коэффициенты_учитыв__тепловые_потери row = new Коэффициенты_учитыв__тепловые_потери();
            try
            {
                row.Номер_СНиП = textBox_tab21_1.Text;
                row.id_материала = id1;
                row.Нач_диаметр = Convert.ToDouble(textBox_tab21_3.Text);
                row.Кон_диаметр = Convert.ToDouble(textBox_tab21_4.Text);
                row.Коэффициент = Convert.ToDouble(textBox_tab21_5.Text);
                context.Коэффициенты_учитыв__тепловые_потери.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab21.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Select(c => new
                {
                    id = c.id_коэфф_тепл_потери,
                    idm = c.id_материала,
                    СНиП = c.Номер_СНиП,
                    Материал = c.Материал.Наименование_материала,
                    Нач_диаметр = c.Нач_диаметр,
                    Кон_диаметр = c.Кон_диаметр,
                    Коэффициент = c.Коэффициент
                }).ToList();
                id_cur = id1 = -1;
                textBox_tab21_1.Text = textBox_tab21_2.Text = textBox_tab21_3.Text = textBox_tab21_4.Text = "";
                textBox_tab21_5.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab2_edit_Click(object sender, RoutedEventArgs e)
        {
            Коэффициенты_учитыв__тепловые_потери row = context.Коэффициенты_учитыв__тепловые_потери.Where(c => c.id_коэфф_тепл_потери == id_cur).First();
            try
            {
                row.Номер_СНиП = textBox_tab21_1.Text;
                row.id_материала = id1;
                row.Нач_диаметр = Convert.ToDouble(textBox_tab21_3.Text);
                row.Кон_диаметр = Convert.ToDouble(textBox_tab21_4.Text);
                row.Коэффициент = Convert.ToDouble(textBox_tab21_5.Text);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab21.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Select(c => new
                {
                    id = c.id_коэфф_тепл_потери,
                    idm = c.id_материала,
                    СНиП = c.Номер_СНиП,
                    Материал = c.Материал.Наименование_материала,
                    Нач_диаметр = c.Нач_диаметр,
                    Кон_диаметр = c.Кон_диаметр,
                    Коэффициент = c.Коэффициент
                }).ToList();
                id_cur = id1 = -1;
                textBox_tab21_1.Text = textBox_tab21_2.Text = textBox_tab21_3.Text = textBox_tab21_4.Text = textBox_tab21_5.Text = "";
                textBox_tab21_5.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab2_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Коэффициенты_учитыв__тепловые_потери row = context.Коэффициенты_учитыв__тепловые_потери.Where(c => c.id_коэфф_тепл_потери == id_cur).First();
                try
                {
                    context.Коэффициенты_учитыв__тепловые_потери.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    dataGrid_tab21.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Select(c => new
                    {
                        id = c.id_коэфф_тепл_потери,
                        idm = c.id_материала,
                        СНиП = c.Номер_СНиП,
                        Материал = c.Материал.Наименование_материала,
                        Нач_диаметр = c.Нач_диаметр,
                        Кон_диаметр = c.Кон_диаметр,
                        Коэффициент = c.Коэффициент
                    }).ToList();
                    id_cur = id1 = -1;
                    textBox_tab21_1.Text = textBox_tab21_2.Text = textBox_tab21_3.Text = textBox_tab21_4.Text = textBox_tab21_5.Text = "";
                    textBox_tab21_5.Text = "0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_tab2_clear_Click(object sender, RoutedEventArgs e)
        {
            id_cur = id1 = -1;
            textBox_tab21_1.Text = textBox_tab21_2.Text = textBox_tab21_3.Text = textBox_tab21_4.Text = textBox_tab21_5.Text = "";
            textBox_tab21_5.Text = "0";
        }
        private void dataGrid_tab22_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab22.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab22.Columns[0].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text);
            textBox_tab22_1.Text = (dataGrid_tab22.Columns[1].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text;
            textBox_tab22_2.Text = (dataGrid_tab22.Columns[2].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text;
            textBox_tab22_3.Text = (dataGrid_tab22.Columns[3].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text;
            comboBox_tab22.Text = (dataGrid_tab22.Columns[4].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text;
            textBox_tab22_4.Text = (dataGrid_tab22.Columns[5].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text;
            textBox_tab22_5.Text = (dataGrid_tab22.Columns[6].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text;
            textBox_tab22_6.Text = (dataGrid_tab22.Columns[7].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text;
            textBox_tab22_7.Text = (dataGrid_tab22.Columns[8].GetCellContent(dataGrid_tab22.Items[dataGrid_tab22.SelectedIndex]) as TextBlock).Text;
        }

        private void button_tab22_add_Click(object sender, RoutedEventArgs e)
        {
            Коэффициенты_теплоотдачи row = new Коэффициенты_теплоотдачи();
            try
            {
                row.Номер_СНиП = textBox_tab22_1.Text;
                row.t_до = textBox_tab22_2.Text.Trim() != "" ? (double?)Convert.ToDouble(textBox_tab22_2.Text) : null;
                row.t_после = textBox_tab22_3.Text.Trim() != "" ? (double?)Convert.ToDouble(textBox_tab22_3.Text) : null;
                row.Тип_поверхности = comboBox_tab22.Text;
                row.Коэфф_помещения_с_малым_к_изл = Convert.ToDouble(textBox_tab22_4.Text);
                row.Коэфф_помещения_с_высоким_к_изл = Convert.ToDouble(textBox_tab22_5.Text);
                row.Коэфф_откр_с_малым_к_изл = Convert.ToDouble(textBox_tab22_6.Text);
                row.Коэфф_откр_с_высоким_к_изл = Convert.ToDouble(textBox_tab22_7.Text);
                context.Коэффициенты_теплоотдачи.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab22.ItemsSource = context.Коэффициенты_теплоотдачи.Select(c => new
                {
                    id = c.id_коэф_тепл_отдачи,
                    СНиП = c.Номер_СНиП,
                    t_до = c.t_до,
                    t_после = c.t_после,
                    Поверхность = c.Тип_поверхности,
                    КПМИ = c.Коэфф_помещения_с_малым_к_изл,
                    КПВИ = c.Коэфф_помещения_с_высоким_к_изл,
                    КОМИ = c.Коэфф_откр_с_малым_к_изл,
                    КОВИ = c.Коэфф_откр_с_высоким_к_изл
                }).ToList();
                textBox_tab22_1.Text = textBox_tab22_2.Text = textBox_tab22_3.Text = "";
                comboBox_tab22.SelectedIndex = 0;
                textBox_tab22_4.Text = textBox_tab22_5.Text = textBox_tab22_6.Text = textBox_tab22_7.Text = "0";
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void button_tab22_edit_Click(object sender, RoutedEventArgs e)
        {
            Коэффициенты_теплоотдачи row = context.Коэффициенты_теплоотдачи.Where(c => c.id_коэф_тепл_отдачи == id_cur).First();
            try
            {
                row.Номер_СНиП = textBox_tab22_1.Text;
                row.t_до = textBox_tab22_2.Text.Trim() != "" ? (double?)Convert.ToDouble(textBox_tab22_2.Text) : null;
                row.t_после = textBox_tab22_3.Text.Trim() != "" ? (double?)Convert.ToDouble(textBox_tab22_3.Text) : null;
                row.Тип_поверхности = comboBox_tab22.Text;
                row.Коэфф_помещения_с_малым_к_изл = Convert.ToDouble(textBox_tab22_4.Text);
                row.Коэфф_помещения_с_высоким_к_изл = Convert.ToDouble(textBox_tab22_5.Text);
                row.Коэфф_откр_с_малым_к_изл = Convert.ToDouble(textBox_tab22_6.Text);
                row.Коэфф_откр_с_высоким_к_изл = Convert.ToDouble(textBox_tab22_7.Text);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab22.ItemsSource = context.Коэффициенты_теплоотдачи.Select(c => new
                {
                    id = c.id_коэф_тепл_отдачи,
                    СНиП = c.Номер_СНиП,
                    t_до = c.t_до,
                    t_после = c.t_после,
                    Поверхность = c.Тип_поверхности,
                    КПМИ = c.Коэфф_помещения_с_малым_к_изл,
                    КПВИ = c.Коэфф_помещения_с_высоким_к_изл,
                    КОМИ = c.Коэфф_откр_с_малым_к_изл,
                    КОВИ = c.Коэфф_откр_с_высоким_к_изл
                }).ToList();
                textBox_tab22_1.Text = textBox_tab22_2.Text = textBox_tab22_3.Text = "";
                textBox_tab22_4.Text = textBox_tab22_5.Text = textBox_tab22_6.Text = textBox_tab22_7.Text = "0";
                comboBox_tab22.SelectedIndex = 0;
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab22_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Коэффициенты_теплоотдачи row = context.Коэффициенты_теплоотдачи.Where(c => c.id_коэф_тепл_отдачи == id_cur).First();
                try
                {
                    context.Коэффициенты_теплоотдачи.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    dataGrid_tab22.ItemsSource = context.Коэффициенты_теплоотдачи.Select(c => new
                    {
                        id = c.id_коэф_тепл_отдачи,
                        СНиП = c.Номер_СНиП,
                        t_до = c.t_до,
                        t_после = c.t_после,
                        Поверхность = c.Тип_поверхности,
                        КПМИ = c.Коэфф_помещения_с_малым_к_изл,
                        КПВИ = c.Коэфф_помещения_с_высоким_к_изл,
                        КОМИ = c.Коэфф_откр_с_малым_к_изл,
                        КОВИ = c.Коэфф_откр_с_высоким_к_изл
                    }).ToList();
                    textBox_tab22_1.Text = textBox_tab22_2.Text = textBox_tab22_3.Text = "";
                    textBox_tab22_4.Text = textBox_tab22_5.Text = textBox_tab22_6.Text = textBox_tab22_7.Text = "0";
                    comboBox_tab22.SelectedIndex = 0;
                    id_cur = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_tab22_clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_tab22_1.Text = textBox_tab22_2.Text = textBox_tab22_3.Text = "";
            textBox_tab22_4.Text = textBox_tab22_5.Text = textBox_tab22_6.Text = textBox_tab22_7.Text = "0";
            comboBox_tab22.SelectedIndex = 0;
            id_cur = -1;
        }



        private void dataGrid_tab32_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab32.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab32.Columns[0].GetCellContent(dataGrid_tab32.Items[dataGrid_tab32.SelectedIndex]) as TextBlock).Text);
            textBox_tab32_1.Text = (dataGrid_tab32.Columns[1].GetCellContent(dataGrid_tab32.Items[dataGrid_tab32.SelectedIndex]) as TextBlock).Text;
            textBox_tab32_2.Text = (dataGrid_tab32.Columns[2].GetCellContent(dataGrid_tab32.Items[dataGrid_tab32.SelectedIndex]) as TextBlock).Text;
            textBox_tab32_3.Text = (dataGrid_tab32.Columns[3].GetCellContent(dataGrid_tab32.Items[dataGrid_tab32.SelectedIndex]) as TextBlock).Text;
            textBox_tab32_4.Text = (dataGrid_tab32.Columns[4].GetCellContent(dataGrid_tab32.Items[dataGrid_tab32.SelectedIndex]) as TextBlock).Text;
        }


        private void button_tab32_add_Click(object sender, RoutedEventArgs e)
        {
            Материал row = new Материал();
            try
            {
                row.Наименование_материала = textBox_tab32_1.Text;
                row.Коэффициент_теплопроводности_материал = Convert.ToDouble(textBox_tab32_2.Text);
                row.Наименование_изоляции = textBox_tab32_3.Text;
                row.Коэффициент_теплопроводности_изоляция = Convert.ToDouble(textBox_tab32_4.Text);
                context.Материал.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab32.ItemsSource = context.Материал.Select(c => new
                {
                    id = c.id_материала,
                    Н_мат = c.Наименование_материала,
                    К_мат = c.Коэффициент_теплопроводности_материал,
                    Н_из = c.Наименование_изоляции,
                    К_из = c.Коэффициент_теплопроводности_изоляция,
                }).ToList();
                textBox_tab32_1.Text = textBox_tab32_3.Text = "";
                textBox_tab32_2.Text = textBox_tab32_4.Text = "0";
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab32_edit_Click(object sender, RoutedEventArgs e)
        {
            Материал row = context.Материал.Where(c => c.id_материала == id_cur).First();
            try
            {
                row.Наименование_материала = textBox_tab32_1.Text;
                row.Коэффициент_теплопроводности_материал = Convert.ToDouble(textBox_tab32_2.Text);
                row.Наименование_изоляции = textBox_tab32_3.Text;
                row.Коэффициент_теплопроводности_изоляция = Convert.ToDouble(textBox_tab32_4.Text);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab32.ItemsSource = context.Материал.Select(c => new
                {
                    id = c.id_материала,
                    Н_мат = c.Наименование_материала,
                    К_мат = c.Коэффициент_теплопроводности_материал,
                    Н_из = c.Наименование_изоляции,
                    К_из = c.Коэффициент_теплопроводности_изоляция,
                }).ToList();
                textBox_tab32_1.Text = textBox_tab32_3.Text = "";
                textBox_tab32_2.Text = textBox_tab32_4.Text = "0";
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab32_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Материал row = context.Материал.Where(c => c.id_материала == id_cur).First();
                try
                {
                    context.Материал.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    dataGrid_tab32.ItemsSource = context.Материал.Select(c => new
                    {
                        id = c.id_материала,
                        Н_мат = c.Наименование_материала,
                        К_мат = c.Коэффициент_теплопроводности_материал,
                        Н_из = c.Наименование_изоляции,
                        К_из = c.Коэффициент_теплопроводности_изоляция,
                    }).ToList();
                    textBox_tab32_1.Text = textBox_tab32_3.Text = "";
                    textBox_tab32_2.Text = textBox_tab32_4.Text = "0";
                    id_cur = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_tab32_clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_tab32_1.Text = textBox_tab32_3.Text = "";
            textBox_tab32_2.Text = textBox_tab32_4.Text = "0";
            id_cur = -1;
        }
        private void dataGrid_tab41_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab41.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab41.Columns[0].GetCellContent(dataGrid_tab41.Items[dataGrid_tab41.SelectedIndex]) as TextBlock).Text);
            textBox_tab41_1.Text = (dataGrid_tab41.Columns[1].GetCellContent(dataGrid_tab41.Items[dataGrid_tab41.SelectedIndex]) as TextBlock).Text;
            comboBox_tab41.Text = (dataGrid_tab41.Columns[2].GetCellContent(dataGrid_tab41.Items[dataGrid_tab41.SelectedIndex]) as TextBlock).Text;
        }

        private void button_tab41_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Участок row = new Участок();
                row.Наименование = textBox_tab41_1.Text;
                row.Населенный_пункт = comboBox_tab41.Text;
                context.Участок.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                comboBox_tab41.ItemsSource = context.Участок.Select(c => c.Населенный_пункт).ToList();
                dataGrid_tab41.ItemsSource = context.Участок.Select(c => new
                {
                    id = c.id_участка,
                    Наименование = c.Наименование,
                    Нас_пункт = c.Населенный_пункт
                }).ToList();
                textBox_tab41_1.Text = comboBox_tab41.Text = "";
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab41_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Участок row = context.Участок.Where(c => c.id_участка == id_cur).First();
                row.Наименование = textBox_tab41_1.Text;
                row.Населенный_пункт = comboBox_tab41.Text;
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                comboBox_tab41.ItemsSource = context.Участок.Select(c => c.Населенный_пункт).ToList();
                dataGrid_tab41.ItemsSource = context.Участок.Select(c => new
                {
                    id = c.id_участка,
                    Наименование = c.Наименование,
                    Нас_пункт = c.Населенный_пункт
                }).ToList();
                textBox_tab41_1.Text = comboBox_tab41.Text = "";
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab41_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    Участок row = context.Участок.Where(c => c.id_участка == id_cur).First();
                    context.Участок.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    comboBox_tab41.ItemsSource = context.Участок.Select(c => c.Населенный_пункт).ToList();
                    dataGrid_tab41.ItemsSource = context.Участок.Select(c => new
                    {
                        id = c.id_участка,
                        Наименование = c.Наименование,
                        Нас_пункт = c.Населенный_пункт
                    }).ToList();
                    textBox_tab41_1.Text = comboBox_tab41.Text = "";
                    id_cur = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_tab41_clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_tab41_1.Text = comboBox_tab41.Text = "";
            id_cur = -1;
        }

        private void dataGrid_tab42_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab42.SelectedIndex == -1) return;
            id_curr = (dataGrid_tab42.Columns[0].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
            id1 = Convert.ToInt32((dataGrid_tab42.Columns[1].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text);
            id2 = Convert.ToInt32((dataGrid_tab42.Columns[2].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text);
            textBox_tab42_1.Text = (dataGrid_tab42.Columns[0].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
            textBox_tab42_2.Text = (dataGrid_tab42.Columns[3].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
            textBox_tab42_3.Text = (dataGrid_tab42.Columns[4].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
            textBox_tab42_4.Text = (dataGrid_tab42.Columns[5].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
            textBox_tab42_5.Text = (dataGrid_tab42.Columns[6].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
            textBox_tab42_6.Text = (dataGrid_tab42.Columns[7].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
            textBox_tab42_7.Text = (dataGrid_tab42.Columns[8].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
            textBox_tab42_8.Text = (dataGrid_tab42.Columns[9].GetCellContent(dataGrid_tab42.Items[dataGrid_tab42.SelectedIndex]) as TextBlock).Text;
        }

        private void button_tab42_choose_1_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 0, this, 0, textBox_tab42_7);
            form.ShowDialog();
        }

        private void button_tab42_choose_2_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 1, this, 0, textBox_tab42_8);
            form.ShowDialog();
        }

        private void button_tab42_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Труба row = new Труба();
                row.Код_трубы = textBox_tab42_1.Text;
                row.id_материала = id1;
                row.id_участка = id2;
                row.Длина = Convert.ToInt32(textBox_tab42_2.Text);
                row.d_внутр_материал = Convert.ToDouble(textBox_tab42_3.Text);
                row.d_наруж_материал = Convert.ToDouble(textBox_tab42_4.Text);
                row.d_внутр_изол = Convert.ToDouble(textBox_tab42_5.Text);
                row.d_наруж_изол = Convert.ToDouble(textBox_tab42_6.Text);
                context.Труба.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab42.ItemsSource = context.Труба.Select(c => new
                {
                    id = c.Код_трубы,
                    idm = c.id_материала,
                    idu = c.id_участка,
                    Длина = c.Длина,
                    ВТД = c.d_внутр_материал,
                    НТД = c.d_наруж_материал,
                    ВТИ = c.d_внутр_изол,
                    НТИ = c.d_наруж_изол,
                    Материал = c.Материал.Наименование_материала,
                    Участок = c.Участок.Наименование
                }).ToList();
                textBox_tab42_1.Text = textBox_tab42_7.Text = textBox_tab42_8.Text = "";
                textBox_tab42_2.Text = textBox_tab42_3.Text = textBox_tab42_4.Text = textBox_tab42_5.Text = textBox_tab42_6.Text = "0";
                id_curr = "";
                id_cur = id1 = id2 = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab42_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Труба row = context.Труба.Where(c => c.Код_трубы == id_curr).First();
                row.Код_трубы = textBox_tab42_1.Text;
                row.id_материала = id1;
                row.id_участка = id2;
                row.Длина = Convert.ToInt32(textBox_tab42_2.Text);
                row.d_внутр_материал = Convert.ToDouble(textBox_tab42_3.Text);
                row.d_наруж_материал = Convert.ToDouble(textBox_tab42_4.Text);
                row.d_внутр_изол = Convert.ToDouble(textBox_tab42_5.Text);
                row.d_наруж_изол = Convert.ToDouble(textBox_tab42_6.Text);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab42.ItemsSource = context.Труба.Select(c => new
                {
                    id = c.Код_трубы,
                    idm = c.id_материала,
                    idu = c.id_участка,
                    Длина = c.Длина,
                    ВТД = c.d_внутр_материал,
                    НТД = c.d_наруж_материал,
                    ВТИ = c.d_внутр_изол,
                    НТИ = c.d_наруж_изол,
                    Материал = c.Материал.Наименование_материала,
                    Участок = c.Участок.Наименование
                }).ToList();
                textBox_tab42_1.Text = textBox_tab42_7.Text = textBox_tab42_8.Text = "";
                textBox_tab42_2.Text = textBox_tab42_3.Text = textBox_tab42_4.Text = textBox_tab42_5.Text = textBox_tab42_6.Text = "0";
                id_curr = "";
                id_cur = id1 = id2 = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab42_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    Труба row = context.Труба.Where(c => c.Код_трубы == id_curr).First();
                    context.Труба.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    dataGrid_tab42.ItemsSource = context.Труба.Select(c => new
                    {
                        id = c.Код_трубы,
                        idm = c.id_материала,
                        idu = c.id_участка,
                        Длина = c.Длина,
                        ВТД = c.d_внутр_материал,
                        НТД = c.d_наруж_материал,
                        ВТИ = c.d_внутр_изол,
                        НТИ = c.d_наруж_изол,
                        Материал = c.Материал.Наименование_материала,
                        Участок = c.Участок.Наименование
                    }).ToList();
                    textBox_tab42_1.Text = textBox_tab42_7.Text = textBox_tab42_8.Text = "";
                    textBox_tab42_2.Text = textBox_tab42_3.Text = textBox_tab42_4.Text = textBox_tab42_5.Text = textBox_tab42_6.Text = "0";
                    id_curr = "";
                    id_cur = id1 = id2 = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_tab42_clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_tab42_1.Text = textBox_tab42_7.Text = textBox_tab42_8.Text = "";
            textBox_tab42_2.Text = textBox_tab42_3.Text = textBox_tab42_4.Text = textBox_tab42_5.Text = textBox_tab42_6.Text = "0";
            id_curr = "";
            id_cur = id1 = id2 = -1;
        }

        private void dataGrid_tab5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab5.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab5.Columns[0].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text);
            textBox_tab5_1.Text = (dataGrid_tab5.Columns[2].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text;
            textBox_tab5_2.Text = (dataGrid_tab5.Columns[4].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text;
            datePicker_tab5.SelectedDate = Convert.ToDateTime((dataGrid_tab5.Columns[3].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text);
            textBox_tab5_3.Text = (dataGrid_tab5.Columns[5].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text;
        }

        private void button_tab5_choose_1_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 2, this, 0, textBox_tab5_1);
            form.ShowDialog();
        }

        private void button_tab5_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Обслуживание row = new Обслуживание();
                row.Код_трубы = textBox_tab5_1.Text;
                row.Номер_бригады = textBox_tab5_2.Text;
                row.Дата = datePicker_tab5.SelectedDate ?? DateTime.Now;
                row.Результат = textBox_tab5_3.Text;
                row.id_сотрудника = account.id_аккаунта;
                context.Обслуживание.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab5.ItemsSource = context.Обслуживание.Select(c => new
                {
                    id = c.id_обслуживания,
                    idt = c.Код_трубы,
                    Участок = c.Труба.Участок.Наименование,
                    Дата = c.Дата,
                    Бригада = c.Номер_бригады,
                    Результат = c.Результат
                }).ToList();
                textBox_tab5_1.Text = textBox_tab5_2.Text = textBox_tab5_3.Text = "";
                datePicker_tab5.DisplayDate = DateTime.Now;
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab5_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Обслуживание row = context.Обслуживание.Where(c => c.id_обслуживания == id_cur).First();
                row.Код_трубы = textBox_tab5_1.Text;
                row.Номер_бригады = textBox_tab5_2.Text;
                row.Дата = datePicker_tab5.SelectedDate ?? DateTime.Now;
                row.Результат = textBox_tab5_3.Text;
                row.id_сотрудника = account.id_аккаунта;
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab5.ItemsSource = context.Обслуживание.Select(c => new
                {
                    id = c.id_обслуживания,
                    idt = c.Код_трубы,
                    Участок = c.Труба.Участок.Наименование,
                    Дата = c.Дата,
                    Бригада = c.Номер_бригады,
                    Результат = c.Результат
                }).ToList();
                textBox_tab5_1.Text = textBox_tab5_2.Text = textBox_tab5_3.Text = "";
                datePicker_tab5.DisplayDate = DateTime.Now;
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab5_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    Обслуживание row = context.Обслуживание.Where(c => c.id_обслуживания == id_cur).First();
                    context.Обслуживание.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    dataGrid_tab5.ItemsSource = context.Обслуживание.Select(c => new
                    {
                        id = c.id_обслуживания,
                        idt = c.Код_трубы,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Бригада = c.Номер_бригады,
                        Результат = c.Результат
                    }).ToList();
                    textBox_tab5_1.Text = textBox_tab5_2.Text = textBox_tab5_3.Text = "";
                    datePicker_tab5.DisplayDate = DateTime.Now;
                    id_cur = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_tab5_clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_tab5_1.Text = textBox_tab5_2.Text = textBox_tab5_3.Text = "";
            datePicker_tab5.DisplayDate = DateTime.Now;
            id_cur = -1;
        }

        private void dataGrid_tab6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab6.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab6.Columns[0].GetCellContent(dataGrid_tab6.Items[dataGrid_tab6.SelectedIndex]) as TextBlock).Text);
            textBox_tab6_1.Text = (dataGrid_tab6.Columns[2].GetCellContent(dataGrid_tab6.Items[dataGrid_tab6.SelectedIndex]) as TextBlock).Text;
            textBox_tab6_3.Text = (dataGrid_tab6.Columns[4].GetCellContent(dataGrid_tab6.Items[dataGrid_tab6.SelectedIndex]) as TextBlock).Text;
            datePicker_tab6.Text = (dataGrid_tab6.Columns[3].GetCellContent(dataGrid_tab6.Items[dataGrid_tab6.SelectedIndex]) as TextBlock).Text;
        }

        private void button_tab6_choose_1_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 2, this, 0, textBox_tab6_1);
            form.ShowDialog();
        }
        private void button_tab6_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Фактические_потери row = new Фактические_потери();
                row.Код_трубы = textBox_tab6_1.Text;
                row.Значение = Convert.ToDouble(textBox_tab6_3.Text);
                row.Дата = datePicker_tab6.SelectedDate ?? DateTime.Now;
                row.id_сотрудника = account.id_аккаунта;
                context.Фактические_потери.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab6.ItemsSource = context.Фактические_потери.Select(c => new
                {
                    id = c.id_факт_потерь,
                    idt = c.Код_трубы,
                    Участок = c.Труба.Участок.Наименование,
                    Дата = c.Дата,
                    Значение = c.Значение
                }).ToList();
                textBox_tab6_1.Text = textBox_tab6_3.Text = "";
                textBox_tab6_3.Text = "0";
                datePicker_tab6.DisplayDate = DateTime.Now;
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab6_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Фактические_потери row = context.Фактические_потери.Where(c => c.id_факт_потерь == id_cur).First();
                row.Код_трубы = textBox_tab6_1.Text;
                row.Значение = Convert.ToDouble(textBox_tab6_3.Text);
                row.Дата = datePicker_tab6.SelectedDate ?? DateTime.Now;
                row.id_сотрудника = account.id_аккаунта;
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                dataGrid_tab6.ItemsSource = context.Фактические_потери.Select(c => new
                {
                    id = c.id_факт_потерь,
                    idt = c.Код_трубы,
                    Участок = c.Труба.Участок.Наименование,
                    Дата = c.Дата,
                    Значение = c.Значение
                }).ToList();
                textBox_tab6_1.Text = textBox_tab6_3.Text = "";
                textBox_tab6_3.Text = "0";
                datePicker_tab6.DisplayDate = DateTime.Now;
                id_cur = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab6_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    Фактические_потери row = context.Фактические_потери.Where(c => c.id_факт_потерь == id_cur).First();
                    context.Фактические_потери.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    dataGrid_tab6.ItemsSource = context.Фактические_потери.Select(c => new
                    {
                        id = c.id_факт_потерь,
                        idt = c.Код_трубы,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Значение = c.Значение
                    }).ToList();
                    textBox_tab6_1.Text = textBox_tab6_3.Text = "";
                    textBox_tab6_3.Text = "0";
                    datePicker_tab6.DisplayDate = DateTime.Now;
                    id_cur = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_tab6_clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_tab6_1.Text = textBox_tab6_3.Text = "";
            textBox_tab6_3.Text = "0";
            datePicker_tab6.DisplayDate = DateTime.Now;
            id_cur = -1;
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (button_tab8_choose_1 != null)
            {
                button_tab8_choose_1.IsEnabled = false;
                id1 = -1;
                textBox_tab8_1.Text = "";
            }
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            if (button_tab8_choose_1 != null)
            {
                button_tab8_choose_1.IsEnabled = true;
                id1 = -1;
                textBox_tab8_1.Text = "";
            }
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            if (button_tab8_choose_2 != null)
            {
                textBox_tab8_2.IsReadOnly = true;
                textBox_tab8_2.Text = "";
            }
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            if (button_tab8_choose_2 != null)
            {
                textBox_tab8_2.IsReadOnly = false;
                textBox_tab8_2.Text = "0";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan t1 = new TimeSpan(0, 0, 0), t2 = new TimeSpan(23, 59, 59);
            DateTime d1 = datePicker1_tab8.SelectedDate.Value.Date.Add(t1), d2 = datePicker2_tab8.SelectedDate.Value.Add(t2);
            var ras = context.Расчетные_потери.Where(d => (d.Дата >= d1) && (d.Дата <= d2)).ToList();
            var fact = context.Фактические_потери.Where(d => (d.Дата >= d1) && (d.Дата <= d2)).ToList();
            if (radioButton1.IsChecked == true)
            {
                ras = ras.Where(c => c.Труба.id_участка == id2).ToList();
                fact = fact.Where(c => c.Труба.id_участка == id2).ToList();
            }
            string diff = "";
            foreach (Расчетные_потери r in ras)
            {
                if (fact.Where(c => c.Дата == r.Дата).Count() == 0)
                {
                    diff += "Missing record of actual losses for " + r.Дата.Value.ToShortDateString() + "\r\n";
                }
            }
            foreach (Фактические_потери f in fact)
            {
                if (ras.Where(c => c.Дата == f.Дата).Count() == 0)
                {
                    diff += "Missing record of calculated losses for " + f.Дата.ToShortDateString() + "\r\n";
                }
            }
            if (diff != "")
            {
                MessageBox.Show("Inconsistencies is found!\r\n" + diff + "It is necessary to eliminate all inconsistencies for the preparation of the report!", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }
            //формирование отчета
            try
            {
                var list = context.Труба.Select(c => new
                {
                    id_участок = c.id_участка,
                    Населенный_пункт = c.Участок.Населенный_пункт,
                    Участок = c.Участок.Наименование,
                    Номер_трубы = c.Код_трубы,
                    Факт_потери = context.Фактические_потери.Where(d => (d.Код_трубы == c.Код_трубы) && (d.Дата >= d1) && (d.Дата <= d2)).Select(d => d.Значение).DefaultIfEmpty(0).Sum(),
                    Расч_потери = context.Расчетные_потери.Where(d => (d.Код_трубы == c.Код_трубы) && (d.Дата >= d1) && (d.Дата <= d2)).Select(d => d.Результат).DefaultIfEmpty(0).Sum(),
                    Различие = Math.Abs((1.0 - context.Фактические_потери.Where(d => (d.Код_трубы == c.Код_трубы) && (d.Дата >= d1) && (d.Дата <= d2)).Select(d => d.Значение).DefaultIfEmpty(0).Sum() / context.Расчетные_потери.Where(d => (d.Код_трубы == c.Код_трубы) && (d.Дата >= d1) && (d.Дата <= d2)).Select(d => d.Результат).DefaultIfEmpty(1).Sum())) * 100.0
                }).Where(c=>(c.Факт_потери!=0)||(c.Расч_потери!=0)).ToList();
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                worksheet = workbook.ActiveSheet;
                worksheet.Cells[1, 1] = "All regions";
                if (radioButton1.IsChecked == true)
                {
                    list = list.Where(c => c.id_участок == id2).ToList();
                    worksheet.Cells[1, 1] = "Region " + context.Участок.Where(c => c.id_участка == id2).First().Наименование;
                }
                if (radioButton3.IsChecked == true)
                {
                    double d = Convert.ToDouble(textBox_tab8_2.Text);
                    list = list.Where(c => c.Различие >= d).ToList();
                    worksheet.Cells[2, 1] = "Difference more than " + d.ToString() + "%";
                }

                app.Visible = true;

                worksheet.Cells[2, 1].EntireRow.Font.Bold = true;
                worksheet.Name = "Differences";
                if (list.Count == 0) return;
                worksheet.Cells[2, 1] = "Locality";
                worksheet.Cells[2, 2] = "Region";
                worksheet.Cells[2, 3] = "No. pipe";
                worksheet.Cells[2, 4] = "Act.losses";
                worksheet.Cells[2, 5] = "Calc.losses";
                worksheet.Cells[2, 6] = "Difference, %";
                for (int i = 0; i < list.Count; i++)
                {
                    worksheet.Cells[i + 3, 1] = list[i].Населенный_пункт;
                    worksheet.Cells[i + 3, 2] = list[i].Участок;
                    worksheet.Cells[i + 3, 3] = list[i].Номер_трубы;
                    worksheet.Cells[i + 3, 4] = list[i].Факт_потери;
                    worksheet.Cells[i + 3, 5] = list[i].Расч_потери;
                    worksheet.Cells[i + 3, 6] = list[i].Различие;
                }
                worksheet.Columns.AutoFit();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("There are no recordes for this period!", "Executing", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void datePicker1_tab8_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datePicker2_tab8.DisplayDateStart = datePicker1_tab8.SelectedDate;
            datePicker2_tab8.SelectedDate = datePicker1_tab8.SelectedDate;
        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            if (button_tab8_choose_2 != null)
            {
                button_tab8_choose_2.IsEnabled = false;
                button_tab8_choose_3.IsEnabled = false;
                textBox_tab8_3.Text = textBox_tab8_4.Text = "";
                id1 = -1;
                id_curr = "";
            }

        }

        private void radioButton5_Checked(object sender, RoutedEventArgs e)
        {
            if (button_tab8_choose_2 != null)
            {
                button_tab8_choose_2.IsEnabled = true;
                button_tab8_choose_3.IsEnabled = false;
                textBox_tab8_3.Text = textBox_tab8_4.Text = "";
                id1 = -1;
                id_curr = "";
            }
        }

        private void radioButton6_Checked(object sender, RoutedEventArgs e)
        {
            if (button_tab8_choose_2 != null)
            {
                button_tab8_choose_2.IsEnabled = false;
                button_tab8_choose_3.IsEnabled = true;
                textBox_tab8_3.Text = textBox_tab8_4.Text = "";
                id1 = -1;
                id_curr = "";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan t1 = new TimeSpan(0, 0, 0), t2 = new TimeSpan(23, 59, 59);
            DateTime d1 = datePicker1_tab8.SelectedDate.Value.Date.Add(t1), d2 = datePicker2_tab8.SelectedDate.Value.Add(t2);
            //формирование отчета
            try
            {
                var list = context.Обслуживание.Where(c => (c.Дата >= d1) && (c.Дата <= d2)).Select(c => new
                {
                    id_участок = c.Труба.id_участка,
                    id_труба = c.Код_трубы,
                    Участок = c.Труба.Участок.Наименование,
                    Номер_трубы = c.Код_трубы,
                    Бригада = c.Номер_бригады,
                    Дата = c.Дата,
                    Результат = c.Результат
                }).ToList();
                if (list.Count == 0) throw new InvalidCastException();
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
                app.Visible = true;
                worksheet = workbook.ActiveSheet;
                worksheet.Cells[1, 1] = "All regions";
                if (radioButton5.IsChecked == true)
                {
                    list = list.Where(c => c.id_участок == id2).ToList();
                    worksheet.Cells[1, 1] = "Region " + context.Участок.Where(c => c.id_участка == id2).First().Наименование;
                }
                if (radioButton6.IsChecked == true)
                {
                    list = list.Where(c => c.id_труба == textBox_tab8_4.Text).ToList();
                    worksheet.Cells[1, 1] = "Pipe " + context.Труба.Where(c => c.id_участка == id1).First().Код_трубы;
                }

                worksheet.Cells[2, 1].EntireRow.Font.Bold = true;
                worksheet.Name = "Difference";
                if (list.Count == 0) return;
                worksheet.Cells[2, 1] = "Region";
                worksheet.Cells[2, 2] = "No.pipe";
                worksheet.Cells[2, 3] = "Brigade number";
                worksheet.Cells[2, 4] = "Date";
                worksheet.Cells[2, 5] = "Result";
                for (int i = 0; i < list.Count; i++)
                {
                    worksheet.Cells[i + 3, 1] = list[i].Участок;
                    worksheet.Cells[i + 3, 2] = list[i].Номер_трубы;
                    worksheet.Cells[i + 3, 3] = list[i].Бригада;
                    worksheet.Cells[i + 3, 4] = list[i].Дата;
                    worksheet.Cells[i + 3, 5] = list[i].Результат;
                }
                worksheet.Columns.AutoFit();
            }
            catch (InvalidCastException ex)
            {
                MessageBox.Show("There are no recordes for this period!", "Executing", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public AdminWindow(DBase context, Аккаунты account)
        {
            InitializeComponent();
            this.context = context;
            this.account = account;
            int id = account.id_аккаунта;
            user = context.Сотрудники.Where(c => c.id_аккаунта == id).FirstOrDefault();
            userButton.Text = user.Фамилия + " " + user.Имя;
            datePicker1_tab8.SelectedDate = datePicker2_tab8.SelectedDate = datePicker3_tab8.SelectedDate = datePicker4_tab8.SelectedDate = datePicker_tab5.SelectedDate = datePicker_tab6.SelectedDate = DateTime.Now;
        }

        private void datePicker3_tab8_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            datePicker4_tab8.DisplayDateStart = datePicker3_tab8.SelectedDate;
            datePicker4_tab8.SelectedDate = datePicker3_tab8.SelectedDate;
        }

        private void stuffButton_Click(object sender, RoutedEventArgs e)
        {
            StaffWindow form = new StaffWindow(context, account);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void button_tab8_choose_1_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 1, this, 0, textBox_tab8_1);
            form.ShowDialog();
        }

        private void button_tab8_choose_2_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 1, this, 0, textBox_tab8_3);
            form.ShowDialog();
        }

        private void button_tab8_choose_3_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 2, this, 0, textBox_tab8_4);
            form.ShowDialog();
        }

        private void textBox_tab32_4_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_tab21_5_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text.Replace('.', ',');
            double d = 0;
            if (!Double.TryParse(tb.Text, out d))
            {
                tb.Text = "0";
            }
        }

        private void popup_Button_Click(object sender, RoutedEventArgs e)
        {
            string alfaBet = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890qwertyuiopasdfghjklzxcvbnm";
            char[] password=new char[8];
            Random r = new Random();
            for (int i=0; i < 8; i++)
            {
                password[i] = alfaBet[r.Next(0, alfaBet.Count())];
            }
            popup_textBox.Text = new string(password);
            passwordBox_tab1.Password = new string(password);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        string getMD5String(string inputString)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(inputString);
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] byteArrayHash = md5provider.ComputeHash(byteArray);
            return BitConverter.ToString(byteArrayHash);
        }
    }
}
