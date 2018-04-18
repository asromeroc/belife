using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Aplicacion.Datos;
namespace Aplicacion.Presentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BeLifeEntities conector;
        public MainWindow()
        {
            InitializeComponent();
            if (conector == null)
            {
                conector = new BeLifeEntities();
            }
            ActualizarLista();
        }

        private void ActualizarLista()
        {
            dgListarClientes.ItemsSource = null;
            dgListarClientes.ItemsSource = conector.Clientes.ToList();

        }
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            int idSexo = 0;
            int idEstadoCivil = 0;
            //string edad;
           
                //Valida nombres
                if (string.IsNullOrEmpty(txtNombres.Text))
                {

                    MessageBox.Show("Debe ingresar los nombres");
                    txtNombres.Focus();

                    return;

                }
                //valida apellidos
                if (string.IsNullOrEmpty(txtApellidos.Text))
                {

                    MessageBox.Show("Debe ingresar los apellidos");
                    txtApellidos.Focus();
                    return;

                }
                //valida rut
                if (string.IsNullOrEmpty(txtRut.Text))
                {

                    MessageBox.Show("Debe el rut");
                    txtRut.Focus();
                    return;

                }
                //Valida sexo
                if (cbSexo.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar sexo");
                    cbSexo.Focus();
                }
                else
                {

                    if (cbSexo.SelectedIndex == 0)

                    {
                        //id sexo = hombre
                        idSexo = 1;
                    }
                    else
                    {
                        //id sexo = mujer
                        idSexo = 2;
                    }
                }
                
                //valida estado civil
                if (cbEstadoCivil.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar Estado Civil");
                    cbEstadoCivil.Focus();
                }
                else
                {

                    if (cbSexo.SelectedIndex == 0)

                    {
                        //soltero
                        idSexo = 1;
                    }
                    if (cbSexo.SelectedIndex == 1)
                    {
                        //casado
                        idSexo = 2;
                    }
                    if (cbSexo.SelectedIndex == 2)
                    {
                        //divorciado
                        idSexo = 3;
                    }
                    else
                    {
                        //viudo
                        idSexo = 4;
                    }

                }

            //    //valida edad
            //    edad = dpInsertarFechaNacimiento.SelectedDate.ToString();
                

            //if (edad < 0)
            //    {
            //       MessageBox.Show("la fecha seleccionada es igual o mayor al dia actual");
            //    }

            //    int años = edad / 365;

            //    if (años < 18)
            //    {
            //    MessageBox.Show("debe ser mayor a 18 años");
            //    }




            try
            {
                Cliente cli = new Cliente();
                cli.Nombres = txtNombres.Text;
                cli.Apellidos = txtApellidos.Text;
                cli.FechaNacimiento = dpInsertarFechaNacimiento.SelectedDate.ToString();
                cli.IdSexo = idSexo;
                cli.RutCliente = txtRut.Text;
                cli.IdEstadoCivil = idEstadoCivil;


                conector.Clientes.Add(cli);
                conector.SaveChanges();

                MessageBox.Show("Se ha Ingresado un cliente");
                txtNombres.Text = string.Empty;
                txtRut.Text = string.Empty;
                txtApellidos.Text = string.Empty;
                ActualizarLista();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}