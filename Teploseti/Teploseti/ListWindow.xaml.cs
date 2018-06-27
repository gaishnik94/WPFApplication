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
    /// Логика взаимодействия для ListWindow.xaml
    /// </summary>
    public partial class ListWindow : MetroWindow
    {
        DBase context;
        MetroWindow form;
        TextBox c;
        int table, form_number;

        public ListWindow(DBase context, int table, MetroWindow form, int form_number, TextBox control)
        {
            InitializeComponent();
            this.form = form;
            this.context=context;
            this.table = table;
            this.form_number = form_number;
            this.c = control;
            if (table == 0)
            {
                dataGrid_tab.ItemsSource = context.Материал.Select(c => new
                {
                    Номер=c.id_материала,
                    Материал = c.Наименование_материала,
                    К_теплопров_м = c.Коэффициент_теплопроводности_материал,
                    Изоляция = c.Наименование_изоляции,
                    К_теплопров_из = c.Коэффициент_теплопроводности_изоляция
                }).ToList();
                stack.Visibility=Visibility.Hidden;                
            }
            if (table == 1)
            {
                dataGrid_tab.ItemsSource = context.Участок.Select(c => new
                {
                    Номер = c.id_участка,
                    Наименование = c.Наименование,
                    Населенный_пункт = c.Населенный_пункт
                }).ToList();
                tab3_checkBox.Content = "Поиск по нас.пункту";
            }
            if (table == 2)
            {
                dataGrid_tab.ItemsSource = context.Труба.Select(c => new
                {
                    Участок=c.Участок.Наименование,
                    Номер = c.Код_трубы,
                    Длина = c.Длина,
                    Материал=c.Материал.Наименование_материала,
                    Изоляция=c.Материал.Наименование_изоляции
                }).ToList();
                tab3_checkBox.Content = "Поиск по участку";
            }
            if (table == 3)
            {
                dataGrid_tab.ItemsSource = context.Коэффициенты_теплоотдачи.Select(c => new
                {
                    Номер = c.id_коэф_тепл_отдачи,
                    СНиП = c.Номер_СНиП,
                    t_до = c.t_до,
                    t_после = c.t_после,
                    Поверхность = c.Тип_поверхности,
                    КПМИ=c.Коэфф_помещения_с_малым_к_изл,
                    КПВИ=c.Коэфф_помещения_с_высоким_к_изл,
                    КОМИ=c.Коэфф_откр_с_малым_к_изл,
                    КОВИ=c.Коэфф_откр_с_высоким_к_изл
                }).ToList();
                tab3_checkBox.Content = "Поиск по СНиП";
            }
            if (table == 4)
            {
                dataGrid_tab.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Select(c => new
                {
                    Номер = c.id_коэфф_тепл_потери,
                    СНиП = c.Номер_СНиП,
                    Материал = c.Материал.Наименование_материала,
                    Нач_диаметр = c.Нач_диаметр,
                    Кон_диаметр = c.Кон_диаметр,
                    Коэффициент = c.Коэффициент
                }).ToList();
                tab3_checkBox.Content = "Поиск по СНиП";
            }
        }

        private void tab3_checkBox_Click(object sender, RoutedEventArgs e)
        {
            tab3_button_choose.IsEnabled = tab3_checkBox.IsChecked??false;
            tab3_textBox1.IsReadOnly = !tab3_checkBox.IsChecked.Value;
            tab3_textBox1.Text = "";
            if (tab3_checkBox.IsChecked == false)
            {
                if (table == 1)
                {
                    dataGrid_tab.ItemsSource = context.Участок.Select(c => new
                    {
                        Номер = c.id_участка,
                        Наименование = c.Наименование,
                        Населенный_пункт = c.Населенный_пункт
                    }).ToList();
                }
                if (table == 2)
                {
                    dataGrid_tab.ItemsSource = context.Труба.Select(c => new
                    {
                        Участок = c.Участок.Наименование,
                        Номер = c.Код_трубы,
                        Длина = c.Длина,
                        Материал = c.Материал.Наименование_материала,
                        Изоляция = c.Материал.Наименование_изоляции
                    }).ToList();
                }
                if (table == 3)
                {
                    dataGrid_tab.ItemsSource = context.Коэффициенты_теплоотдачи.Select(c => new
                    {
                        Номер = c.id_коэф_тепл_отдачи,
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
                if (table == 4)
                {
                    dataGrid_tab.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Select(c => new
                    {
                        Номер = c.id_коэфф_тепл_потери,
                        СНиП = c.Номер_СНиП,
                        Материал = c.Материал.Наименование_материала,
                        Нач_диаметр = c.Нач_диаметр,
                        Кон_диаметр = c.Кон_диаметр,
                        Коэффициент = c.Коэффициент
                    }).ToList();
                }
            }
        }

        private void button_tab3_add_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid_tab.SelectedItems.Count > 0)
            {
                if (table == 0)
                {
                    if (form_number == 0)
                    {
                        AdminWindow f = (AdminWindow)form;
                        f.id1 = Convert.ToInt32((dataGrid_tab.Columns[0].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text);
                    }
                    c.Text = (dataGrid_tab.Columns[2].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text;
                    this.DialogResult = true;
                }
                if (table == 1)
                {
                    if (form_number == 0)
                    {
                        AdminWindow f = (AdminWindow)form;
                        f.id2 = Convert.ToInt32((dataGrid_tab.Columns[0].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text);
                    }
                    if (form_number == 1)
                    {
                        StaffWindow f = (StaffWindow)form;
                        f.id2 = Convert.ToInt32((dataGrid_tab.Columns[0].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text);
                    }
                    c.Text = (dataGrid_tab.Columns[2].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text;
                    this.DialogResult = true;
                }
                if (table == 2)
                {
                    c.Text = (dataGrid_tab.Columns[0].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text;
                    this.DialogResult = true;
                }
                if (table == 3)
                {
                    if (form_number == 1)
                    {
                        StaffWindow f = (StaffWindow)form;
                        f.id_k1 = Convert.ToInt32((dataGrid_tab.Columns[0].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text);
                    }
                    c.Text = (dataGrid_tab.Columns[6].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text;
                    this.DialogResult = true;
                }
                if (table == 4)
                {
                    if (form_number == 1)
                    {
                        StaffWindow f = (StaffWindow)form;
                        f.id_k2 = Convert.ToInt32((dataGrid_tab.Columns[0].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text);
                    }
                    c.Text = (dataGrid_tab.Columns[6].GetCellContent(dataGrid_tab.Items[dataGrid_tab.SelectedIndex]) as TextBlock).Text;
                    this.DialogResult = true;
                }
            }
        }

        private void tab3_button_choose_Click(object sender, RoutedEventArgs e)
        {
            if (table == 1)
            {
                dataGrid_tab.ItemsSource = context.Участок.Where(c=>c.Населенный_пункт.StartsWith(tab3_textBox1.Text)).Select(c => new
                {
                    Номер = c.id_участка,
                    Наименование = c.Наименование,
                    Населенный_пункт = c.Населенный_пункт
                }).ToList();
            }
            if (table == 2)
            {
                dataGrid_tab.ItemsSource = context.Труба.Where(c => c.Участок.Наименование.StartsWith(tab3_textBox1.Text)).Select(c => new
                {
                    Участок = c.Участок.Наименование,
                    Номер = c.Код_трубы,
                    Длина = c.Длина,
                    Материал = c.Материал.Наименование_материала,
                    Изоляция = c.Материал.Наименование_изоляции
                }).ToList();
            }
            if (table == 3)
            {
                dataGrid_tab.ItemsSource = context.Коэффициенты_теплоотдачи.Where(c => c.Номер_СНиП==tab3_textBox1.Text).Select(c => new
                {
                    Номер = c.id_коэф_тепл_отдачи,
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
            if (table == 4)
            {
                dataGrid_tab.ItemsSource = context.Коэффициенты_учитыв__тепловые_потери.Where(c => c.Номер_СНиП == tab3_textBox1.Text).Select(c => new
                {
                    Номер = c.id_коэфф_тепл_потери,
                    СНиП = c.Номер_СНиП,
                    Материал = c.Материал.Наименование_материала,
                    Нач_диаметр = c.Нач_диаметр,
                    Кон_диаметр = c.Кон_диаметр,
                    Коэффициент = c.Коэффициент
                }).ToList();
            }
        }
    }
}
