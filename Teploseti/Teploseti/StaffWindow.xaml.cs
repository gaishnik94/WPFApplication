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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace Teploseti
{
    /// <summary>
    /// Логика взаимодействия для StaffWindow.xaml
    /// </summary>
    public partial class StaffWindow : MetroWindow
    {
        DBase context;
        Аккаунты account;
        Сотрудники user;
        int id_cur = -1;
        public int id2=-1, id_k1 = -1, id_k2 = -1;
        public StaffWindow(DBase context, Аккаунты account)
        {
            this.context = context;
            this.account = account;
            int id = account.id_аккаунта;
            InitializeComponent();
            datePicker_tab3.SelectedDate = datePicker_tab5.SelectedDate = datePicker_tab6.SelectedDate = DateTime.Now;
            user = context.Сотрудники.Where(c => c.id_аккаунта == id).FirstOrDefault();
            userButton.Text = user.Фамилия + " " + user.Имя;
        }

        private void tab1_checkBox_Click(object sender, RoutedEventArgs e)
        {
            tab1_button_choose.IsEnabled = tab1_checkBox.IsChecked??false;
            tab1_textBox1.Text = "";
            id2 = -1;
            if (tab1_checkBox.IsChecked == false)
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
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (tabControl.SelectedIndex==0)
                {
                    if (id2==-1)
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
                        id_cur = -1;
                    }
                    else
                    {
                        dataGrid_tab5.ItemsSource = context.Обслуживание.Where(c=>c.Труба.id_участка==id2).Select(c => new
                        {
                            id = c.id_обслуживания,
                            idt = c.Код_трубы,
                            Участок = c.Труба.Участок.Наименование,
                            Дата = c.Дата,
                            Бригада = c.Номер_бригады,
                            Результат = c.Результат
                        }).ToList();
                        id_cur = -1;
                    }
                    
                }
                if (tabControl.SelectedIndex == 1)
                {
                    if (id2 == -1)
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
                    else
                    {
                        dataGrid_tab6.ItemsSource = context.Фактические_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                        {
                            id = c.id_факт_потерь,
                            idt = c.Код_трубы,
                            Участок = c.Труба.Участок.Наименование,
                            Дата = c.Дата,
                            Значение = c.Значение
                        }).ToList();
                    }
                }
                if (tabControl.SelectedIndex == 2)
                {
                    if (id2 == -1)
                    {
                        dataGrid_tab3.ItemsSource = context.Расчетные_потери.Select(c => new
                        {
                            id = c.id_расчетных_потерь,
                            idt = c.Код_трубы,
                            idk1=c.id_коэфф_тепл_отдачи,
                            idk2=c.id_коэф_тепл_потери,
                            n=c.Номер_коэфф,
                            k=c.Коэффициенты_учитыв__тепловые_потери.Коэффициент,
                            tv=c.t_воды,
                            ts=c.t_среды,
                            Участок = c.Труба.Участок.Наименование,
                            Дата = c.Дата,
                            Значение = c.Результат
                        }).ToList();
                    }
                    else
                    {
                        dataGrid_tab3.ItemsSource = context.Расчетные_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                        {
                            id = c.id_расчетных_потерь,
                            idt = c.Код_трубы,
                            idk1 = c.id_коэфф_тепл_отдачи,
                            idk2 = c.id_коэф_тепл_потери,
                            tv = c.t_воды,
                            ts = c.t_среды,
                            Участок = c.Труба.Участок.Наименование,
                            Дата = c.Дата,
                            Значение = c.Результат
                        }).ToList();
                    }
                }
                
            }
        }
        private void dataGrid_tab5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab5.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab5.Columns[0].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text);
            textBox_tab5_1.Text = (dataGrid_tab5.Columns[2].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text;
            textBox_tab5_2.Text = (dataGrid_tab5.Columns[3].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text;
            datePicker_tab5.Text = (dataGrid_tab5.Columns[4].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text;
            textBox_tab5_3.Text = (dataGrid_tab5.Columns[5].GetCellContent(dataGrid_tab5.Items[dataGrid_tab5.SelectedIndex]) as TextBlock).Text;
        }
        private void button_tab5_choose_1_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 2, this, 1, textBox_tab5_1);
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
                if (id2 == -1)
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
                    id_cur = -1;
                }
                else
                {
                    dataGrid_tab5.ItemsSource = context.Обслуживание.Where(c => c.Труба.id_участка == id2).Select(c => new
                    {
                        id = c.id_обслуживания,
                        idt = c.Код_трубы,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Бригада = c.Номер_бригады,
                        Результат = c.Результат
                    }).ToList();
                    id_cur = -1;
                }
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
                if (id2 == -1)
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
                    id_cur = -1;
                }
                else
                {
                    dataGrid_tab5.ItemsSource = context.Обслуживание.Where(c => c.Труба.id_участка == id2).Select(c => new
                    {
                        id = c.id_обслуживания,
                        idt = c.Код_трубы,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Бригада = c.Номер_бригады,
                        Результат = c.Результат
                    }).ToList();
                    id_cur = -1;
                }
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
                    if (id2 == -1)
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
                        id_cur = -1;
                    }
                    else
                    {
                        dataGrid_tab5.ItemsSource = context.Обслуживание.Where(c => c.Труба.id_участка == id2).Select(c => new
                        {
                            id = c.id_обслуживания,
                            idt = c.Код_трубы,
                            Участок = c.Труба.Участок.Наименование,
                            Дата = c.Дата,
                            Бригада = c.Номер_бригады,
                            Результат = c.Результат
                        }).ToList();
                        id_cur = -1;
                    }
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
        private void button_tab6_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Фактические_потери row = new Фактические_потери();
                row.Код_трубы = textBox_tab6_1.Text;
                row.Значение = Convert.ToDouble(textBox_tab6_3.Text);
                row.Дата = datePicker_tab6.SelectedDate??DateTime.Now;
                row.id_сотрудника = account.id_аккаунта;
                context.Фактические_потери.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                if (id2 == -1)
                {
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
                else
                {
                    dataGrid_tab6.ItemsSource = context.Фактические_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
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
                row.Дата = datePicker_tab6.SelectedDate??DateTime.Now;
                row.id_сотрудника = account.id_аккаунта;
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                if (id2 == -1)
                {
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
                else
                {
                    dataGrid_tab6.ItemsSource = context.Фактические_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                    {
                        id = c.id_факт_потерь,
                        idt = c.Код_трубы,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Значение = c.Значение
                    }).ToList();
                }
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
                    if (id2 == -1)
                    {
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
                    else
                    {
                        dataGrid_tab6.ItemsSource = context.Фактические_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                        {
                            id = c.id_факт_потерь,
                            idt = c.Код_трубы,
                            Участок = c.Труба.Участок.Наименование,
                            Дата = c.Дата,
                            Значение = c.Значение
                        }).ToList();
                    }
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

        private void button_tab6_choose_1_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 2, this, 1, textBox_tab6_1);
            form.ShowDialog();
        }

        private void button_tab3_choose_2_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox_tab3_4_MouseEnter(object sender, MouseEventArgs e)
        {
            if (id_k1 != -1)
            {
                dataGrid_popup.ItemsSource = context.Коэффициенты_теплоотдачи.Where(c => c.id_коэф_тепл_отдачи == id_k1).Select(c => new
                {
                    СНиП = c.Номер_СНиП,
                    t_до = c.t_до,
                    t_после = c.t_после,
                    КПМИ = c.Коэфф_помещения_с_малым_к_изл,
                    КПВИ = c.Коэфф_помещения_с_высоким_к_изл,
                    КОМИ = c.Коэфф_откр_с_малым_к_изл,
                    КОВИ = c.Коэфф_откр_с_высоким_к_изл
                }).ToList();
                popup.IsOpen = true;
            }
            
        }

        private void textBox_tab3_4_MouseLeave(object sender, MouseEventArgs e)
        {
            //popup.IsOpen = false;
        }

        private void textBox_tab3_5_MouseEnter(object sender, MouseEventArgs e)
        {
            if (id_k2 != -1)
            {
                dataGrid_popup.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Where(c => c.id_коэфф_тепл_потери == id_k2).Select(c => new
                {
                    СНиП = c.Номер_СНиП,
                    Материал = c.Материал.Наименование_материала,
                    Нач_диаметр = c.Нач_диаметр,
                    Кон_диаметр = c.Кон_диаметр,
                    Коэфф = c.Коэффициент,
                }).ToList();
                popup.IsOpen = true;
            }
        }

        private void tab2_checkBox_Click(object sender, RoutedEventArgs e)
        {
            tab2_button_choose.IsEnabled = tab2_checkBox.IsChecked ?? false;
            tab2_textBox1.Text = "";
            id2 = -1;
            if (tab2_checkBox.IsChecked == false)
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
        }

        private void button_tab3_choose_2_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 3, this, 1, textBox_tab3_4);
            form.ShowDialog();
            comboBox.SelectedIndex = 0;
            calculate();
        }

        private void button_tab3_choose_1_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 2, this, 1, textBox_tab3_1);
            form.ShowDialog();
            calculate();
        }

        private void button_tab3_choose_3_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 4, this, 1, textBox_tab3_5);
            form.ShowDialog();
            if (id_k1 == -1)
                return;
            Коэффициенты_теплоотдачи kt = context.Коэффициенты_теплоотдачи.Where(c => c.id_коэф_тепл_отдачи == id_k1).First();
            if (comboBox.SelectedIndex == 0)
            {
                textBox_tab3_4.Text = kt.Коэфф_помещения_с_малым_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 1)
            {
                textBox_tab3_4.Text = kt.Коэфф_помещения_с_высоким_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 2)
            {
                textBox_tab3_4.Text = kt.Коэфф_откр_с_малым_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 3)
            {
                textBox_tab3_4.Text = kt.Коэфф_откр_с_высоким_к_изл.Value.ToString();
            }
            calculate();
        }

        private void tab3_button_choose_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 4, this, 1, tab3_textBox1);
            if (form.ShowDialog() == true)
            {

                dataGrid_tab3.ItemsSource = context.Расчетные_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                {
                    id = c.id_расчетных_потерь,
                    idt = c.Код_трубы,
                    idk1 = c.id_коэфф_тепл_отдачи,
                    idk2 = c.id_коэф_тепл_потери,
                    tv = c.t_воды,
                    ts = c.t_среды,
                    Участок = c.Труба.Участок.Наименование,
                    Дата = c.Дата,
                    Значение = c.Результат
                }).ToList();
            }
        }

        private void tab2_button_choose_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 1, this, 1, tab2_textBox1);
            if (form.ShowDialog() == true)
            {
                dataGrid_tab6.ItemsSource = context.Фактические_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                {
                    id = c.id_факт_потерь,
                    idt = c.Код_трубы,
                    Участок = c.Труба.Участок.Наименование,
                    Дата = c.Дата,
                    Значение = c.Значение
                }).ToList();
            }
        }

        private void tab1_button_choose_Click(object sender, RoutedEventArgs e)
        {
            ListWindow form = new ListWindow(context, 1, this, 1, tab1_textBox1);
            if (form.ShowDialog()==true)
            {
                dataGrid_tab5.ItemsSource = context.Обслуживание.Where(c=>c.Труба.id_участка==id2).Select(c => new
                {
                    id = c.id_обслуживания,
                    idt = c.Код_трубы,
                    Участок = c.Труба.Участок.Наименование,
                    Дата = c.Дата,
                    Бригада = c.Номер_бригады,
                    Результат = c.Результат
                }).ToList();
                id_cur = -1;
            }
        }

        private void textBox_tab3_2_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text.Replace('.', ',');
            double d = 0;
            if (!Double.TryParse(tb.Text, out d))
            {
                tb.Text = "0";
            }
            calculate();
        }

        private void textBox_tab3_3_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text.Replace('.', ',');
            double d = 0;
            if (!Double.TryParse(tb.Text, out d))
            {
                tb.Text = "0";
            }
            calculate();
        }

        private void button_tab3_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Расчетные_потери row = new Расчетные_потери();
                row.Код_трубы = textBox_tab3_1.Text;
                row.t_воды = Convert.ToDouble(textBox_tab3_2.Text);
                row.t_среды = Convert.ToDouble(textBox_tab3_3.Text);
                row.id_коэф_тепл_потери = id_k2;
                row.id_коэфф_тепл_отдачи = id_k1;
                row.Дата = datePicker_tab3.SelectedDate??DateTime.Now;
                row.Результат = Convert.ToDouble(label51.Content);
                row.id_сотрудника = account.id_аккаунта;
                row.Дата_расчета = DateTime.Now;
                row.Номер_коэфф = comboBox.SelectedIndex;
                context.Расчетные_потери.Add(row);
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                if (id2 == -1)
                {
                    dataGrid_tab3.ItemsSource = context.Расчетные_потери.Select(c => new
                    {
                        id = c.id_расчетных_потерь,
                        idt = c.Код_трубы,
                        idk1 = c.id_коэфф_тепл_отдачи,
                        idk2 = c.id_коэф_тепл_потери,
                        n = c.Номер_коэфф,
                        k = c.Коэффициенты_учитыв__тепловые_потери.Коэффициент,
                        tv = c.t_воды,
                        ts = c.t_среды,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Значение = c.Результат
                    }).ToList();
                    textBox_tab3_1.Text = textBox_tab3_2.Text = textBox_tab3_3.Text = textBox_tab3_4.Text = textBox_tab3_5.Text = "";
                    id2 = id_k1 = id_k2 = -1;
                }
                else
                {
                    dataGrid_tab3.ItemsSource = context.Расчетные_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                    {
                        id = c.id_расчетных_потерь,
                        idt = c.Код_трубы,
                        idk1 = c.id_коэфф_тепл_отдачи,
                        idk2 = c.id_коэф_тепл_потери,
                        tv = c.t_воды,
                        ts = c.t_среды,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Значение = c.Результат
                    }).ToList();
                }
                textBox_tab3_1.Text = textBox_tab3_2.Text = textBox_tab3_3.Text = textBox_tab3_4.Text = textBox_tab3_5.Text = "";
                id2 = id_k1 = id_k2 = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dataGrid_tab3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid_tab3.SelectedIndex == -1) return;
            id_cur = Convert.ToInt32((dataGrid_tab3.Columns[0].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text);
            id_k1 = Convert.ToInt32((dataGrid_tab3.Columns[1].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text);
            id_k2 = Convert.ToInt32((dataGrid_tab3.Columns[2].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text);
            comboBox.SelectedIndex = -1;
            comboBox.SelectedIndex = Convert.ToInt32((dataGrid_tab3.Columns[3].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text);
            textBox_tab3_1.Text = (dataGrid_tab3.Columns[5].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text;
            textBox_tab3_2.Text = (dataGrid_tab3.Columns[6].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text;
            textBox_tab3_3.Text = (dataGrid_tab3.Columns[7].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text;
            textBox_tab3_5.Text = (dataGrid_tab3.Columns[8].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text;
            datePicker_tab3.Text = (dataGrid_tab3.Columns[9].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text;
            label51.Content = (dataGrid_tab3.Columns[10].GetCellContent(dataGrid_tab3.Items[dataGrid_tab3.SelectedIndex]) as TextBlock).Text;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Коэффициенты_теплоотдачи kt = context.Коэффициенты_теплоотдачи.Where(c => c.id_коэф_тепл_отдачи == id_k1).FirstOrDefault();
            if (kt == null) return;
            if (comboBox.SelectedIndex == 0)
            {
                textBox_tab3_4.Text = kt.Коэфф_помещения_с_малым_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 1)
            {
                textBox_tab3_4.Text = kt.Коэфф_помещения_с_высоким_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 2)
            {
                textBox_tab3_4.Text = kt.Коэфф_откр_с_малым_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 3)
            {
                textBox_tab3_4.Text = kt.Коэфф_откр_с_высоким_к_изл.Value.ToString();
            }
            calculate();
        }

        private void button_tab3_edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Расчетные_потери row = context.Расчетные_потери.Where(c=>c.id_расчетных_потерь==id_cur).First();
                row.Код_трубы = textBox_tab3_1.Text;
                row.t_воды = Convert.ToDouble(textBox_tab3_2.Text);
                row.t_среды = Convert.ToDouble(textBox_tab3_3.Text);
                row.id_коэф_тепл_потери = id_k2;
                row.id_коэфф_тепл_отдачи = id_k1;
                row.Дата = datePicker_tab3.SelectedDate??DateTime.Now;
                row.Результат = Convert.ToDouble(label51.Content);
                row.id_сотрудника = account.id_аккаунта;
                row.Дата_расчета = DateTime.Now;
                row.Номер_коэфф = comboBox.SelectedIndex;
                context.SaveChanges();
                MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                if (id2 == -1)
                {
                    dataGrid_tab3.ItemsSource = context.Расчетные_потери.Select(c => new
                    {
                        id = c.id_расчетных_потерь,
                        idt = c.Код_трубы,
                        idk1 = c.id_коэфф_тепл_отдачи,
                        idk2 = c.id_коэф_тепл_потери,
                        n = c.Номер_коэфф,
                        k = c.Коэффициенты_учитыв__тепловые_потери.Коэффициент,
                        tv = c.t_воды,
                        ts = c.t_среды,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Значение = c.Результат
                    }).ToList();
                    textBox_tab3_1.Text = textBox_tab3_2.Text = textBox_tab3_3.Text = textBox_tab3_4.Text = textBox_tab3_5.Text = "";
                    id2 = id_k1 = id_k2 = -1;
                }
                else
                {
                    dataGrid_tab3.ItemsSource = context.Расчетные_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                    {
                        id = c.id_расчетных_потерь,
                        idt = c.Код_трубы,
                        idk1 = c.id_коэфф_тепл_отдачи,
                        idk2 = c.id_коэф_тепл_потери,
                        tv = c.t_воды,
                        ts = c.t_среды,
                        Участок = c.Труба.Участок.Наименование,
                        Дата = c.Дата,
                        Значение = c.Результат
                    }).ToList();
                }
                textBox_tab3_1.Text = textBox_tab3_2.Text = textBox_tab3_3.Text = textBox_tab3_4.Text = textBox_tab3_5.Text = "";
                id2 = id_k1 = id_k2 = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void button_tab3_remove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Removing is an irreversible action. Are you sure?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    Расчетные_потери row = context.Расчетные_потери.Where(c => c.id_расчетных_потерь == id_cur).First();
                    context.Расчетные_потери.Remove(row);
                    context.SaveChanges();
                    MessageBox.Show("Success!", "Executing operation", MessageBoxButton.OK, MessageBoxImage.Information);
                    if (id2 == -1)
                    {
                        dataGrid_tab3.ItemsSource = context.Расчетные_потери.Select(c => new
                        {
                            id = c.id_расчетных_потерь,
                            idt = c.Код_трубы,
                            idk1 = c.id_коэфф_тепл_отдачи,
                            idk2 = c.id_коэф_тепл_потери,
                            n = c.Номер_коэфф,
                            k = c.Коэффициенты_учитыв__тепловые_потери.Коэффициент,
                            tv = c.t_воды,
                            ts = c.t_среды,
                            Участок = c.Труба.Участок.Наименование,
                            Дата = c.Дата,
                            Значение = c.Результат
                        }).ToList();
                        textBox_tab3_1.Text = textBox_tab3_2.Text = textBox_tab3_3.Text = textBox_tab3_4.Text = textBox_tab3_5.Text = "";
                        id2 = id_k1 = id_k2 = -1;
                    }
                    else
                    {
                        dataGrid_tab3.ItemsSource = context.Расчетные_потери.Where(c => c.Труба.id_участка == id2).Select(c => new
                        {
                            id = c.id_расчетных_потерь,
                            idt = c.Код_трубы,
                            idk1 = c.id_коэфф_тепл_отдачи,
                            idk2 = c.id_коэф_тепл_потери,
                            tv = c.t_воды,
                            ts = c.t_среды,
                            Участок = c.Труба.Участок.Наименование,
                            Дата = c.Дата,
                            Значение = c.Результат
                        }).ToList();
                    }
                    textBox_tab3_1.Text = textBox_tab3_2.Text = textBox_tab3_3.Text = textBox_tab3_4.Text = textBox_tab3_5.Text = "";
                    id2 = id_k1 = id_k2 = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void button_tab3_clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_tab3_1.Text = textBox_tab3_2.Text = textBox_tab3_3.Text = textBox_tab3_4.Text = textBox_tab3_5.Text = "";
            textBox_tab3_2.Text = textBox_tab3_3.Text = "0";
            id2 = id_k1 = id_k2 = -1;
        }

        private void textBox_tab6_3_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text.Replace('.', ',');
            double d = 0;
            if (!Double.TryParse(tb.Text, out d))
            {
                tb.Text = "0";
            }
        }

        private void tab3_checkBox_Click(object sender, RoutedEventArgs e)
        {
            tab3_button_choose.IsEnabled = tab3_checkBox.IsChecked ?? false;
            tab3_textBox1.Text = "";
            id2 = -1;
            if (tab3_checkBox.IsChecked == false)
            {
                dataGrid_tab3.ItemsSource = context.Расчетные_потери.Select(c => new
                {
                    id = c.id_расчетных_потерь,
                    idt = c.Код_трубы,
                    idk1 = c.id_коэфф_тепл_отдачи,
                    idk2 = c.id_коэф_тепл_потери,
                    n = c.Номер_коэфф,
                    k = c.Коэффициенты_учитыв__тепловые_потери.Коэффициент,
                    tv = c.t_воды,
                    ts = c.t_среды,
                    Участок = c.Труба.Участок.Наименование,
                    Дата = c.Дата,
                    Значение = c.Результат
                }).ToList();
            }
        }

        private void button_tab6_clear_Click(object sender, RoutedEventArgs e)
        {
            textBox_tab6_1.Text = textBox_tab6_3.Text = "";
            textBox_tab6_3.Text = "0";
            datePicker_tab6.DisplayDate = DateTime.Now;
            id_cur = -1;
        }

       void calculate()
        {
            if ((textBox_tab3_1.Text.Trim() == "")||(textBox_tab3_2.Text=="")||(textBox_tab3_3.Text=="")||(id_k1==-1)||(id_k2==-1))
            {
                return;
            }
            Коэффициенты_теплоотдачи kt = context.Коэффициенты_теплоотдачи.Where(c => c.id_коэф_тепл_отдачи == id_k1).First();
            Труба truba = context.Труба.Where(c => c.Код_трубы == textBox_tab3_1.Text).First();
            double an=0;
            if (comboBox.SelectedIndex==0)
            {
                an = kt.Коэфф_помещения_с_малым_к_изл.Value;
                textBox_tab3_4.Text = kt.Коэфф_помещения_с_малым_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 1)
            {
                an = kt.Коэфф_помещения_с_высоким_к_изл.Value;
                textBox_tab3_4.Text = kt.Коэфф_помещения_с_высоким_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 2)
            {
                an = kt.Коэфф_откр_с_малым_к_изл.Value;
                textBox_tab3_4.Text = kt.Коэфф_откр_с_малым_к_изл.Value.ToString();
            }
            if (comboBox.SelectedIndex == 3)
            {
                an = kt.Коэфф_откр_с_высоким_к_изл.Value;
                textBox_tab3_4.Text = kt.Коэфф_откр_с_высоким_к_изл.Value.ToString();
            }
            double t_water = Convert.ToDouble(textBox_tab3_2.Text), t_air=Convert.ToDouble(textBox_tab3_3.Text), k_tpr_m=truba.Материал.Коэффициент_теплопроводности_материал, k_tpr_i=truba.Материал.Коэффициент_теплопроводности_изоляция, b=Convert.ToDouble(textBox_tab3_5.Text.Replace('.',',')), dvt=truba.d_внутр_материал,dnt=truba.d_наруж_материал,dvi=truba.d_внутр_изол,dni=truba.d_наруж_изол, l=truba.Длина;
            double k = 1.0 / ((0.5 * k_tpr_m) * Math.Log(dnt / dvt) + (0.5* k_tpr_i)*Math.Log(dni / dvi) + 1.0 / (an * dni));
            double q = k * 3.14 * (t_water - t_air);
            double Q = b * l * q;
            label51.Content = Math.Round(Q).ToString();
        }
    }
}
