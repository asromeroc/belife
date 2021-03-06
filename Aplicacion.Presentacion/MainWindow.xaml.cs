﻿using System;
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
using System.Globalization;


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
            ActualizarListaContra();

        }

        private void ActualizarListaContra()
        {
            dgListarContrat.ItemsSource = null;
            dgListarContrat.ItemsSource = conector.Contratoes.ToList();

        }

        private void ActualizarLista()
        {


            dgListarClientes.ItemsSource = null;
            dgListarClientes.ItemsSource = conector.Clientes.ToList();



        }

        
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
        
            try
            {
            
            int idSexo = 0;
            int idEstadoCivil = 0;


            //valida nombres
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

            //valida Rut
            if (string.IsNullOrEmpty(txtRut.Text))
            {

                MessageBox.Show("Debe ingresar un rut");
                txtRut.Focus();
                return;

            }

            //Valida sexo
            if (cbSexo.SelectedIndex == -1)
            {
            MessageBox.Show("Debe seleccionar sexo");
            cbSexo.Focus();
            return;
            }
            if (cbHombre.IsSelected)
            {
                    //id sexo = hombre
                    idSexo = 1;
                }
            if (cbMujer.IsSelected)
            {
                    //id sexo = mujer
                    idSexo = 2;
                }
                

            //valida estado civil
            if (cbEstadoCivil.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar Estado Civil");
                cbEstadoCivil.Focus();
                return;
                }
                if (cbSoltero.IsSelected)

                {
                    //soltero
                    idEstadoCivil = 1;
                }
                if (cbCasado.IsSelected)
                {
                    //casado
                    idEstadoCivil = 2;
                }
                if (cbDivorciado.IsSelected)
                {
                    //divorciado
                    idEstadoCivil = 3;
                }
                if (cbViudo.IsSelected)
                    {
                    //viudo
                    idEstadoCivil = 4;
                }

                //valida fecha y edad
                if (dpInsertarFechaNacimiento.SelectedDate == null)
                {
                    MessageBox.Show("Debe seleccionar una fecha");
                    dpInsertarFechaNacimiento.Focus();
                    return;
                }
                
                DateTime fechaNacimiento = (DateTime)dpInsertarFechaNacimiento.SelectedDate;

                string fecha = fechaNacimiento.ToString("dd/MM/yyyy");

                int edadEnDias = ((TimeSpan)(DateTime.Now - fechaNacimiento)).Days;
                edadEnDias = edadEnDias / 365;

                if (edadEnDias < 18)
                {
                    MessageBox.Show("El cliente debe ser mayor de 18 años");
                    dpInsertarFechaNacimiento.Focus();
                    return;
                }


                Cliente cli = new Cliente();
                cli.Nombres = txtNombres.Text;
                cli.Apellidos = txtApellidos.Text;
                cli.FechaNacimiento = fecha;
                cli.IdSexo = idSexo;
                cli.RutCliente = txtRut.Text;
                cli.IdEstadoCivil = idEstadoCivil;


                conector.Clientes.Add(cli);
                conector.SaveChanges();

                MessageBox.Show("Se ha Ingresado un cliente");
                txtNombres.Text = string.Empty;
                txtRut.Text = string.Empty;
                txtApellidos.Text = string.Empty;
                cbSexo.SelectedIndex = -1;
                cbEstadoCivil.SelectedIndex = -1;
                dpInsertarFechaNacimiento.SelectedDate = null;
                ActualizarLista();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRegistrarContrato_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                Contrato cont = new Contrato();
                string cod1 = "VID01";
                string cod2 = "VID02";
                string cod3 = "VID03";
                string cod4 = "VID04";
                string cod5 = "VID05";

                txtPrima.IsEnabled = false;
                //declaracion
                cont.Observaciones = txtDeclaracion.Text;

                //numero de contrato
                string numContrato = DateTime.Now.ToString("yyyy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
                cont.Numero = numContrato;


                //rut
                cont.RutCliente = txtRut1.Text;
                //IdPlan
                if (cbxContratos.SelectedIndex == 0)
                {
                    cont.CodigoPlan = cod1;


                }
                else if (cbxContratos.SelectedIndex == 1)
                {
                    cont.CodigoPlan = cod2;
                }
                else if (cbxContratos.SelectedIndex == 2)
                {
                    cont.CodigoPlan = cod3;
                }
                else if (cbxContratos.SelectedIndex == 3)
                {
                    cont.CodigoPlan = cod4;
                }
                else if (cbxContratos.SelectedIndex == 4)
                {
                    cont.CodigoPlan = cod5;
                }

                // fecha de creacion

                DateTime fechaCreacion;
                fechaCreacion = DateTime.Now;
                cont.FechaCreacion = fechaCreacion;

                // Fecha inicio Vigencia
                cont.FechaInicioVigencia = dpInicioVigencia.SelectedDate.Value;


                //fechaFinVigencia 
                cont.FechaFinVigencia = dpInicioVigencia.SelectedDate.Value.AddYears(1);

                //Vigencia

                cont.Vigente = true;

                //Declaracion salud

                if (cbxDeclaracionSalud.SelectedIndex == 0)
                {
                    cont.DeclaracionSalud = true;

                }
                else if (cbxDeclaracionSalud.SelectedIndex == 1)
                {
                    cont.DeclaracionSalud = false;
                }

                //CODIGO NUEVOOOOOO =================================




                DateTime hoy = DateTime.Today;
                Cliente cli = conector.Clientes.First(cl => cl.RutCliente == cont.RutCliente);



                if (string.IsNullOrEmpty(txtPrima1.Text))
                {
                    MessageBox.Show("Debe calcular la prima antes de registrar el contrato");
                    txtPrima.Focus();
                    return;
                }

                if (dpInicioVigencia.SelectedDate == null)
                {
                    MessageBox.Show("Debe seleccionar una fecha de inicio de vigencia");
                    dpInsertarFechaNacimiento.Focus();
                    return;
                }

                if (cbxDeclaracionSalud.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar declaracion de salud");
                    cbSexo.Focus();
                    return;
                }

                if (cbxContratos.SelectedIndex == -1)
                {
                    MessageBox.Show("Debe seleccionar un contrato");
                    cbSexo.Focus();
                    return;
                }

                string value1 = txtPrima1.Text;
                Double Prima_total = Convert.ToDouble(value1);


                MessageBox.Show(Prima_total.ToString());

               

                Plan pl = conector.Plans.First(p => p.IdPlan == cont.CodigoPlan);
                Double p_base = pl.PrimaBase;


                cont.PrimaAnual = Prima_total + p_base;
                MessageBox.Show((cont.PrimaAnual).ToString());
                cont.PrimaMensual = cont.PrimaAnual / 12;


                conector.Contratoes.Add(cont);
                conector.Configuration.ValidateOnSaveEnabled = false;
                conector.SaveChanges();

                MessageBox.Show("Se ha Ingresado un cliente");
                txtFiltrarRegistro.Text = string.Empty;
                txtDeclaracion.Text = string.Empty;
                ActualizarListaContra();
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

            //ELIMINAR CLIENTE.
            private void btnEliminar_Click(object sender, RoutedEventArgs e)
                    {
            try
            {
                string rut = txtEliminar.Text;
                Cliente cli = conector.Clientes.First(c => c.RutCliente == rut);
                conector.Clientes.Remove(cli);
                conector.SaveChanges();

                MessageBox.Show("Se ha eliminado el rut : " + cli.RutCliente);
                txtEliminar.Text = string.Empty;
                ActualizarLista();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBuscarRut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string rut = txtRut1.Text;
                int contador = conector.Clientes.Count(c => c.RutCliente == rut);
                if (contador == 0)
                {
                    MessageBox.Show("No hay personas con ese RUT");

                }
                else
                {
                    Cliente cli = conector.Clientes.First(c => c.RutCliente == rut);
                    txtNombreCon.IsEnabled = false;
                    txtApellidoContra.IsEnabled = false;

                    txtNombreCon.Text = cli.Nombres;
                    txtApellidoContra.Text = cli.Apellidos;
                    int idEstado = cli.IdEstadoCivil;
                    txtEstadoCivilPrima.Text = idEstado.ToString();
                    string fecha = cli.FechaNacimiento;
                    int sexoPrima = cli.IdSexo;
                    txtSexoPrima.Text = sexoPrima.ToString();
                    
                    DateTime mydate = DateTime.ParseExact(fecha,"dd/MM/yyyy",CultureInfo.InvariantCulture);
                    
                    string fechaCorta = mydate.ToString("dd/MM/yyyy");

                    int edadEnDias = ((TimeSpan)(DateTime.Now - mydate)).Days;
                    edadEnDias = edadEnDias / 365;

                    txtEdad.Text = edadEnDias.ToString();
                    txtEdad.IsEnabled = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }


        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFiltrarRegistro.Text))
                {
                    MessageBox.Show("Debe seleccionar ID de SEXO");
                    cbEstadoCivil.Focus();
                    return;
                }
                int IdSexo = int.Parse(txtFiltrarRegistro.Text);
                List<Cliente> c = conector.Clientes.Where(cli => cli.IdSexo == IdSexo).ToList();
                dgListarClientes.ItemsSource = c;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
            
            
        }
        //CALCULO PRIMA
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //prima base
                double valorPlan = 0;
                if (cbxContratos.SelectedIndex ==-1)
                {
                    MessageBox.Show("ingrese un plan a calcular");
                    return;
                }
                if (cbVidaInicial.IsSelected)
                {
                    valorPlan = 0.5;
                }
                else if (cbVidaTotal.IsSelected)
                {
                    valorPlan = 3.5;
                }
                else if (cbVidaConductor.IsSelected)
                {
                    valorPlan = 1.2;
                }
                else if (cbVidaSenior.IsSelected)
                {
                    valorPlan = 2;
                }
                else if (cbVidaAhorro.IsSelected)
                {
                    valorPlan = 3.5;
                }
                //mas prima edad
                int edad = Convert.ToInt32(txtEdad.Text);
                if (edad >= 18 && edad <= 25)
                {
                    valorPlan = valorPlan + 3.6;
                }
                if (edad >= 26 && edad <= 45)
                {
                    valorPlan = valorPlan + 2.4;
                }
                if (edad >= 46)
                {
                    valorPlan = valorPlan + 6;
                }
                //mas prima sexo
                int sexo = Convert.ToInt32(txtSexoPrima.Text);
                if (sexo == 1)
                {
                    valorPlan = valorPlan + 2.4;
                }
                if (sexo == 2)
                {
                    valorPlan = valorPlan + 1.2;
                }
                //mas prima por estado civil
                int idestado = Convert.ToInt32(txtEstadoCivilPrima.Text);
                if (idestado == 1)
                {
                    valorPlan = valorPlan + 4.8;
                }
                if (idestado == 2)
                {
                    valorPlan = valorPlan + 2.4;
                }
                if (idestado == 3)
                {
                    valorPlan = valorPlan + 3.6;
                }
                if (idestado == 4)
                {
                    valorPlan = valorPlan + 3.6;
                }

                //string plan = valorPlan.ToString();
                //MessageBox.Show(plan);

                txtPrima.Text = valorPlan.ToString()+" UF";
                txtPrima1.Text = valorPlan.ToString();
                txtPrima.IsEnabled = false;
                
            
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
    }

        private void btnFiltrarCont_Click(object sender, RoutedEventArgs e)
        {
            string numcontr = txtNumfiltrar.Text;
            List<Contrato> c = conector.Contratoes.Where(ca => ca.Numero == numcontr).ToList();
            dgListarContrat.ItemsSource = c;
        }

        private void btnActualizarCliente_Click(object sender, RoutedEventArgs e)
        {
            int cod1 = 1;
            int cod2 = 2;


            try
            {
                //validaciones

                //valida nombres
                if (string.IsNullOrEmpty(txtActNombre.Text))
                {

                    MessageBox.Show("Debe ingresar los nombres");
                    txtActNombre.Focus();

                    return;

                }

                //valida apellidos
                if (string.IsNullOrEmpty(txtActApellido.Text))
                {

                    MessageBox.Show("Debe ingresar los apellidos");
                    txtActApellido.Focus();
                    return;

                }

                //valida Rut
                if (string.IsNullOrEmpty(txtActRutBuscar.Text))
                {

                    MessageBox.Show("Debe ingresar un rut");
                    txtRut.Focus();
                    return;

                }

                DateTime fechaNacimiento = (DateTime)dpActFechaNacimiento.SelectedDate;

                string fecha = fechaNacimiento.ToString("dd/MM/yyyy");

                int edadEnDias = ((TimeSpan)(DateTime.Now - fechaNacimiento)).Days;
                edadEnDias = edadEnDias / 365;

                if (edadEnDias < 18)
                {
                    MessageBox.Show("El cliente debe ser mayor de 18 años");
                    dpInsertarFechaNacimiento.Focus();
                    return;
                }

                string rut = txtActRutBuscar.Text;
                Cliente cli = conector.Clientes.First(c => c.RutCliente == rut);

                cli.Nombres = txtActNombre.Text;
                cli.Apellidos = txtActApellido.Text;
                cli.FechaNacimiento = fecha;
                

                if (cbxActSexo.SelectedIndex == 0)
                {
                    cli.IdSexo = cod1;

                }
                else if (cbxActSexo.SelectedIndex == 1)
                {
                    cli.IdSexo = cod2;

                }

                //ID ESTADO CIVIL
                if (cbxActEstadoCivil.SelectedIndex == 0)
                {
                    cli.IdEstadoCivil = 1;

                }
                else if (cbxActEstadoCivil.SelectedIndex == 1)
                {
                    cli.IdEstadoCivil = 2;

                }
                else if (cbxActEstadoCivil.SelectedIndex == 2)
                {
                    cli.IdEstadoCivil = 3;

                }
                else if (cbxActEstadoCivil.SelectedIndex == 3)
                {
                    cli.IdEstadoCivil = 4;

                }

                conector.SaveChanges();
                MessageBox.Show("se ha actualizado al cliente");
                ActualizarLista();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnActBuscarRut_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string rut = txtActRutBuscar.Text;
                int contador = conector.Clientes.Count(cli => cli.RutCliente == rut);
                if (contador == 0)
                {

                    MessageBox.Show("No hay personas con ese rut");
                }
                else
                {
                    Cliente c = conector.Clientes.First(cli => cli.RutCliente == rut);
                    txtActNombre.IsEnabled = true;
                    txtActApellido.IsEnabled = true;
                    cbxActEstadoCivil.IsEnabled = true;
                    cbxActSexo.IsEnabled = true;
                    dpActFechaNacimiento.IsEnabled = true;
                    btnActualizarCliente.IsEnabled = true;

                    txtActNombre.Text = c.Nombres;
                    txtActApellido.Text = c.Apellidos;
                    cbxActSexo.SelectedIndex = c.IdSexo - 1;
                    cbxActEstadoCivil.SelectedIndex = c.IdEstadoCivil - 1;
                    dpActFechaNacimiento.Text = c.FechaNacimiento;



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);


            }
        }
    }
    
}